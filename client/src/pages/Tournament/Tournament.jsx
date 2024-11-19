import { useNavigate } from 'react-router-dom';
import { useState, useEffect } from "react";
import TournamentService from '../../api/services/TournamentService';
import './Tournament.css';
import Sidebar from '../../components/Sidebar/Sidebar';

function Tournament() {
    const [tournaments, setTournaments] = useState([]);
    const [form, setForm] = useState({ id: '', name: '' });

    
    const getTournaments = () => {
        TournamentService.getTournaments()
        .then((response) => {
            console.log('Response:', response);
            setTournaments(response.data);
        })
        .catch((error) => console.error('Error fetching tournaments:', error));
    }

    useEffect(() => {
       getTournaments();
    }, []);


    const handleAddTournament = () => {
        const tournamentData = {
            tournamentName: form.name,
            teams: [],
        };

        TournamentService.addTournament(tournamentData)
        .then((data) => {
            console.log('Tournament added:', data); // Verifique os dados retornados
            setTournaments((prevTournaments) => {
                const newTournaments = [...prevTournaments, data];
                console.log('Updated tournaments list:', newTournaments); // Verifique a nova lista
                return newTournaments;
            });
            setForm({ id: data.id || '', name: '' }); // Atribui o ID corretamente
            getTournaments();
        })
        .catch((error) => {
             console.error('Error creating tournament:', error);
        });
    };

    const handleUpdateTournament = () => {
        TournamentService.updateTournament(form)
            .then((data) => {
                setTournaments((prevTournaments) =>
                    prevTournaments.map((tournament) =>
                        tournament.id === form.id
                            ? { ...tournament, tournamentName: form.name }
                            : tournament
                    )
                );
                setForm({ id: '', name: '' });
            })
            .catch((error) => {
                console.error("Error updating tournament:", error);
            });
    };

    const handleDeleteTournament = (id) => {
        TournamentService.deleteTournament(id)
            .then(() => {
                setTournaments((prevTournaments) =>
                    prevTournaments.filter((tournament) => tournament.id !== id)
                );
            })
            .catch((error) => {
                console.error("Error deleting tournament:", error);
            });
    };

    const handleEditTournament = (tournament) => {
        setForm({ id: tournament.id, name: tournament.tournamentName || '' });
    };

    return (
        <>
            <Sidebar />
            <div>
                <div className="form-container">
                    <h1>Tournament</h1>
                    <input
                        type="text"
                        value={form.id}
                        readOnly
                        placeholder="Tournament Id"
                    />
                    <input
                        type="text"
                        value={form.name}
                        onChange={(e) => setForm({ ...form, name: e.target.value })}
                        placeholder="Tournament Name"
                    />
                    <div className="button-group">
                        <button onClick={handleAddTournament}>Create</button>
                        <button onClick={handleUpdateTournament} disabled={!form.id || !form.name}>
                            Update
                        </button>
                    </div>
                </div>

                <table className="tournament-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Tournament Name</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {tournaments && tournaments.length > 0 ? (
                            tournaments.map((tournament, index) => (
                                <tr key={tournament.id || index}>
                                    <td>{tournament.id}</td>
                                    <td>{tournament.tournamentName}</td>
                                    <td>
                                        <button onClick={() => handleEditTournament(tournament)}>Edit</button>
                                        <button onClick={() => handleDeleteTournament(tournament.id)}>Delete</button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="3">No tournaments available</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </>
    );
}

export default Tournament;
