import { useState } from "react";
import { useCatalogoGet, useCrearPartida } from "../hooks";

export const CrearPartidas = () => {
  const { crearPartida, isLoading, error: apiError } = useCrearPartida();
  const {
    cuentas,
    isLoading: loadingCuentas,
    error: errorCuentas,
  } = useCatalogoGet();

  const [partida, setPartida] = useState({
    fecha: "",
    descripcion: "",
    detalles: []
  });

  const [nuevoDetalle, setNuevoDetalle] = useState({
    idCatalogoCuenta: "",
    descripcion: "",
    monto: 0,
    tipoMovimiento: "",
  });

  const [cuentaSeleccionada, setCuentaSeleccionada] = useState(null); // Para mostrar datos de la cuenta seleccionada
  const [formError, setFormError] = useState("");
  const [success, setSuccess] = useState("");
  const [cuentaBusqueda, setCuentaBusqueda] = useState("");
  const [cuentaNoExistente, setCuentaNoExistente] = useState(false);

  const buscarCuenta = (input) => {
    setCuentaBusqueda(input);
    const cuentaEncontrada = cuentas.find(
      (cuenta) =>
        cuenta.numeroCuenta.includes(input) ||
        cuenta.descripcion.toLowerCase().includes(input.toLowerCase())
    );

    if (cuentaEncontrada) {
      setNuevoDetalle((prev) => ({
        ...prev,
        idCatalogoCuenta: cuentaEncontrada.id,
      }));
      setCuentaSeleccionada(cuentaEncontrada);
      setCuentaNoExistente(false);
    } else {
      setCuentaNoExistente(true);
      setCuentaSeleccionada(null);
    }
  };

  const agregarDetalle = () => {
    if (!nuevoDetalle.idCatalogoCuenta || !nuevoDetalle.tipoMovimiento || nuevoDetalle.monto <= 0) {
      setFormError("Todos los campos de detalle son obligatorios");
      return;
    }
    console.log("Detalle a agregar:", nuevoDetalle);

    setPartida((prev) => ({
      ...prev,
      detalles: [...prev.detalles, nuevoDetalle],
    }));

    setNuevoDetalle({
      idCatalogoCuenta: "",
      descripcion: "",
      monto: 0,
      tipoMovimiento: "",
    });
    setCuentaSeleccionada(null);
    setCuentaBusqueda("");
  };

  const eliminarDetalle = (index) => {
    setPartida((prev) => ({
      ...prev,
      detalles: prev.detalles.filter((_, i) => i !== index),
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setFormError("");
    setSuccess("");
  
    // Validar formulario
    if (!partida.fecha || !partida.descripcion || partida.detalles.length === 0) {
      setFormError("Todos los campos son requeridos y debe haber al menos un detalle");
      return;
    }
    console.log("Partida a enviar:", partida);
  
    try {
      // Llamada a la API para crear partida
      const response = await crearPartida(partida);
  
      // Verificar el resultado de la respuesta
      if (response?.status === 201) {
        setSuccess("Partida creada exitosamente");
        setPartida({ fecha: "", descripcion: "", detalles: [] }); // Reiniciar formulario
        setPartida((prev) => ({
          ...prev,
          detalles: [],
        }));
      } else {
        setFormError(response?.message || "Error al crear la partida 2");
      }
    } catch (error) {
      console.error("Error al crear la partida:", error);
      setFormError(`Error: ${error.message}`);
    }
  };
  

  const balance = partida.detalles.reduce(
    (acc, detalle) => {
      if (detalle.tipoMovimiento === "Debe") {
        acc.debe += detalle.monto;
      } else if (detalle.tipoMovimiento === "Haber") {
        acc.haber += detalle.monto;
      }
      return acc;
    },
    { debe: 0, haber: 0 }
  );


  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-6">Crear Partida Contable</h1>

      {formError && (
        <div className="mb-4 p-2 bg-red-100 text-red-800 rounded">{formError}</div>
      )}
      {success && (
        <div className="mb-4 p-2 bg-green-100 text-green-800 rounded">{success}</div>
      )}

      <form onSubmit={handleSubmit} className="space-y-6">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block font-medium mb-1">Fecha</label>
            <input
              type="date"
              value={partida.fecha}
              onChange={(e) =>
                setPartida((prev) => ({ ...prev, fecha: e.target.value }))
              }
              className="border p-2 rounded w-full"
            />
          </div>
          <div>
            <label className="block font-medium mb-1">Descripción General</label>
            <input
              type="text"
              value={partida.descripcion}
              onChange={(e) =>
                setPartida((prev) => ({ ...prev, descripcion: e.target.value }))
              }
              className="border p-2 rounded w-full"
            />
          </div>
        </div>

        <div className="mt-6">
          <h2 className="text-xl font-semibold mb-4">Detalles de la Partida</h2>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-4">
            <div>
              <label className="block font-medium mb-1">Buscar Cuenta</label>
              <input
                type="text"
                value={cuentaBusqueda}
                onChange={(e) => buscarCuenta(e.target.value)}
                placeholder="Número o descripción de cuenta"
                className="border p-2 rounded w-full"
              />
              {cuentaNoExistente && (
                <p className="text-red-500 text-sm mt-1">Cuenta no encontrada</p>
              )}
            </div>
            <div>
              <label className="block font-medium mb-1">Descripción</label>
              <input
                type="text"
                value={nuevoDetalle.descripcion}
                onChange={(e) =>
                  setNuevoDetalle((prev) => ({
                    ...prev,
                    descripcion: e.target.value,
                  }))
                }
                placeholder="Descripción del detalle"
                className="border p-2 rounded w-full"
              />
            </div>
            <div>
              <label className="block font-medium mb-1">Monto</label>
              <input
                type="number"
                value={nuevoDetalle.monto}
                onChange={(e) =>
                  setNuevoDetalle((prev) => ({
                    ...prev,
                    monto: parseFloat(e.target.value) || 0,
                  }))
                }
                className="border p-2 rounded w-full"
              />
            </div>
            <div>
              <label className="block font-medium mb-1">Tipo de Cuenta</label>
              <select
                value={nuevoDetalle.tipoMovimiento}
                onChange={(e) =>
                  setNuevoDetalle((prev) => ({
                    ...prev,
                    tipoMovimiento: e.target.value,
                  }))
                }
                className="border p-2 rounded w-full"
              >
                <option value="">Seleccione</option>
                <option value="Debe">Debe</option>
                <option value="Haber">Haber</option>
              </select>
            </div>
          </div>

          {cuentaSeleccionada && (
            <div className="p-4 border rounded bg-gray-50 mt-4">
              <h3 className="font-semibold mb-2">Detalles de la Cuenta Seleccionada</h3>
              <p><strong>Número:</strong> {cuentaSeleccionada.numeroCuenta}</p>
              <p><strong>Descripción:</strong> {cuentaSeleccionada.descripcion}</p>
              <p><strong>Saldo:</strong> {cuentaSeleccionada.saldo}</p>
            </div>
          )}

          <button
            type="button"
            onClick={agregarDetalle}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 mt-4"
          >
            Agregar Detalle
          </button>
        </div>

        <table className="w-full mt-6 border">
          <thead>
            <tr className="bg-gray-100">
              <th className="p-2 border">Cuenta</th>
              <th className="p-2 border">Descripción</th>
              <th className="p-2 border">Monto</th>
              <th className="p-2 border">Tipo</th>
              <th className="p-2 border">Acciones</th>
            </tr>
          </thead>
          <tbody>
            {partida.detalles.map((detalle, index) => (
              <tr key={index}>
                <td className="p-2 border">{detalle.idCatalogoCuenta}</td>
                <td className="p-2 border">{detalle.descripcion}</td>
                <td className="p-2 border">{detalle.monto}</td>
                <td className="p-2 border">{detalle.tipoMovimiento}</td>
                <td className="p-2 border">
                  <button
                    onClick={() => eliminarDetalle(index)}
                    className="bg-red-500 text-white px-2 py-1 rounded hover:bg-red-600"
                  >
                    Eliminar
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        <div className="mt-4">
          <p className="text-gray-700">
            <strong>Total Debe:</strong> {balance.debe.toFixed(2)} |{" "}
            <strong>Total Haber:</strong> {balance.haber.toFixed(2)}
          </p>
          <p
            className={`font-semibold ${
              balance.debe === balance.haber ? "text-green-600" : "text-red-600"
            }`}
          >
            {balance.debe === balance.haber
              ? "El balance es correcto"
              : "El balance no cuadra"}
          </p>
        </div>

        <button
          type="submit"
          disabled={isLoading || balance.debe !== balance.haber}
          className="bg-green-500 text-white px-6 py-2 rounded hover:bg-green-600 mt-6"
        >
          {isLoading ? "Guardando..." : "Crear Partida"}
        </button>
      </form>
    </div>
  );
};
