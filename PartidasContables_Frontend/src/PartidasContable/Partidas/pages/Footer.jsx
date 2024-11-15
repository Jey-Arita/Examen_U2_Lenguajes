export const Footer = ()  => {
    return (
      <footer className="bg-gray-900 text-white">
        <div className="max-w-4xl mx-auto px-4 py-12">
          {/* Logo y descripción */}
          <div className="text-center mb-8">
            <h2 className="text-3xl font-bold mb-4">Sistema de Partidas Contables</h2>
          </div>
  
          {/* Línea divisoria */}
          <hr className="my-8 border-gray-800" />
  
          {/* Derechos de autor */}
          <div className="text-center text-gray-400">
            <p>&copy; {new Date().getFullYear()} Sistema de Partidas Contables. Todos los derechos reservados.</p>
          </div>
        </div>
      </footer>
    )
  }