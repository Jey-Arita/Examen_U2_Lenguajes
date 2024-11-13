import { Route, Routes } from "react-router-dom"
import { SecurityRouter } from "../Partidas_Contable/Security/routes"


export const AppRouter = () => {
    return(
        <Routes>
            <Route path="*" element={<SecurityRouter/>}/>
        </Routes>
    )
}