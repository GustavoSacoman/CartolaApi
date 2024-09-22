﻿using CartolaApi.Data.Models;

namespace CartolaApi.Data.Functions;

public class TournamentDbFunctions
{
    private readonly AppDbContext _db;

    public TournamentDbFunctions( AppDbContext db)
    {
        _db = db;
    }
    
    public bool VerifyTournamentExistence(int? tournamentId, string? tournamentName)
    {
        Tournament? tournament = null;

        if (tournamentId != null && tournamentName != null)
        {
            tournament = _db.Tournaments.FirstOrDefault(t => t.Id == tournamentId);
        }
        else if (tournamentName != null)
        {
            tournament = _db.Tournaments.FirstOrDefault(t => t.TournamentName == tournamentName);
        }

        return tournament != null;
    }
    
    public List<Tournament> GetTournaments()
    {
        return _db.Tournaments.ToList();
    }
    
    
    public void CreateTournament(Tournament tournament)
    {
        if (VerifyTournamentExistence(null, tournament.TournamentName))
        {
            throw new Exception("Tournament already exists");
        }

        _db.Tournaments.Add(tournament);
        _db.SaveChanges();
    }
    
    public void DeleteTournament(string tournamentName)
    {
        if (!VerifyTournamentExistence(null, tournamentName))
        {
            throw new Exception("Tournament not found");
        }

        var tournament = _db.Tournaments.FirstOrDefault(tournament => tournament.TournamentName == tournamentName);
        _db.Tournaments.Remove(tournament);
        _db.SaveChanges();
    }
    
    public void UpdateTournament(Tournament tournament, string newTournamentName, List<Team>? teams, Team? team)
    {
        if (!VerifyTournamentExistence(tournament.Id ?? null, tournament.TournamentName))
        {
            throw new Exception("Tournament not found");
        }
        if (teams != null)
        {
            foreach (var t in teams)
            {
                tournament.Teams.Add(t);
            }
        }
        if (team != null)
        {
            tournament.Teams.Add(team);
        }
        tournament.TournamentName = newTournamentName;
        _db.SaveChanges();
        
    }
    
}
    
