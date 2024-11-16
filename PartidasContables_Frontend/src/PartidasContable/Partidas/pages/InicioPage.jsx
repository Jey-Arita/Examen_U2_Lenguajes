import React from "react";
import { Link } from "react-router-dom";
import { FaBookOpen } from "react-icons/fa";
import { FiFileText } from "react-icons/fi";
import { FaHistory } from "react-icons/fa";
import { FaFileContract } from "react-icons/fa";

export const InicioPage = () => {
  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-200 via-white to-gray-200 flex items-center justify-center p-4">
      <div className="w-full max-w-4xl bg-white/95 backdrop-blur-sm rounded-2xl shadow-2xl p-8">
        <div className="text-center mb-12">
          <h1 className="text-4xl font-bold text-gray-800 mb-3">
            Sistema Contable
          </h1>
          <p className="text-gray-600 text-lg">
            Gestiona tus registros contables de manera eficiente
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          {/* Catálogo de Cuentas */}
          <Link to="/menu/catalogo-cuenta" className="group">
            <div className="h-full bg-white rounded-xl border-2 border-blue-100 p-6 hover:border-blue-500 transition-all duration-300 shadow-lg hover:shadow-blue-100">
              <div className="flex flex-col items-center gap-4">
                <div className="p-4 bg-blue-50 rounded-full group-hover:bg-blue-100 transition-colors duration-300">
                  <FaBookOpen className="w-8 h-8 text-blue-600" />
                </div>
                <h2 className="text-xl font-semibold text-gray-800">
                  Catálogo de Cuentas
                </h2>
                <p className="text-gray-600 text-center">
                  Administra y visualiza tu plan contable
                </p>
              </div>
            </div>
          </Link>

          {/* Crear Partida */}
          <Link to="/menu/crear-partidas" className="group">
            <div className="h-full bg-white rounded-xl border-2 border-green-100 p-6 hover:border-green-500 transition-all duration-300 shadow-lg hover:shadow-green-100">
              <div className="flex flex-col items-center gap-4">
                <div className="p-4 bg-green-50 rounded-full group-hover:bg-green-100 transition-colors duration-300">
                  <FiFileText className="w-8 h-8 text-green-600" />
                </div>
                <h2 className="text-xl font-semibold text-gray-800">
                  Crear Partida
                </h2>
                <p className="text-gray-600 text-center">
                  Registra nuevos asientos contables
                </p>
              </div>
            </div>
          </Link>

          {/* Ver Partida */}
          <Link to="/menu/ver-partida" className="group">
            <div className="h-full bg-white rounded-xl border-2 border-yellow-100 p-6 hover:border-yellow-500 transition-all duration-300 shadow-lg hover:shadow-yellow-100">
              <div className="flex flex-col items-center gap-4">
                <div className="p-4 bg-yellow-50 rounded-full group-hover:bg-yellow-100 transition-colors duration-300">
                  <FaFileContract className="w-8 h-8 text-yellow-600" />
                </div>
                <h2 className="text-xl font-semibold text-gray-800">
                  Ver Partida
                </h2>
                <p className="text-gray-600 text-center">
                  Consulta tus registros contables
                </p>
              </div>
            </div>
          </Link>

          {/* Historial de Logs */}
          <Link to="/menu/logs" className="group">
            <div className="h-full bg-white rounded-xl border-2 border-purple-100 p-6 hover:border-purple-500 transition-all duration-300 shadow-lg hover:shadow-purple-100">
              <div className="flex flex-col items-center gap-4">
                <div className="p-4 bg-purple-50 rounded-full group-hover:bg-purple-100 transition-colors duration-300">
                  <FaHistory className="w-8 h-8 text-purple-600" />
                </div>
                <h2 className="text-xl font-semibold text-gray-800">
                  Historial de Logs
                </h2>
                <p className="text-gray-600 text-center">
                  Revisa el registro de actividades
                </p>
              </div>
            </div>
          </Link>
        </div>
      </div>
    </div>
  );
};
