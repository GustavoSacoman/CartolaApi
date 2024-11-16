import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import './Matches.css';
import axios from 'axios';

function Matches() {
  const [search, setSearch] = useState(''); // Mantém o estado da busca
  const [matches, setMatches] = useState([]); // Estado para armazenar as partidas

  const navigate = useNavigate();

  // Faz a requisição para buscar as partidas
  useEffect(() => {
    axios.get('http://localhost:5210/v1/api/v1/match')
      .then(response => {
        setMatches(response.data.data); // Supondo que os dados estejam na chave `data`
      })
      .catch(error => {
        console.error('Erro ao buscar matches', error);
      });
  }, []);

  function excluir(id) {
    axios.delete(`http://localhost:5210/v1/api/v1/match/${id}`).then(() => {
      // Refaz a requisição para atualizar a lista após exclusão
      axios.get('http://localhost:5210/v1/api/v1/match')
        .then(response => {
          setMatches(response.data.data); // Atualiza a lista de partidas
        })
        .catch(error => {
          console.error('Erro ao buscar matches', error);
        });
    });
  }

  const handleSearchChange = (e) => {
    setSearch(e.target.value); // Atualiza o valor da busca
  };

  const filteredMatches = matches.filter(match => {
    return (
      match.idTeam1.toString().includes(search) || match.idTeam2.toString().includes(search)
    );
  });

  const handleEdit = (idMatch) => {
    // Redireciona para a página de edição
    navigate(`/edit-match/${idMatch}`);
  };

  const handleBack = () => {
    navigate('/matches'); // Redireciona para a página de lista de partidas
  };

  return (
    <div className="match-form">
      <h2>Matches</h2>
      
      {/* Campo de busca */}
      <div className="input-group">
        <input
          type="text"
          placeholder="Search by team ID:"
          value={search}
          onChange={handleSearchChange}
        />
      </div>

      {/* Lista de partidas */}
      <div className="matches-table">
        <table>
          <thead>
            <tr>
              <th>Teams</th>
              <th>Result</th> {/* Coluna de resultado */}
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {filteredMatches.map(match => (
              <tr key={match.idMatch}>
                <td>
                  {/* Exibe os IDs dos times */}
                  {`Team ${match.idTeam1} X Team ${match.idTeam2}`}
                </td>
                <td>
                  {/* Exibe o resultado da partida */}
                  {match.result}
                </td>
                <td className="actions">
                  {/* Botão Edit */}
                  <button
                    className="edit-button"
                    onClick={() => handleEdit(match.idMatch)}
                  >
                    Edit
                  </button>
                  
                  {/* Botão Delete */}
                  <button
                    className="delete-button"
                    onClick={() => excluir(match.idMatch)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Botões de ação */}
      <div className="button-group">
        <Link to="/cadastro-match">
          <button className="new-button">New</button>
        </Link>
        <button className="new-button" onClick={handleBack}>Voltar</button>
      </div>
    </div>
  );
}

export default Matches;
