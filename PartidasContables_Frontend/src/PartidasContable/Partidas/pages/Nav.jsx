import { Link } from "react-router-dom";

export const Nav = () => {
    return (
        <nav className="bg-gray-900 shadow-lg">
            <div className="mx-auto px-4">
                <div className="flex justify-between items-center h-16">
                    <div className="flex-shrink-0">
                        <Link to="/menu/inicio" className="text-2xl font-bold text-white">
                            Menu Partidas Contables
                        </Link>
                    </div>
                </div>
            </div>
        </nav>
    );
};

