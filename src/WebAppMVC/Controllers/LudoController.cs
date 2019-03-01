using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebAppMVC.Models;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Newtonsoft.Json;
using WebAppMVC.Code;
using Microsoft.Extensions.Logging;

namespace WebAppMVC.Controllers
{
    public class LudoController : Controller
    {
        private IRestClient client;
        private ILogger<LudoController> logger;

        public LudoController(IRestClient client, ILogger<LudoController> _logger)
        {
            this.client = client;
            client.BaseUrl = new Uri("https://ludoapp.azurewebsites.net");
            logger = _logger;
        }

        public IActionResult CreateGame(CreateLudoGameModel model)
        {
            // Create game
            model.GameId = Api.CreateGame(client);
            logger.LogInformation("User created a new game.");

            // Add all players that aren't null to the game.
            if (model.Player1Name != null)
            {
                Api.AddPlayer(model.GameId, model.Player1Name, "0", client);
                logger.LogInformation($"User added player: {model.Player1Name}");
            }
            if (model.Player2Name != null)
            {
                Api.AddPlayer(model.GameId, model.Player2Name, "1", client);
                logger.LogInformation($"User added player: {model.Player2Name}");
            }
            if (model.Player3Name != null)
            {
                Api.AddPlayer(model.GameId, model.Player3Name, "2", client);
                logger.LogInformation($"User added player: {model.Player3Name}");
            }
            if (model.Player4Name != null)
            {
                Api.AddPlayer(model.GameId, model.Player4Name, "3", client);
                logger.LogInformation($"User added player: {model.Player4Name}");
            }

            // Set cookies
            Api.AssignPlayerCookies(model, Response);

            return RedirectToAction("Lobby");
        }

        public IActionResult Game()
        {
            Guid gameId = Guid.Parse(Request.Cookies["gameid"].ToString());
            GameModel game = Api.GetSpecificGame(gameId, client);
            if (Request.Cookies["lastdicevalue"] != null)
            {
                game.diceValue = int.Parse(Request.Cookies["lastdicevalue"]);
            }

            return View(game);
        }

        public IActionResult Home()
        {
            return View();
        }

        

        public IActionResult Lobby()
        {
            if (Request.Cookies["gameid"] == null)
            {
                logger.LogInformation("User tried to navigate to lobby with no assigned game.");
                ViewBag.message = "You are currently not assigned to a game.";
                return View();
            }

            // Get the cookie that represents which game the user is playing
            var gameId = Request.Cookies["gameid"].ToString();

            // Get players for the game and deserialize them
            PlayerModelContainer model = new PlayerModelContainer
            {
                gameId = Guid.Parse(gameId),
                Players = Api.GetAllPlayers(Guid.Parse(gameId), client)
            };

            return View(model);
        }

        public IActionResult MovePiece(GameModel model)
        {
            Guid gameId = Guid.Parse(Request.Cookies["gameid"]);
            Api.MovePiece(gameId, (model.pieceId - 1), int.Parse(Request.Cookies["lastdicevalue"]), client);
            logger.LogInformation($"Piece moved number of steps: {Request.Cookies["lastdicevalue"].ToString()}");
            Response.Cookies.Append("lastdicevalue", "0");
            TempData["rolled"] = "false";


            return RedirectToAction("game");
        }

        public IActionResult NewGame()
        {
            return View();
        }

        public IActionResult RollDice(GameModel model)
        {
            if (TempData["rolled"] != "true")
            {
                Guid gameId = Guid.Parse(Request.Cookies["gameid"]);
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Append("lastdicevalue", Api.RollDice(gameId, client).ToString(), cookie);
                logger.LogInformation("User rolled dice.");
            }
            TempData["rolled"] = "true";
            return RedirectToAction("game");
        }

        public IActionResult Rules()
        {
            return View();
        }

        

        public IActionResult StartGame()
        {
            Guid gameId = Guid.Parse(Request.Cookies["gameid"]);
            bool startGameSuccess = Api.StartGame(gameId, client);

            if (startGameSuccess)
            {
                logger.LogInformation("User successfully started game.");
                return RedirectToAction("Game");
            }
            else
            {
                logger.LogInformation("Failed to start game.");
                ViewBag.errorMessage = "Couldn't start game.";
                return View("Lobby", new PlayerModelContainer { gameId = gameId, Players = Api.GetAllPlayers(gameId, client) });
            }
        }
    }
}