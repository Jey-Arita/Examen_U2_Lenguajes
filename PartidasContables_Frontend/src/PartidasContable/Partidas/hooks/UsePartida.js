import { useState, useCallback } from "react";
import { getPartidaAsync, postPartidaAsync } from "../../../shared/actions/PartidaContable/partida.action";

export const usePartida = () => {
  const [partidas, setPartidas] = useState([]); // Lista de partidas
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  // Función para cargar la lista de partidas
  const loadPartidas = useCallback(async () => {
    setIsLoading(true);
    setError(null);
    try {
      const result = await getPartidaAsync();
      setPartidas(result.data);
    } catch (err) {
      setError("Error al cargar las partidas");
    } finally {
      setIsLoading(false);
    }
  }, []);

  // Función para crear una nueva partida
  const createPartida = async (newPartida) => {
    setIsLoading(true);
    setError(null);
    try {
      const result = await postPartidaAsync(newPartida);
      if (result.status === 201) {
        setPartidas((prevPartidas) => [...prevPartidas, result.data]);
      } else {
        setError(result.message || "Error al crear la partida");
      }
      return result;
    } catch (err) {
      setError("Error de red");
    } finally {
      setIsLoading(false);
    }
  };

  return {
    partidas,
    isLoading,
    error,
    loadPartidas,
    createPartida,
  };
};

