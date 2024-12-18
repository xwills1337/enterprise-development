import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import ClientCreate from './components/ClientCreate';
import ClientDelete from './components/ClientDelete';
import ClientList from './components/ClientList';
import ClientUpdate from './components/ClientUpdate';


function App() {
    return (
        <Router>
            <div>
                <h1 className="text-center">Client</h1>
                <Routes>
                    <Route path="/" element={<ClientList />} />
                    <Route path="/client/create" element={<ClientCreate />} />
                    <Route path="/client/update/:id" element={<ClientUpdate />} />
                    <Route path="/client/delete/:id" element={<ClientDelete />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
