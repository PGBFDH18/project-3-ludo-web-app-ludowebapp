using LudoGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebAPI.Models
{
    public class LudoModel : ILudoModel
    {
        public Guid AddGame()
        {
            g = Guid.NewGuid();

            theGames.Add(g, new LudoGame(new Dice()));
            return g;
        }


        public Dictionary<Guid, LudoGame> theGames;
        public Guid g;

        public LudoModel()
        {
            theGames = new Dictionary<Guid, LudoGame>();
        }


        public bool RemoveGame(Guid id)
        {
            return theGames.Remove(id);
        }

        public Player AddPlayer(Guid id, string name, int colorID)
        {
            if (name == null)
            {
                return null;
            }
            return theGames[id].AddPlayer(name, colorID);
        }

        public bool RemovePlayer(Guid id, int colorID)
        {
            return theGames[id].RemovePlayer(colorID);
        }

        public Dictionary<Guid, LudoGame> GetAllGames()
        {
            return theGames;
        }

        public LudoGame GetGameDetail(Guid _id)
        {
            return theGames.First(id => id.Key == _id).Value;
        }

        public Player[] GetAllPlayers(Guid id)
        {
            return theGames[id].GetPlayers();
        }

        public Player GetPlayerDetails(Guid id, int colorID)
        {
            return theGames[id].GetPlayer(colorID);
        }

        public Player ChangePlayerDetails(Guid id, int oldColorID, string name, int colorID)
        {
            return theGames [id].UpdatePlayer(oldColorID, name, colorID);
        }

        public Piece MovePiece(Guid id, int pieceId, int numberOfFields)
        {
            return theGames[id].MovePiece(theGames[id].GetCurrentPlayer(), pieceId, numberOfFields);
        }

        public bool StartGame(Guid id)
        {
            return theGames[id].StartGame();
        }

        public int RollDice(Guid id)
        {
            return theGames[id].RollDice();
        }

        public void EndTurn(Guid id)
        {
            theGames[id].EndTurn(theGames[id].GetCurrentPlayer());
        }

        public Player GetWinner(Guid id)
        {
            return theGames[id].GetWinner();
        }
    }
}