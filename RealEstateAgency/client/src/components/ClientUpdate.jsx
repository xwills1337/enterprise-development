import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../api/api';

const ClientUpdate = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [fullName, setFullName] = useState('');
    const [passport, setPassport] = useState('');
    const [phone, setPhone] = useState('');
    const [address, setAddress] = useState('');
    const [message, setMessage] = useState('');
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchClient = async () => {
            try {
                const response = await api.get(`/client/${id}`);
                setFullName(response.data.fullName);
                setPassport(response.data.passport);
                setPhone(response.data.phone);
                setAddress(response.data.address);
            } catch (error) {
                console.error('Error fetching client:', error);
                setError('Client not found');
            }
        };

        fetchClient();
    }, [id]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await api.put(`/client/${id}`, { fullName, passport, phone, address });
            setMessage('Client updated successfully!');
            setTimeout(() => {
                navigate('/');
            }, 1000);
        } catch (error) {
            console.error('Error updating client:', error);
            setMessage(`Failed to update client: ${error.response?.data || error.message}`);
        }
    };

    return (
        <div className="container mt-5">
            <h2>Update Client</h2>
            {message && (
                <div className={`alert ${message.includes('Failed') ? 'alert-danger' : 'alert-success'}`} role="alert">
                    {message}
                </div>
            )}
            {error && (
                <div className="alert alert-danger" role="alert">
                    {error}
                </div>
            )}
            {!error && (
                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label htmlFor="fullName" className="form-label">Full Name:</label>
                        <input
                            type="text"
                            className="form-control"
                            id="fullName"
                            value={fullName}
                            onChange={(e) => setFullName(e.target.value)}
                            required
                            maxLength={100}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="passport" className="form-label">Passport:</label>
                        <input
                            type="text"
                            className="form-control"
                            id="passport"
                            value={passport}
                            onChange={(e) => setPassport(e.target.value)}
                            required
                            maxLength={12}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="phone" className="form-label">Phone:</label>
                        <input
                            type="text"
                            className="form-control"
                            id="phone"
                            value={phone}
                            onChange={(e) => setPhone(e.target.value)}
                            required
                            maxLength={15}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="address" className="form-label">Address:</label>
                        <input
                            type="text"
                            className="form-control"
                            id="address"
                            value={address}
                            onChange={(e) => setAddress(e.target.value)}
                            required
                            maxLength={100}
                        />
                    </div>
                    <button type="submit" className="btn btn-primary">Update Client</button>
                </form>
            )}
        </div>
    );
};

export default ClientUpdate;