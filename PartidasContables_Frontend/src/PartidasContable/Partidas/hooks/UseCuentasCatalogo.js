import { useState, useEffect } from "react";
import { deleteCatalogoCuentas, editCatalogoCuentas, getCatalogoCuentas, postCatalogoCuentas } from "../../../shared/actions/CatalogoCuentas";

// Obtener el catálogo de cuentas 
export const useCatalogoGet = () => {
  const [cuentas, setCuentas] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchCuentas = async () => {
      setIsLoading(true);
      try {
        const response = await getCatalogoCuentas();
        if (response?.data && Array.isArray(response.data)) {
          setCuentas(response.data);
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

// Hook para crear una nueva cuenta en el catálogo
export const useCrearCatalogoCuentas = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);

  const crearCatalogo = async (newCatalogo) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      await postCatalogoCuentas(newCatalogo);
      setSuccess(true);
    } catch (error) {
      setError(error.message || 'Error al crear el catálogo');
    } finally {
      setLoading(false);
    }
  };

  return { crearCatalogo, loading, error, success };
};

// Hook para editar una cuenta en el catálogo
export const useEditarCatalogoCuentas = (id) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);

  const editarCatalogo = async (id, actualizar) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      await editCatalogoCuentas(id, actualizar);
      setSuccess(true);
    } catch (err) {
      setError(err.message || 'Error al editar el catálogo');
    } finally {
      setLoading(false);
    }
  };

  return { editarCatalogo, loading, error, success };
};

// Hook para eliminar una cuenta del catálogo
export const useEliminarCatalogoCuentas = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);

  const eliminarCatalogo = async (id) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      await deleteCatalogoCuentas(id);
      setSuccess(true);
    } catch (err) {
      setError(err.message || 'Error al eliminar el catálogo');
    } finally {
      setLoading(false);
    }
  };

  return { eliminarCatalogo, loading, error, success };
};