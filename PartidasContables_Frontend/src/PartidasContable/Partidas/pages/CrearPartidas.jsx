import { useEffect, useState } from "react";
import { usePartida } from "../hooks/UsePartida";
import { useCatalogoGet } from "../hooks";


export const CrearPartidas = () => {
  const { createPartida, isLoading, error: apiError } = usePartida();
  const { cuentas, isLoading: loadingCuentas, error: errorCuentas } = useCatalogoGet();  

  const [partida, setPartida] = useState({
    fecha: "",
    descripcion: "",
    idUsuario: "",
    detalles: []
  });

  const [nuevoDetalle, setNuevoDetalle] = useState({
    idCatalogoCuenta: "",
    descripcion: "",
    monto: 0, // El monto se actualizara automaticamente
    tipoCuenta: "",
  });

  const [formError, setFormError] = useState("");
  const [success, setSuccess] = useState("");
  const [cuentaBusqueda, setCuentaBusqueda] = useState("");
  const [cuentaNoExistente, setCuentaNoExistente] = useState(false);

  const agregarDetalle = () => {
    if (!nuevoDetalle.idCatalogoCuenta) {
      setFormError("La cuenta es obligatoria");
      return;
    }

    setPartida(prev => ({
      ...prev,
      detalles: [...prev.detalles, nuevoDetalle]
    }));

    setNuevoDetalle({
      idCatalogoCuenta: "",
      descripcion: "",
      monto: 0, // Reiniciar monto despues de agregar el detalle
      tipoCuenta: "",
    });
  };

  const eliminarDetalle = (index) => {
    setPartida(prev => ({
      ...prev,
      detalles: prev.detalles.filter((_, i) => i !== index)
    }));
  };

  const buscarCuenta = (input) => {
    setCuentaBusqueda(input);
    const cuentaEncontrada = cuentas.find(cuenta =>
      cuenta.numeroCuenta.includes(input) || cuenta.descripcion.toLowerCase().includes(input.toLowerCase())
    );

    if (cuentaEncontrada) {
      setNuevoDetalle(prev => ({
        ...prev,
        idCatalogoCuenta: cuentaEncontrada.id,
        descripcion: cuentaEncontrada.descripcion,
        tipoCuenta: cuentaEncontrada.tipoCuenta,
        monto: cuentaEncontrada.monto || 0, // Asignar monto de la cuenta automaticamente
      }));
      setCuentaNoExistente(false);
    } else {
      setCuentaNoExistente(true);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setFormError("");
    setSuccess("");

    if (!partida.fecha || !partida.descripcion || partida.detalles.length === 0) {
      setFormError("Todos los campos son requeridos y debe haber al menos un detalle");
      return;
    }

    try {
      const result = await createPartida(partida);

      if (result.status) {
        setSuccess("Partida creada exitosamente");
        setPartida({
          fecha: "",
          descripcion: "",
          idUsuario: "",
          detalles: []
        });
      } else {
        setFormError(result.message || "Error al crear la partida");
      }
    } catch (error) {
      setFormError("Error al conectar con el servidor");
    }
  };

  const totalDetalles = partida.detalles.reduce((sum, detalle) => sum + detalle.monto, 0);


  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-6">Crear Partida Contable</h1>

      {(formError || apiError || errorCuentas) && (
        <div className="mb-4 p-2 bg-red-100 text-red-800 rounded">
          {formError || apiError || errorCuentas}
        </div>
      )}

      {success && (
        <div className="mb-4 p-2 bg-green-100 text-green-800 rounded">
          {success}
        </div>
      )}

      <form onSubmit={handleSubmit} className="space-y-6">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block font-medium mb-1">Fecha</label>
            <input
              type="date"
              value={partida.fecha}
              onChange={(e) => setPartida(prev => ({ ...prev, fecha: e.target.value }))}
              className="border p-2 rounded w-full"
            />
          </div>
          <div>
            <label className="block font-medium mb-1">Descripción</label>
            <input
              type="text"
              value={partida.descripcion}
              onChange={(e) => setPartida(prev => ({ ...prev, descripcion: e.target.value }))}
              className="border p-2 rounded w-full"
            />
          </div>
        </div>

        <div className="mt-6">
          <h2 className="text-xl font-semibold mb-4">Detalles de la Partida</h2>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-4">
            <div>
              <label className="block font-medium mb-1">Cuenta</label>
              <input
                type="text"
                value={cuentaBusqueda}
                onChange={(e) => buscarCuenta(e.target.value)}
                placeholder="Buscar o escribir cuenta"
                className="border p-2 rounded w-full"
              />
              {cuentaNoExistente && (
                <div className="text-red-500 text-sm mt-1">La cuenta no existe en el catálogo</div>
              )}
            </div>
            <div>
              <label className="block font-medium mb-1">Descripción</label>
              <input
                type="text"
                value={nuevoDetalle.descripcion}
                onChange={(e) => setNuevoDetalle(prev => ({ ...prev, descripcion: e.target.value }))}
                className="border p-2 rounded w-full"
                disabled
              />
            </div>
            <div>
              <label className="block font-medium mb-1">Monto</label>
              <input
                type="text"
                value={nuevoDetalle.monto.toLocaleString("es-HN", { style: "currency", currency: "HNL" })}
                className="border p-2 rounded w-full"
                disabled
              />
            </div>
          </div>

          <button
            type="button"
            onClick={agregarDetalle}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          >
            Agregar
          </button>

          <table className="w-full border-collapse border mt-6">
            <thead>
              <tr className="bg-gray-100">
                <th className="border p-2">Cuenta</th>
                <th className="border p-2">Descripción</th>
                <th className="border p-2">Débito</th>
                <th className="border p-2">Crédito</th>
                <th className="border p-2">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {partida.detalles.map((detalle, index) => (
                <tr key={index}>
                  <td className="border p-2">{cuentas.find(c => c.id === detalle.idCatalogoCuenta)?.numeroCuenta}</td>
                  <td className="border p-2">{detalle.descripcion}</td>
                  <td className="border p-2">{detalle.tipoCuenta === "Gasto" ? detalle.monto.toLocaleString("es-HN", { style: "currency", currency: "HNL" }) : ""}</td>
                  <td className="border p-2">{detalle.tipoCuenta === "Ingreso" ? detalle.monto.toLocaleString("es-HN", { style: "currency", currency: "HNL" }) : ""}</td>
                  <td className="border p-2">
                    <button
                      type="button"
                      onClick={() => eliminarDetalle(index)}
                      className="text-red-500 hover:text-red-700"
                    >
                      Eliminar
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          {partida.detalles.length > 0 && (
            <div className="mt-4 text-right">
              <strong>Total: {totalDetalles.toLocaleString("es-HN", { style: "currency", currency: "HNL" })}</strong>
            </div>
          )}
        </div>

        <button
          type="submit"
          disabled={isLoading}
          className="bg-green-500 text-white px-6 py-3 rounded w-full"
        >
          {isLoading ? "Creando..." : "Crear Partida"}
          {loadingCuentas ? "Cargando..." : "Se cargo Cuentas"}
        </button>
      </form>
    </div>
  );
};
