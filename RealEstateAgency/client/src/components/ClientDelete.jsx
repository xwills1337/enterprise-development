import React, { useState } from 'react';
import api from '../api/api';

function ClientDelete({ id, onDeleteSuccess }) {
    const [message, setMessage] = useState('');

    const handleDelete = async () => {
        try {
            await api.delete(`/client/${id}`);
            onDeleteSuccess(id);
            setMessage('Client deleted successfully!');
        } catch (error) {
            console.error('Error deleting client:', error);
            setMessage('Failed to delete client');
        }
    };

    return (
        <div>
            {message && (
                <div className={`alert ${message.includes('Failed') ? 'alert-danger' : 'alert-success'}`} role="alert">
                    {message}
                </div>
            )}
            <button onClick={handleDelete} className="btn btn-danger btn-sm">
                Delete
            </button>
        </div>
    );
}

export default ClientDelete;