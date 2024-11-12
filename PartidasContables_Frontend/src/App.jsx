import { BrowserRouter } from 'react-router-dom'
import { AppRouter } from './routers/AppRouter'

export const App = () => {
return(
  <BrowserRouter>
    <AppRouter/>
  </BrowserRouter>
  )
};

