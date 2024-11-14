import { useState, useEffect } from "react";
import { getCatalogoCuentas } from "../../../shared/actions/CatalogoCuentas";


export const useCatalogoGet = () => {
  const [cuentas, setCuentas] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchCuentas = async () => {
      setIsLoading(true);
      try {
        const data = await getCatalogoCuentas();
        setCuentas(data);
      } catch (error) {
        setError("Error al cargar el catálogo de cuentas");
      } finally {
        setIsLoading(false);
      }
    };

    fetchCuentas();
  }, []);

  return { cuentas, isLoading, error };
};