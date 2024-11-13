import {Route, Routes } from "react-router-dom";
import { LoginPage } from "../pages";

export const SecurityRouter = () => {
  return (
          <Routes>
              <Route path="/" element={<LoginPage />} />
          </Routes>
  );
};
