import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import '../../pages/RegisterMatch/Match.css';
import MatchService from '../../api/services/MatchService';
import Sidebar from '../../components/Sidebar/Sidebar';

function ListMatch() {
  const [search, setSearch] = useState('');
  const [matches, setMatches] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchMatches = async () => {
      try {
        const matchData = await MatchService.getMatches();
        setMatches(matchData);
      } catch (error) {
        console.error('Error fetching matches:', error);
      }
    };

    fetchMatches();
  }, []);

  const deleteMatch = async (id) => {
    try {
      await MatchService.deleteMatch(id);
      const updatedMatches = await MatchService.getMatches();
      setMatches(updatedMatches);
    } catch (error) {
      console.error('Error deleting match:', error);
    }
  };

  const handleSearchChange = (e) => {
    setSearch(e.target.value);
  };

  const handleEdit = (match) => {
    localStorage.setItem('selectedMatch', JSON.stringify(match)); // Salva no localStorage
    navigate(`/edit-match/${match.idMatch}`);
  };

  const handleBack = () => {
    navigate('/match');
  };

  const filteredMatches = matches.filter((match) =>
    match.idTeam1.toString().includes(search) || match.idTeam2.toString().includes(search)
  );

  return (
    <>
      <Sidebar />
      <div className="match-container">
      <div className="match-form">
        <h2>Match</h2>
        <div className="input-group">
          <input
            type="text"
            placeholder="Search by team ID:"
            value={search}
            onChange={handleSearchChange}
          />
        </div>
        <div className="matches-table">
          <table>
            <thead>
              <tr>
                <th>Teams</th>
                <th>Result</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {filteredMatches.map((match) => (
                <tr key={match.idMatch}>
                  <td>{`Team ${match.idTeam1} X Team ${match.idTeam2}`}</td>
                  <td>{match.result}</td>
                  <td className="actions">
                    <button
                      className="edit-button"
                      onClick={() => handleEdit(match)}
                    >
                      Edit
                    </button>
                    <button
                      className="delete-button"
                      onClick={() => deleteMatch(match.idMatch)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
        <div className="button-group">
          <Link to="/register-match">
            <button className="new-button">New</button>
          </Link>
          <button className="new-button" onClick={handleBack}>Back</button>
        </div>
        </div>
      </div>
    </>
  );
}

export default ListMatch;
