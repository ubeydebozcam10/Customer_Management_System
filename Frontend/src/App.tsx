import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import CustomerList from './pages/CustomerList';
import CustomerForm from './pages/CustomerForm';


function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Uygulama ilk açıldığında (/) doğrudan Home sayfası gelsin */}
                <Route path="/" element={<Home />} />

                {/* /list adresine gidildiğinde veya butona basıldığında müşteri tablosu gelsin */}
                <Route path="/list" element={<CustomerList />} />

                {<Route path="/add" element={<CustomerForm />} />}

                {<Route path='/form' element={<CustomerForm />} />}
            </Routes>
        </BrowserRouter>
    );
}

export default App;