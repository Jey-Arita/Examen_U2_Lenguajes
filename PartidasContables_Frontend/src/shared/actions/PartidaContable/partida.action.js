import { partidaApi } from '../../../Config/api';

export const postPartidaAsync = async (newPartida) => {
    try {
        const {data} = await partidaApi.post(`/partida`, newPartida);
        return data;
    } catch (error) {
        console.error({...error});
        return error?.response?.data;
    }
}

export const getPartidaAsync = async () => {
    try {
        const {data} = await partidaApi.get(`/partida`);
        return data;
    } catch (error) {
        console.error({...error});
        return error?.response?.data;
    }
}