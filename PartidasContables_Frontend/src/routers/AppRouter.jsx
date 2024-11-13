import { Navigate, Route, Routes } from "react-router-dom"
import { SecurityRouter } from "../PartidasContable/Security/routes"
import { PartidasContableRouter } from "../PartidasContable/Partidas/routes/PartidasContableRoute"


export const AppRouter = () => {
    return(
        <Routes>
             <Route path="/*" element={<Navigate to="/security" />} /> 
            <Route path="/security/*" element={<SecurityRouter/>}/>
            <Route path="/inicio/*" element={<PartidasContableRouter/>}/>
        </Routes>
    )
}