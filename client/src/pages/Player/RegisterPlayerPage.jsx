import React, { useEffect } from 'react';
import './Player.css';
import { RegisterPlayerService } from '../../api/services/RegisterPlayer';
import { useState } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import EditIcon from '@mui/icons-material/Edit';
import SearchIcon from '@mui/icons-material/Search';
import Sidebar from '../../components/Sidebar/Sidebar';

const DEFAULT_FORM_VALUES = {
  namePlayer: '',
  position: '',
  teamId: 0,
};

const Player = () => {
  const [players, setPlayers] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [formData, setFormData] = useState(DEFAULT_FORM_VALUES);
  const [isEditing, setIsEditing] = useState(false);
  const [editPlayerId, setEditPlayerId] = useState(null);

  //show players
  const getPlayers = async () => {
    try {
      const response = await RegisterPlayerService.getPlayers();
      setPlayers(response.data);
      console.log(response.data);
    } catch (error) {
      showError(error);
    }
  };
    //Busca jogadores ao carregar o componente
    useEffect(() => {
      getPlayers();
    }, []);
    
  const searchPlayers = async () => {
    try {
      const filterteredPlayers = await RegisterPlayerService.getPlayersName(searchQuery);
      setPlayers(filterteredPlayers);
    } catch (error) {
      showError(error);
    }
    }; 
  //Search players by name  
  const handleKeyDown = (event) => {
    if (event.key === 'Enter') {
      searchPlayers();
    }
  }; 
  //Handle submit form player
  const handleSubmit = async (e) => {
    e.preventDefault();
     
    const playerData = {
      namePlayer: formData.namePlayer,
      position: formData.position,
      teamId: 0,
    }
    if(playerData.namePlayer === '' || playerData.position === ''){
        showError('Preencha todos os campos'); 
        return;
      }
    try{
      if(isEditing){
        await RegisterPlayerService.updatePlayer(editPlayerId, playerData);
        showError('Jogador editado com sucesso');
      }else{
        await RegisterPlayerService.addPlayer(playerData);
        showError('Jogador cadastrado com sucesso');
      }
      setFormData({ namePlayer: '', position: '',teamId: 0,});
      setIsEditing(false);
      setEditPlayerId(null);
      getPlayers();
    }catch(error){
      showError(error);
    }
  };
  
  const handleInputChange = (e) => {
    const value = e.target.value;
    setSearchQuery(value);
    if (value === "") 
      searchPlayers("");
    
  };
  
  const editPlayer = (id) => {
    const player = players.find((player) => player.id === id);
    if(player){
      setFormData({
        namePlayer: player.namePlayer,
        position: player.position,
        teamId: player.teamId,
      });
      setIsEditing(true);
      setEditPlayerId(id);
    }
  };
    
  const handleCancel = () => {
    setFormData(DEFAULT_FORM_VALUES);
    setIsEditing(false);
    setEditPlayerId(null);
    setSearchQuery('');
  };

  //delete player
  const deletePlayer = async (id) => {
    try {
      await RegisterPlayerService.deletePlayer(id);
      const newPlayers = players.filter((player) => player.id !== id);
      setPlayers(newPlayers);
    } catch (error) {
      showError(error);
    }}
  
  const showError = (message) => {
    toast.error(message, {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: false,
      draggable: true,
      progress: undefined,
      
    });
  };

  
  window.onload = getPlayers;

  return (
    <>
      <Sidebar/>
      <div className='player-page'>   
        <div className='player-card'> 
          <section className='player-form-section'>
            <h1 className='player-title'>{isEditing ? 'Edit Player' : 'Register Player'}</h1>
            <div className='player-form'>
              <div className='form-group'>
                <input 
                  type="text" 
                  name="name" 
                  value={formData.namePlayer}
                  placeholder=" " 
                  className='form-input'
                  onChange={(e) => setFormData({ ...formData, namePlayer: e.target.value })}
                  required 
                />
                <label className='form-label'>Name</label>
              </div>
              <div className='form-group'>
                <input 
                  type="text"
                  name="position"
                  value={formData.position}
                  placeholder=" " 
                  className='form-input'
                  onChange={(e) => setFormData({ ...formData, position: e.target.value })}
                  required 
                />
                <label className='form-label'>Position</label>
              </div>
            </div>
          </section>

          <section className='player-list-section'>
            <div className='search-bar'>
              <h2 className='player-title'>Players</h2>
              <div className='search-wrapper'>
                <input 
                  type='text'
                  name='search'
                  className='search-input'
                  placeholder="Search player" 
                  value={searchQuery}
                  onChange={handleInputChange}
                  onKeyDown={handleKeyDown}
                />
              </div>
            </div>

            <div className='player-list'>
              {players.map((player) => (
                <div className='player-item' key={player.id}>
                  <div className="player-details">
                    <div>Name:</div>
                    <span>{player.namePlayer}</span>
                    <div>Position:</div>
                    <span>{player.position}</span>
                  </div>
                  <div className='player-actions'>
                    <button 
                      className='action-button' 
                      onClick={() => editPlayer(player.id)}
                    >
                      <EditIcon/>
                    </button>
                    <button 
                      className='action-button delete' 
                      onClick={() => deletePlayer(player.id)}
                    >
                      <DeleteOutlineIcon/>
                    </button>
                  </div>
                </div>
              ))}
            </div> 
          </section>

          <section className='form-buttons'>
            <button 
              type="button" 
              className='btn btn-secondary' 
              onClick={handleCancel}
            >
              Cancel
            </button>
            <button 
              type="button" 
              className='btn btn-primary' 
              onClick={handleSubmit}
            >
              {isEditing ? 'Save Changes' : 'Register Player'}
            </button>
          </section>
        </div>
        <ToastContainer />
      </div>
    </>
  );
};

export default Player;
