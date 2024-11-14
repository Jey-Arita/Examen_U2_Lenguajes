import { partidaApi } from "../../../Config/api";

export const getCatalogoCuentas = async () => {
    try {
      const {data} = await partidaApi.get("/catalogo-cuentas");
        return data;      
    } catch (error) {
        console.log(error);
        throw error;
    }
 };

 export const postCatalogoCuentas = async (newCatalogo) => {
    try {
      const {data} = await partidaApi.post("/catalogo-cuentas", newCatalogo);
        return data;      
    } catch (error) {
        console.error('Error en la solicitud:', error.response.data);
        console.log(error);
        throw error;
    }
 };

 export const editCatalogoCuentas = async (id, actualizar) => {
    try {
      const {data} = await partidaApi.put(`/catalogo-cuentas/${id}`, actualizar);
        return data;      
    } catch (error) {
        console.log(error);
        throw error;
    }
 };

 export const deleteCatalogoCuentas = async (id) => {
    try {
      const {data} = await partidaApi.delete(`/catalogo-cuentas/${id}`);
        return data;      
    } catch (error) {
        console.log(error);
        throw error;
    }
 };