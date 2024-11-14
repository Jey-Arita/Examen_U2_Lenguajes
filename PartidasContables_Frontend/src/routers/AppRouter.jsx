import { Navigate, Route, Routes } from "react-router-dom"
import { SecurityRouter } from "../PartidasContable/Security/routes"
import { PartidasContableRouter } from "../PartidasContable/Partidas/routes"

export const AppRouter = () => {
    return(
        <Routes>
             <Route path="/*" element={<Navigate to="/security" />} /> 
            <Route path="/security/*" element={<SecurityRouter/>}/>
            <Route path="/menu/*" element={<PartidasContableRouter/>}/>
        </Routes>
    )
}