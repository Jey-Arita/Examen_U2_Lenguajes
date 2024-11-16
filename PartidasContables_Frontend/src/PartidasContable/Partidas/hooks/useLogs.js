import { useEffect, useState } from "react";
import { getLogsAsync } from "../../../shared/actions/Logs";


export const useLogsGet = () => {
    const [Logs, setLogs] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState("");
  
    useEffect(() => {
      const fetchLogs = async () => {
        setIsLoading(true);
        try {
          const result = await getLogsAsync();
          if (result?.data && Array.isArray(result.data)) {
            setLogs(result.data);
          } else {
            setError("No se encontraron los logs en la respuesta.");
          }
        } catch (err) {
          setError("Error al cargar los log");
        } finally {
          setIsLoading(false);
        }
      };
  
      fetchLogs();
    }, []);
  
    return { Logs: Logs, isLoading, error };
  };