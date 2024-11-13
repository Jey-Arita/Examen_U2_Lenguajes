import { useState } from "react";
import { RxSwitch } from "react-icons/rx";
import { motion } from "framer-motion";

export const Partidas = () => {
  const [entries, setEntries] = useState([
    {
      id: 1,
      code: "1001",
      description: "Caja",
      balance: 5000,
      type: "Activo",
      allowsMovement: true,
    },
    {
      id: 2,
      code: "2001",
      description: "Cuentas por pagar",
      balance: 6000,
      type: "Pasivo",
      allowsMovement: true,
    },
    {
      id: 3,
      code: "3001001",
      description: "Capital social",
      balance: 50000,
      type: "Capital",
      allowsMovement: false,
    },
  ]);

  const [newEntry, setNewEntry] = useState({
    code: "",
    description: "",
    balance: 0,
    type: "Activo",
    allowsMovement: true,
  });

  const [editingId, setEditingId] = useState(null);

  const addEntry = () => {
    setEntries([...entries, { ...newEntry, id: Date.now() }]);
    setNewEntry({
      code: "",
      description: "",
      balance: 0,
      type: "Activo",
      allowsMovement: true,
    });
  };

  const startEditing = (id) => setEditingId(id);
  const saveEdit = () => setEditingId(null);

  const updateEntry = (id, field, value) => {
    setEntries(
      entries.map((entry) =>
        entry.id === id ? { ...entry, [field]: value } : entry
      )
    );
  };

  return (
    <div className="container mx-auto py-10">
      <h1 className="text-2xl font-bold mb-4">Tabla de Partidas Contables</h1>

      <div className="grid grid-cols-2 gap-4 mb-4">
        <input
          type="text"
          placeholder="Código de cuenta"
          value={newEntry.code}
          onChange={(e) => setNewEntry({ ...newEntry, code: e.target.value })}
          className="border px-2 py-1 rounded"
        />
        <input
          type="text"
          placeholder="Descripción"
          value={newEntry.description}
          onChange={(e) =>
            setNewEntry({ ...newEntry, description: e.target.value })
          }
          className="border px-2 py-1 rounded"
        />
        <input
          type="number"
          placeholder="Saldo"
          value={newEntry.balance}
          onChange={(e) =>
            setNewEntry({ ...newEntry, balance: parseFloat(e.target.value) })
          }
          className="border px-2 py-1 rounded"
        />
        <select
          value={newEntry.type}
          onChange={(e) => setNewEntry({ ...newEntry, type: e.target.value })}
          className="border px-2 py-1 rounded"
        >
          <option value="Activo">Activo</option>
          <option value="Pasivo">Pasivo</option>
          <option value="Capital">Capital</option>
        </select>
        <div className="flex items-center space-x-2">
          <motion.div
            animate={{ rotate: newEntry.allowsMovement ? 0 : 180 }}
            transition={{ type: "spring", stiffness: 300 }}
          >
            <RxSwitch
              size={24}
              onClick={() =>
                setNewEntry({
                  ...newEntry,
                  allowsMovement: !newEntry.allowsMovement,
                })
              }
              className={`cursor-pointer ${
                newEntry.allowsMovement ? "text-green-500" : "text-red-500"
              }`}
            />
          </motion.div>
          <label htmlFor="allows-movement" className="ml-2">
            Permite movimiento
          </label>
        </div>
        <button
          onClick={addEntry}
          className="px-4 py-2 bg-blue-500 text-white rounded"
        >
          Agregar entrada
        </button>
      </div>

      <table className="table-auto w-full border">
        <thead className="bg-gray-300">
          <tr>
            <th className="px-4 py-2 border">Código de Cuenta</th>
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
                    value={entry.code}
                    onChange={(e) =>
                      updateEntry(entry.id, "code", e.target.value)
                    }
                    className="border px-2 py-1 rounded"
                  />
                ) : (
                  entry.code
                )}
              </td>
              <td className="px-4 py-2 border">
                {editingId === entry.id ? (
                  <input
                    type="text"
                    value={entry.description}
                    onChange={(e) =>
                      updateEntry(entry.id, "description", e.target.value)
                    }
                    className="border px-2 py-1 rounded"
                  />
                ) : (
                  entry.description
                )}
              </td>
              <td className="px-4 py-2 border">
                {editingId === entry.id ? (
                  <input
                    type="number"
                    value={entry.balance}
                    onChange={(e) =>
                      updateEntry(
                        entry.id,
                        "balance",
                        parseFloat(e.target.value)
                      )
                    }
                    className="border px-2 py-1 rounded"
                  />
                ) : (
                  entry.balance.toLocaleString("es-HN", {
                    style: "currency",
                    currency: "HNL",
                  })
                )}
              </td>
              <td className="px-4 py-2 border">
                {editingId === entry.id ? (
                  <select
                    value={entry.type}
                    onChange={(e) =>
                      updateEntry(entry.id, "type", e.target.value)
                    }
                    className="border px-2 py-1 rounded"
                  >
                    <option value="Activo">Activo</option>
                    <option value="Pasivo">Pasivo</option>
                    <option value="Capital">Capital</option>
                  </select>
                ) : (
                  entry.type
                )}
              </td>
              <td className="px-4 py-2 border">
                {editingId === entry.id ? (
                  <input
                    type="checkbox"
                    checked={entry.allowsMovement}
                    onChange={(e) =>
                      updateEntry(entry.id, "allowsMovement", e.target.checked)
                    }
                  />
                ) : entry.allowsMovement ? (
                  "Sí"
                ) : (
                  "No"
                )}
              </td>
              <td className="px-4 py-2 border">
                {editingId === entry.id ? (
                  <button
                    onClick={() => saveEdit(entry.id)}
                    className="px-4 py-2 bg-green-500 text-white rounded"
                  >
                    Guardar
                  </button>
                ) : (
                  <button
                    onClick={() => startEditing(entry.id)}
                    className="px-4 py-2 bg-yellow-500 text-white rounded"
                  >
                    Modificar
                  </button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};
