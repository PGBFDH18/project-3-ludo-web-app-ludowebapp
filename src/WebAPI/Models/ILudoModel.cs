using LudoGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public interface ILudoModel
    {
        Guid AddGame();
        bool RemoveGame(Guid id);
        Player AddPlayer(Guid id, string name, int colorID);
        bool RemovePlayer(Guid id, int colorID);
        Dictionary<Guid, LudoGame> GetAllGames();
        LudoGame GetGameDetail(Guid id);
        Player[] GetAllPlayers(Guid id);
        Player GetPlayerDetails(Guid id, int colorID);
        Player ChangePlayerDetails(Guid id, int oldColorID, string name, int colorID);
        Piece MovePiece(Guid id, int pieceId, int numberOfFields);
        bool StartGame(Guid id);
        int RollDice(Guid id);
        void EndTurn(Guid id);
        Player GetWinner(Guid id);
    }
}