import { useState } from "react";
import { Link } from "react-router-dom";
import { CiMenuBurger, CiCircleRemove } from "react-icons/ci";

export const Nav = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    const toggleMenu = () => setIsOpen(!isOpen);
    const toggleDropdown = () => setIsDropdownOpen(!isDropdownOpen);

    return (
        <nav className="bg-gray-900 shadow-lg">
            <div className="max-w-6xl mx-auto px-4">
                <div className="flex justify-between items-center h-16">
                    <div className="flex-shrink-0">
                        <a href="/" className="text-2xl font-bold text-white">
                            Sistema de Partidas Contables
                        </a>
                    </div>

                    {/* Desktop Menu */}
                    <div className="hidden md:flex items-center space-x-8">
                        <Link to="/menu/crear-partidas" className="text-white hover:text-blue-600 flex items-center">
                                Partida Contable
                        </Link>
                        <Link to="/menu/ver-partida" className="text-white hover:text-blue-600">
                            Ver Partida Contable
                        </Link>
                        <Link to="/menu/catalogo-cuenta" className="text-white hover:text-blue-600">
                            Catálogo Cuenta
                        </Link>
                        <Link to="/menu/logs" className="text-white hover:text-blue-600">
                            Registro Logs
                        </Link>
                    </div>

                    {/* Mobile Menu Button */}
                    <div className="md:hidden flex items-center">
                        <button
                            onClick={toggleMenu}
                            className="inline-flex items-center justify-center p-2 rounded-md text-white hover:text-blue-600 focus:outline-none"
                        >
                            {isOpen ? <CiCircleRemove className="h-6 w-6" /> : <CiMenuBurger className="h-6 w-6" />}
                        </button>
                    </div>
                </div>

                {/* Mobile Menu */}
                {isOpen && (
                    <div className="md:hidden">
                        <div className="px-2 pt-2 pb-3 space-y-1">
                            <Link to="/" className="block px-3 py-2 rounded-md text-gray-700 hover:bg-blue-50">
                                Inicio
                            </Link>
                            <Link to="/menu/crear-partidas" className="block px-3 py-2 rounded-md text-gray-700 hover:bg-blue-50">
                                Partida Contable
                            </Link>
                            <Link to="/menu/catalogo-cuenta" className="block px-3 py-2 rounded-md text-gray-700 hover:bg-blue-50">
                                Catálogo Cuenta
                            </Link>
                            <Link to="/menu/logs" className="block px-3 py-2 rounded-md text-gray-700 hover:bg-blue-50">
                                Registro Logs
                            </Link>
                        </div>
                    </div>
                )}
            </div>
        </nav>
    );
};

