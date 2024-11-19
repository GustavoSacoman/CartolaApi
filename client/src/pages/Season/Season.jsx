import React, { useState, useEffect } from "react";
import axios from "axios";
import "./Season.css";
import Sidebar from '../../components/Sidebar/Sidebar';
import { ToastContainer, toast } from "react-toastify"; 
import PersonIcon from "@mui/icons-material/Person"; 
import CloseIcon from "@mui/icons-material/Close";
import SaveIcon from "@mui/icons-material/Save";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";

const BASE_URL = "http://localhost:5210"; 
const user = { Name: "John Doe" }; 


const SeasonPage = () => {
    const [seasons, setSeasons] = useState([]); 
    const [selectedSeason, setSelectedSeason] = useState(null); 
    const [loading, setLoading] = useState(false); 
    const [error, setError] = useState(null); 
    const [isEditing, setIsEditing] = useState(false); 
    const [newSeason, setNewSeason] = useState({
      name: '',
      startDate: '',
      finalDate: ''
    });
  
    useEffect(() => {
      const fetchSeasons = async () => {
        try {
          setLoading(true);
          const response = await axios.get(`${BASE_URL}/api/v1/Season`, {
            headers: { 'Content-Type': 'application/json' }
          });
          console.log('Resposta da API:', response.data);
          setSeasons(response.data.data); // Confirme que "data" contém o array
        } catch (err) {
          console.error("Erro ao carregar temporadas:", err);
          setError("Não foi possível carregar as temporadas.");
        } finally {
          setLoading(false);
        }
      };
  
      fetchSeasons();
    }, []);
  
    const handleSeasonChange = async (event) => {
      const seasonId = event.target.value;
      if (!seasonId) {
        setSelectedSeason(null);
        return;
      }
  
      try {
        setLoading(true);
        const response = await axios.get(`${BASE_URL}/api/v1/Season/${seasonId}`, {
          headers: { 'Content-Type': 'application/json' }
        });
        setSelectedSeason(response.data);
      } catch (err) {
        console.error("Erro ao carregar detalhes da temporada:", err);
        setError("Não foi possível carregar os detalhes da temporada.");
      } finally {
        setLoading(false);
      }
    };
  
    const fetchSeasons = async () => {
        try {
          const response = await axios.get(`${BASE_URL}/api/v1/Seasons`);
          setSeasons(response.data.data);
        } catch (err) {
          toast.error('Erro ao buscar temporadas.');
          console.error('Erro ao buscar temporadas:', err);
        }
      };
      
      // Após adicionar ou editar a temporada, chame a função fetchSeasons para atualizar a lista
      

    const handleAddSeason = async () => {
        try {
          const response = await axios.post(`${BASE_URL}/api/v1/Season`, newSeason, {
            headers: { 'Content-Type': 'application/json' }
          });
      
          console.log('Nova temporada adicionada:', response.data.data);
          
          // Atualizando o estado das temporadas
          setSeasons(prevSeasons => {
            const updatedSeasons = [...prevSeasons, response.data.data];
            console.log('Estado atualizado de seasons após adição:', updatedSeasons);
            return updatedSeasons;
          });
      
          // Resetando o formulário
          setNewSeason({
            name: '',
            startDate: '',
            finalDate: ''
          });
      
          toast.success('Temporada adicionada com sucesso!');
          window.location.reload();
        } catch (err) {
          toast.error('Erro ao adicionar temporada.');
          console.error('Erro ao adicionar temporada:', err);
        }
      };
      
      
  
      const handleEditSeason = async () => {
        if (!selectedSeason) {
          toast.error('Nenhuma temporada selecionada para edição.');
          return;
        }
      
        try {
          const response = await axios.put(
            `${BASE_URL}/api/v1/Season/${selectedSeason.id}`,
            selectedSeason,
            { headers: { 'Content-Type': 'application/json' } }
          );
      
          console.log('Temporada editada:', response.data.data);
      
          // Atualizando o estado das temporadas
          setSeasons(prevSeasons => {
            const updatedSeasons = prevSeasons.map((season) =>
              season.id === selectedSeason.id ? response.data.data : season
            );
            console.log('Estado atualizado de seasons após edição:', updatedSeasons);
            return updatedSeasons;
          });
      
          setIsEditing(false);
          setSelectedSeason(null);
      
          toast.success('Temporada editada com sucesso!');
          window.location.reload();
        } catch (err) {
          toast.error('Erro ao editar temporada.');
          console.error('Erro ao editar temporada:', err);
        }
      };
      
      
      
      
  
    const handleDeleteSeason = async (seasonId) => {
      try {
        await axios.delete(`${BASE_URL}/api/v1/Season/${seasonId}`, {
          headers: { 'Content-Type': 'application/json' }
        });
        setSeasons(seasons.filter((season) => season.id !== seasonId));
        toast.success('Temporada excluída com sucesso!');
      } catch (err) {
        toast.error('Erro ao excluir temporada.');
        console.error('Erro ao excluir temporada:', err);
      }
    };
  
    const handleDiscard = () => {
      setIsEditing(false);
      setSelectedSeason(null);
      console.log("Alterações descartadas.");
    };

  return (
<div className="season-page">
  <div className="profile-container">
    <Sidebar />
    <ToastContainer />
    <div className="profile-content">
      <div className="profile-card content-loaded">
        <div className="profile-header">
          <div className="profile-info">
            <div className="profile-avatar">
              <PersonIcon style={{ fontSize: 60 }} />
            </div>
            <h2 className="welcome-message">
              Welcome! {user?.Name || "User"}
            </h2>
          </div>
          <div className="header-actions">
            {isEditing && (
              <button
                className="edit-button discard-button"
                onClick={handleDiscard}
                title="Discard changes"
              >
                <CloseIcon />
              </button>
            )}
            <button
              className="edit-button"
              onClick={() => setIsEditing(!isEditing)}
              title={isEditing ? "Save" : "Edit"}
            >
              {isEditing ? <SaveIcon /> : <EditIcon />}
            </button>
          </div>
        </div>

        <div className="content">
          <h1 className="title">Season Management</h1>
          {loading && <p>Loading...</p>}
          {error && <p className="error">{error}</p>}

          <div className="season-info">
            {/* Add New Season Form */}
            <div className="new-season-form">
              <h3>Add New Season</h3>
              <input
                type="text"
                placeholder="Season Name"
                value={newSeason.name}
                onChange={(e) => setNewSeason({ ...newSeason, name: e.target.value })}
              />
              <input
                type="datetime-local"
                value={newSeason.startDate}
                onChange={(e) => setNewSeason({ ...newSeason, startDate: e.target.value })}
              />
              <input
                type="datetime-local"
                value={newSeason.finalDate}
                onChange={(e) => setNewSeason({ ...newSeason, finalDate: e.target.value })}
              />
              <button onClick={handleAddSeason} className="add-season-button">
                <AddIcon /> Add Season
              </button>
            </div>

            {/* List Seasons with Edit and Delete buttons */}
            <div className="season-list">
              <h3>Existing Seasons</h3>
              {seasons.length > 0 ? (
                <ul>
                  {seasons.map((season) => (
                    <li key={season.id} className="season-item">
                      <div className="season-details">
                        {isEditing && selectedSeason?.id === season.id ? (
                          <>
                            <div>
                            <h3>Seasons List</h3>
                            <ul>
                                {seasons.map(season => (
                                <li key={season.id}>
                                    <span>{season.name}</span>
                                    {/* Outros detalhes da temporada */}
                                </li>
                                ))}
                            </ul>
                            </div>
                          </>
                        ) : (
                          <>
                            {/* Static text when not in edit mode */}
                            <span>{season.name}</span>
                            <span>
                              {new Date(season.startDate).toLocaleDateString()} - {new Date(season.finalDate).toLocaleDateString()}
                            </span>
                          </>
                        )}
                      </div>
                      <div className="season-actions">
                        {!isEditing && (
                          <button
                            onClick={() => setSelectedSeason(season)}
                            className="edit-season-button"
                          >
                            <EditIcon /> Edit
                          </button>
                        )}
                        <button onClick={() => handleDeleteSeason(season.id)} className="delete-season-button">
                          <DeleteIcon /> Delete
                        </button>
                      </div>
                    </li>
                  ))}
                </ul>
              ) : (
                <p>No seasons available.</p>
              )}
            </div>

            {/* Selected Season Details */}
            {selectedSeason && !isEditing && (
              <div className="info-box">
                <h3>Season Details</h3>
                <input
                    type="text"
                    value={selectedSeason.name}
                    onChange={(e) =>
                    setSelectedSeason({
                        ...selectedSeason,
                        name: e.target.value
                    })
                    }
                />
                <input
                    type="datetime-local"
                    value={selectedSeason.startDate}
                    onChange={(e) =>
                    setSelectedSeason({
                        ...selectedSeason,
                        startDate: e.target.value
                    })
                    }
                />
                <input
                    type="datetime-local"
                    value={selectedSeason.finalDate}
                    onChange={(e) =>
                    setSelectedSeason({
                        ...selectedSeason,
                        finalDate: e.target.value
                    })
                    }
                />
                <button onClick={handleEditSeason} className="save-edit-button">
                    Save
                </button>
                <button onClick={() => handleDeleteSeason(selectedSeason.id)} className="delete-season-button">
                  <DeleteIcon /> Delete Season
                </button>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  </div>
</div>



  );
};

export default SeasonPage;
