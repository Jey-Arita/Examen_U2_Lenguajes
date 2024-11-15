import { Navigate, Route, Routes } from "react-router-dom";
import { CatalogoCuenta, CrearPartidas, Footer, InicioPage, LogPages} from "../pages";

export const PartidasContableRouter = () => {
    return (
      <div className="flex flex-col min-h-screen bg-white w-screen overflow-x-hidden bg-hero-pattern bg-no-repeat bg-cover">
        <div className="flex-grow">
            <Routes>
                <Route path="/catalogo-cuenta" element={<CatalogoCuenta />} />
                <Route path="/crear-partidas" element={<CrearPartidas/>}/>
                <Route path="/inicio" element={<InicioPage/>}/>
                <Route path="/logs" element={<LogPages/>}/>
            </Routes>
            </div>
            <footer className="mt-auto">
              <Footer/>
            </footer>
          </div>
    );
  };