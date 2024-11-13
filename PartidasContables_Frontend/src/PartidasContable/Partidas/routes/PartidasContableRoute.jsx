import { Route, Routes } from "react-router-dom";
import { Partidas } from "../pages";

export const PartidasContableRouter = () => {
    return (
      <div className="overflow-x-hidden bg-white w-screen h-screen bg-hero-pattern bg-no-repeat bg-cover">
        <div className="px-6 py-8">
          <div className="container flex justify-between mx-auto">
            <Routes>
                <Route path="/inicio" element={<Partidas />} />
            </Routes>
          </div>
        </div>
      </div>
    );
  };