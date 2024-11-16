import { useState, useCallback, useEffect } from "react";
import { getPartidaAsync, postPartidaAsync } from "../../../shared/actions/PartidaContable/partida.action";


// Obtener la lista de partidas
export const usePartidaGet = () => {
  const [partidas, setPartidas] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchPartidas = async () => {
      setIsLoading(true);
      try {
        const result = await getPartidaAsync();
        if (result?.data && Array.isArray(result.data)) {
          setPartidas(result.data);
        } else {
          setError("No se encontraron partidas en la respuesta.");
        }
      } catch (err) {
        setError("Error al cargar las partidas");
      } finally {
        setIsLoading(false);
      }
    };

    fetchPartidas();
  }, []);

  return { partidas, isLoading, error };
};

// Crear una nueva partida

export const useCrearPartida = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);


  const crearPartida = async (newPartida) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      const result = await postPartidaAsync(newPartida);
      if (result.status === 201) {
        setSuccess(true);
      } else {
        setError(result.message || "Error al crear la partida 2");
      }
    } catch (err) {
      setError(err.message || 'Error al crear la partida');
    } finally {
      setLoading(false);
    }
  };

  return { crearPartida, loading, error, success };
};