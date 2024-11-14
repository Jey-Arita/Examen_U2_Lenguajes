import { partidaApi } from "../../../Config/api";


export const getCatalogoCuentas = async () => {
    try {
      const {data} = await partidaApi.get("catalogo-cuentas");
        return data;      
    } catch (error) {
        console.log(error);
        throw error;
    }
 };


 export const postCatalogoCuentas = async () => {
    try {
      const {data} = await partidaApi.post("catalogo-cuentas");
        return data;      
    } catch (error) {
        console.log(error);
        throw error;
    }
 };