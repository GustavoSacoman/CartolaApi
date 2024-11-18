import React, { useState, useEffect } from "react";
import axios from "axios";
import "./Season.css";

const SeasonPage = () => {
    const [seasons, setSeasons] = useState([]); // Lista de temporadas
    const [selectedSeason, setSelectedSeason] = useState(null); // Temporada selecionada
    const [loading, setLoading] = useState(false); // Estado de carregamento
    const [error, setError] = useState(null); // Mensagem de erro

    // Carrega todas as temporadas quando o componente é montado
    useEffect(() => {
        const fetchSeasons = async () => {
            try {
                setLoading(true);
                const response = await axios.get("/v1/seasons"); // Substitua pelo endpoint correto
                setSeasons(response.data); // Atualiza a lista de temporadas
            } catch (err) {
                console.error("Erro ao carregar temporadas:", err);
                setError("Não foi possível carregar as temporadas.");
            } finally {
                setLoading(false);
            }
        };

        fetchSeasons();
    }, []);

    // Carrega os detalhes da temporada selecionada
    const handleSeasonChange = async (event) => {
        const seasonId = event.target.value;
        if (!seasonId) {
            setSelectedSeason(null);
            return;
        }

        try {
            setLoading(true);
            const response = await axios.get(`/v1/seasons/${seasonId}`); // Substitua pelo endpoint correto
            setSelectedSeason(response.data); // Atualiza os detalhes da temporada
        } catch (err) {
            console.error("Erro ao carregar detalhes da temporada:", err);
            setError("Não foi possível carregar os detalhes da temporada.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="season-page">
            <div className="sidebar">
                <img src="logo.png" alt="Logo" className="logo" />
                <ul>
                    <li>Login</li>
                    <li>Team</li>
                    <li>Tournament</li>
                    <li>Perfil</li>
                </ul>
            </div>
            <div className="content">
                <h1 className="title">Season</h1>
                {loading && <p>Loading...</p>}
                {error && <p className="error">{error}</p>}

                <div className="season-info">
                    <div className="season-select">
                        <label htmlFor="season">Select season:</label>
                        <select id="season" onChange={handleSeasonChange}>
                            <option value="">Select a season</option>
                            {seasons.map((season) => (
                                <option key={season.id} value={season.id}>
                                    {season.name}
                                </option>
                            ))}
                        </select>
                    </div>

                    {selectedSeason && (
                        <div className="info-box">
                            <h3>Details</h3>
                            <p><strong>Name:</strong> {selectedSeason.name}</p>
                            <p><strong>Start Date:</strong> {new Date(selectedSeason.startDate).toLocaleDateString()}</p>
                            <p><strong>Final Date:</strong> {new Date(selectedSeason.finalDate).toLocaleDateString()}</p>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default SeasonPage;
