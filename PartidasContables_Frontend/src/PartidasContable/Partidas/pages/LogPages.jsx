import { useEffect, useState } from "react"

export const LogPages = () => {
  const [logs, setLogs] = useState([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState(null)
  
  useEffect(() => {
    const fetchLogs = async () => {
      try {
        // Simulación de llamada API
        await new Promise(resolve => setTimeout(resolve, 1000))
        setLogs(sampleLogs)
        setIsLoading(false)
      } catch (error) {
        console.error('Error fetching logs:', error)
        setError('Failed to fetch logs. Please try again later.')
        setIsLoading(false)
      }
    }
    
    fetchLogs()
  }, [])

  const handleViewPartida = (idPartida) => {
    router.push(`/partida/${idPartida}`)
  }

  if (isLoading) {
    return (
      <div className="max-w-6xl mx-auto p-6">
        <h2 className="text-2xl font-bold mb-6 text-gray-800">Log Viewer</h2>
        {[...Array(5)].map((_, i) => (
          <div key={i} className="h-16 bg-gray-100 rounded-lg mb-3 animate-pulse" />
        ))}
      </div>
    )
  }

  if (error) {
    return (
      <div className="max-w-6xl mx-auto p-6">
        <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg">
          <strong className="font-bold">Error:</strong> {error}
        </div>
      </div>
    )
  }

  return (
    <div className="max-w-6xl mx-auto p-6">
      <h2 className="text-2xl font-bold mb-6 text-gray-800">Log Viewer</h2>
      
      <div className="overflow-x-auto bg-white rounded-lg shadow">
        <table className="min-w-full table-auto">
          <thead>
            <tr className="bg-gray-50 border-b border-gray-200">
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Fecha
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                ID Usuario
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Acción
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Ver Partida
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-200">
            {logs.map((log) => (
              <tr key={log.id} className="hover:bg-gray-50 transition-colors">
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                  {new Date(log.fecha).toLocaleString()}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                  {log.idUsuario}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm">
                  <span className={`inline-flex px-2 py-1 rounded-full text-xs font-semibold ${
                    log.accion.includes('Crear') ? 'bg-green-100 text-green-800' :
                    log.accion.includes('Modificar') ? 'bg-blue-100 text-blue-800' :
                    'bg-red-100 text-red-800'
                  }`}>
                    {log.accion}
                  </span>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm">
                  {log.idPartida ? (
                    <button
                      onClick={() => handleViewPartida(log.idPartida)}
                      className="bg-white border border-gray-300 rounded-md px-3 py-1.5 text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 transition-colors"
                    >
                      Ver Detalles
                    </button>
                  ) : (
                    <span className="text-gray-400">N/A</span>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}

const sampleLogs = [
  { id: '1', fecha: '2023-06-01T10:30:00Z', idUsuario: 'user123', accion: 'Crear Partida', idPartida: 'abc123' },
  { id: '2', fecha: '2023-06-02T14:45:00Z', idUsuario: 'user456', accion: 'Modificar Partida', idPartida: 'def456' },
  { id: '3', fecha: '2023-06-03T09:15:00Z', idUsuario: 'user789', accion: 'Eliminar Partida', idPartida: null },
  { id: '4', fecha: '2023-06-04T16:20:00Z', idUsuario: 'user123', accion: 'Crear Partida', idPartida: 'ghi789' },
  { id: '5', fecha: '2023-06-05T11:00:00Z', idUsuario: 'user456', accion: 'Modificar Partida', idPartida: 'jkl012' },
]