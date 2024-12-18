import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../api/api';
import ClientDelete from './ClientDelete';

const ClientList = () => {
    const [clients, setClients] = useState([]);
    const [message, setMessage] = useState('');

    const handleDeleteSuccess = (id) => {
        setClients(clients.filter((client) => client.id !== id));
    };

    useEffect(() => {
        const fetchClients = async () => {
            try {
                const response = await api.get('/client');
                setClients(response.data);
            } catch (error) {
                console.error('Error fetching clients:', error);
                setMessage('Failed to fetch clients.');
                setTimeout(() => setMessage(''), 3000);
            }
        };
        fetchClients();
    }, []);

    return (
        <div className="container mt-5">
            <h2>Client List</h2>
            {message && (
                <div className={`alert ${message.includes('Failed') ? 'alert-danger' : 'alert-success'}`} role="alert">
                    {message}
                </div>
            )}
            <Link to="/client/create" className="btn btn-success mb-3">Create New Client</Link>
            <ul className="list-group">
                {clients.length > 0 ? (
                    clients.map((client) => (
                        <li key={client.id} className="list-group-item d-flex justify-content-between align-items-center">
                            <div>
                                <p><strong>Full Name:</strong> {client.fullName}</p>
                                <p><strong>Passport:</strong> {client.passport}</p>
                                <p><strong>Phone:</strong> {client.phone}</p>
                                <p><strong>Address:</strong> {client.address}</p>
                            </div>
                            <div>
                                <Link to={`/client/update/${client.id}`} className="btn btn-warning btn-sm me-2">Update</Link>
                                <ClientDelete id={client.id} onDeleteSuccess={handleDeleteSuccess} />
                            </div>
                        </li>
                    ))
                ) : (
                    <li className="list-group-item">No clients found</li>
                )}
            </ul>
        </div>
    );
}

export default ClientList;