import { partidaApi } from '../../../Config/api';

export const loginAsync = async (form) => {
    try {
        const {data} = await partidaApi.post('/partida');
        return data;
    } catch (error) {
        console.error({...error});
        return error?.response?.data;
    }
}