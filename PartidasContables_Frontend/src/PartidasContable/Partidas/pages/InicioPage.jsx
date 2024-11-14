import { Link } from "react-router-dom";

export const InicioPage = () => {
  return (
    <div className="min-h-screen bg-gradient-to-r from-blue-500 to-indigo-600 flex items-center justify-center px-4 py-12">
      <div className="w-full max-w-3xl bg-white rounded-xl shadow-xl p-8">
        <h1 className="text-3xl font-bold text-center text-gray-800 mb-4">
          Bienvenido a Tu Aplicación
        </h1>
        <p className="text-center text-gray-600 mb-6">
          Selecciona una opción para comenzar
        </p>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-2 gap-6">
          <Link to="/catalogo-cuenta">
            <button className="w-full h-32 flex flex-col items-center justify-center gap-2 bg-blue-100 hover:bg-blue-200 text-blue-800 font-semibold rounded-lg shadow-md transition duration-200 transform hover:scale-105">
              <span className="text-lg">Catálogo de Cuentas</span>
            </button>
          </Link>
          <Link to="/crear-partidas">
            <button className="w-full h-32 flex flex-col items-center justify-center gap-2 bg-green-100 hover:bg-green-200 text-green-800 font-semibold rounded-lg shadow-md transition duration-200 transform hover:scale-105">
              <span className="text-lg">Crear Partida</span>
            </button>
          </Link>
        </div>
      </div>
    </div>
  );
};
