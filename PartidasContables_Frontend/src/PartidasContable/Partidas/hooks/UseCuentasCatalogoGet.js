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
        const response = await getCatalogoCuentas();
        console.log("Respuesta completa:", response);
        if (response?.data?.$values && Array.isArray(response.data.$values)) {
          setCuentas(response.data.$values);
        } else {
          setError("No se encontraron cuentas en la respuesta.");
        }
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