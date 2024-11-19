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
    
    <div className='player-main'>   
      <div className='player-container'> 
        <section className='player-container-register'>
          <h1 id='player-title'>{isEditing ? 'Edit Players' : 'Register Players'}</h1>
            <div className='player-form-container'>
              <div className='player-register'>
                <input 
                type="text" 
                name="name" 
                value={formData.namePlayer}
                placeholder=" " 
                onChange={(e) => setFormData({ ...formData, namePlayer: e.target.value })}
                required />
                <label className='player-floating-label'>Name:</label>
              </div>
              <div className='player-register'>
              
                <input 
                type="text"
                name="position"
                value={formData.position}
                placeholder=" " 
                onChange={(e) => setFormData({ ...formData, position: e.target.value })}
                required />
                <label className='player-floating-label'>Position:</label>
              </div>
            </div>
        </section>
        <section className='palyer-container-list'>
          <div className='player-list-bar'>
            <div id='player-list-title'><h2>PLAYERS</h2></div>
              <div>
              <SearchIcon/>
                <input type='text'
                name='search'
                id='search'
                className='player-input-field'
                placeholder="Search player" 
                value={searchQuery}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyDown}
                required />
            </div>
          </div>
          <div className='player-list-player'>
            {players.map((player) => (
              <div className='player-player-container' key={player.id}>
                <button className='player' id={player.id}>
                  <div className="player-info">
                    <div>Name:</div>
                    <span className="player-name">{player.namePlayer}</span>
                    <div>Position:</div>
                    <span className="player-position"> {player.position}</span>
                  </div>
                </button>
                <div className='player-button-group' id={player.id + "player-button-excluir"}>
                  <button className='player-img-edit' id={player.id + "thrash"} onClick={() => editPlayer(player.id)}>
                    <EditIcon/>
                  </button>
                  <button className='player-img-delete' id={player.id} onClick={()=>deletePlayer(player.id)}>
                    <DeleteOutlineIcon/>
                  </button></div>
              </div>
))}
          </div> 
        </section>
        <section className='player-buttons'>
          <button type="button" onClick={handleCancel}>
            Cancel
          </button>
          <button type="button" onClick={handleSubmit}>
            {isEditing ? 'Edit' : 'Register'}
          </button>
        </section>
      </div>
      <ToastContainer />

      
    </div>
    </>
  );

};

export default Player;
