
import GlobalRouter from './routes'
import { Toaster } from 'react-hot-toast'
import 'semantic-ui-css/semantic.min.css'
const App = () => {
  return (
    <div>
      <GlobalRouter/>
      <Toaster/>
    </div>
  )
}

export default App