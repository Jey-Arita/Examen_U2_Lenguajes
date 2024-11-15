import { Navigate, Route, Routes } from "react-router-dom";
import { CatalogoCuenta, CrearPartidas, InicioPage, LogPages} from "../pages";

export const PartidasContableRouter = () => {
    return (
      <div className="overflow-x-hidden bg-white w-screen h-screen bg-hero-pattern bg-no-repeat bg-cover">
            <Routes>
                <Route path="/catalogo-cuenta" element={<CatalogoCuenta />} />
                <Route path="/crear-partidas" element={<CrearPartidas/>}/>
                <Route path="/inicio" element={<InicioPage/>}/>
                <Route path="/logs" element={<LogPages/>}/>
            </Routes>
          </div>
    
    );
  };