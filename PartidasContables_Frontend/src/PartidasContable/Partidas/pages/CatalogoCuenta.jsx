import { useState, useEffect, useCallback } from "react";
import { RxSwitch } from "react-icons/rx";
import { motion } from "framer-motion";
import {
  useCatalogoGet,
  useCrearCatalogoCuentas,
  useEditarCatalogoCuentas,
  useEliminarCatalogoCuentas,
} from "../hooks";

export const CatalogoCuenta = () => {
  const [entries, setEntries] = useState([]);
  const [newEntry, setNewEntry] = useState({
    numeroCuenta: "",
    descripcion: "",
    tipoCuenta: "Activo",
    saldo: 0,
    idCuentaPadre: null,
    cuentasHijas: [],
    permiteMovimiento: true,
  });
  const [editingId, setEditingId] = useState(null);
  const [user] = useState("Nombre de Usuario");

  // Usar los hooks personalizados
  const { cuentas, isLoading, error: fetchError } = useCatalogoGet();
  const { crearCatalogo } = useCrearCatalogoCuentas();
  const { editarCatalogo } = useEditarCatalogoCuentas();
  const { eliminarCatalogo } = useEliminarCatalogoCuentas();

  // Actualizar entries cuando se cargan las cuentas
  useEffect(() => {
    if (cuentas && Array.isArray(cuentas)) {
      setEntries(cuentas);
    }
  }, [cuentas]);

  const updateEntry = useCallback((id, field, value) => {
    setEntries(prev =>
      prev.map((entry) =>
        entry.id === id ? { ...entry, [field]: value } : entry
      )
    );
  }, []);

  const addEntry = async () => {
    try {
      await crearCatalogo(newEntry);
      // Limpiar el formulario
      setNewEntry({
        numeroCuenta: "",
        descripcion: "",
        tipoCuenta: "Activo",
        saldo: 0,
        idCuentaPadre: null,
        cuentasHijas: [],
        permiteMovimiento: true,
      });
    } catch (error) {
      console.error("Error al agregar la cuenta:", error);
    }
  };

  const saveEdit = async (id) => {
    try {
      const entryToUpdate = entries.find((entry) => entry.id === id);
      if (!entryToUpdate) return;

      await editarCatalogo(id, entryToUpdate);
      setEditingId(null);
    } catch (error) {
      console.error("Error al guardar la edición:", error);
    }
  };

  const deleteEntry = async (id) => {
    try {
      await eliminarCatalogo(id);
      // Actualizar el estado local después de eliminar
      setEntries(prev => prev.filter((entry) => entry.id !== id));
    } catch (error) {
      console.error("Error al eliminar la cuenta:", error);
    }
  };

  const startEditing = useCallback((id) => {
    setEditingId(id);
  }, []);

  if (isLoading) {
    return <div className="container mx-auto py-10">Cargando...</div>;
  }

  if (fetchError) {
    return (
      <div className="container mx-auto py-10">
        Error al cargar el catálogo: {fetchError}
      </div>
    );
  }

  return (
    <div className="container mx-auto py-10">
      <h1 className="text-2xl font-bold mb-4">Catálogo de Cuentas</h1>

      <div className="flex justify-between items-center mb-4">
        <div className="text-lg font-semibold">
          Fecha: {new Date().toLocaleDateString("es-HN")}
        </div>
        <div className="text-lg font-semibold">Usuario: {user}</div>
      </div>

      <div className="grid grid-cols-2 gap-4 mb-4">
        <input
          type="text"
          placeholder="Número de cuenta"
          value={newEntry.numeroCuenta}
          onChange={(e) =>
            setNewEntry({ ...newEntry, numeroCuenta: e.target.value })
          }
          className="border px-2 py-1 rounded"
        />
        <input
          type="text"
          placeholder="Descripción"
          value={newEntry.descripcion}
          onChange={(e) =>
            setNewEntry({ ...newEntry, descripcion: e.target.value })
          }
          className="border px-2 py-1 rounded"
        />
        <input
          type="number"
          placeholder="Saldo"
          value={newEntry.saldo}
          onChange={(e) =>
            setNewEntry({ ...newEntry, saldo: parseFloat(e.target.value) || 0 })
          }
          className="border px-2 py-1 rounded"
        />
        <select
          value={newEntry.tipoCuenta}
          onChange={(e) =>
            setNewEntry({ ...newEntry, tipoCuenta: e.target.value })
          }
          className="border px-2 py-1 rounded"
        >
          <option value="Activo">Activo</option>
          <option value="Pasivo">Pasivo</option>
          <option value="Capital">Capital</option>
        </select>
        <div className="flex items-center space-x-2">
          <motion.div
            animate={{ rotate: newEntry.permiteMovimiento ? 0 : 180 }}
            transition={{ type: "spring", stiffness: 300 }}
          >
            <RxSwitch
              size={24}
              onClick={() =>
                setNewEntry({
                  ...newEntry,
                  permiteMovimiento: !newEntry.permiteMovimiento,
                })
              }
              className={`cursor-pointer ${
                newEntry.permiteMovimiento ? "text-green-500" : "text-red-500"
              }`}
            />
          </motion.div>
          <label className="ml-2">Permite movimiento</label>
        </div>
        <button
          onClick={addEntry}
          className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
        >
          Agregar entrada
        </button>
      </div>

      {entries.length === 0 ? (
        <div className="text-center py-4">No hay cuentas registradas</div>
      ) : (
        <table className="table-auto w-full border">
          <thead className="bg-gray-300">
            <tr>
              <th className="px-4 py-2 border">Número de Cuenta</th>
              <th className="px-4 py-2 border">Descripción</th>
              <th className="px-4 py-2 border">Saldo</th>
              <th className="px-4 py-2 border">Tipo de Cuenta</th>
              <th className="px-4 py-2 border">Permite Movimiento</th>
              <th className="px-4 py-2 border">Acciones</th>
            </tr>
          </thead>
          <tbody>
            {entries.map((entry) => (
              <tr key={entry.id}>
                <td className="px-4 py-2 border">
                  {editingId === entry.id ? (
                    <input
                      type="text"
                      value={entry.numeroCuenta}
                      onChange={(e) =>
                        updateEntry(entry.id, "numeroCuenta", e.target.value)
                      }
                      className="border px-2 py-1 rounded w-full"
                    />
                  ) : (
                    entry.numeroCuenta
                  )}
                </td>
                <td className="px-4 py-2 border">
                  {editingId === entry.id ? (
                    <input
                      type="text"
                      value={entry.descripcion}
                      onChange={(e) =>
                        updateEntry(entry.id, "descripcion", e.target.value)
                      }
                      className="border px-2 py-1 rounded w-full"
                    />
                  ) : (
                    entry.descripcion
                  )}
                </td>
                <td className="px-4 py-2 border">
                  {editingId === entry.id ? (
                    <input
                      type="number"
                      value={entry.saldo}
                      onChange={(e) =>
                        updateEntry(
                          entry.id,
                          "saldo",
                          parseFloat(e.target.value) || 0
                        )
                      }
                      className="border px-2 py-1 rounded w-full"
                    />
                  ) : (
                    entry.saldo.toLocaleString("es-HN", {
                      style: "currency",
                      currency: "HNL",
                    })
                  )}
                </td>
                <td className="px-4 py-2 border">
                  {editingId === entry.id ? (
                    <select
                      value={entry.tipoCuenta}
                      onChange={(e) =>
                        updateEntry(entry.id, "tipoCuenta", e.target.value)
                      }
                      className="border px-2 py-1 rounded w-full"
                    >
                      <option value="Activo">Activo</option>
                      <option value="Pasivo">Pasivo</option>
                      <option value="Capital">Capital</option>
                    </select>
                  ) : (
                    entry.tipoCuenta
                  )}
                </td>
                <td className="px-4 py-2 border text-center">
                  <RxSwitch
                    size={24}
                    className={`cursor-pointer ${
                      entry.permiteMovimiento ? "text-green-500" : "text-red-500"
                    }`}
                  />
                </td>
                <td className="px-4 py-2 border">
                  {editingId === entry.id ? (
                    <button
                      onClick={() => saveEdit(entry.id)}
                      className="px-4 py-1 bg-blue-500 text-white rounded hover:bg-blue-600"
                    >
                      Guardar
                    </button>
                  ) : (
                    <div className="flex gap-2">
                      <button
                        onClick={() => startEditing(entry.id)}
                        className="px-2 py-1 bg-yellow-400 text-white rounded hover:bg-yellow-500"
                      >
                        Editar
                      </button>
                      <button
                        onClick={() => deleteEntry(entry.id)}
                        className="px-2 py-1 bg-red-500 text-white rounded hover:bg-red-600"
                      >
                        Eliminar
                      </button>
                    </div>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};