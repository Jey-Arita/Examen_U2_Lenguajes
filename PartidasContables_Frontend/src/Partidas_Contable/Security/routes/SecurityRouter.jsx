import { Navigate, Route, Routes } from "react-router-dom"
import { LoginPage } from "../pages"


export const SecurityRouter = () => {
    return(
        <Routes>
            <Route path='/login' element={<LoginPage/>}/>
            <Route path='/*' element={<Navigate to={"/security/login"} />} />
        </Routes>
    )
}