using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LudoGameEngine;
using WebAPI.Models;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebAPI.Controllers
{
    [Route("[Controller]")]
    public class LudoController : Controller
    {
        

        [HttpGet]
        public ContentResult Index()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content =
                "<html><body><center> " +
                "<h1> Welcome to Ludo Game </h1>" +
                "<link type='text/html' href='/view.html' />" +
                "<a href = \"/ludo/newludogame\"> Click here to Create new game! </a>" +

                "</center></body></html>"
            };
        }
        //Set Cookie 
        
        public ILudoModel context;
        public LudoController(ILudoModel _context)
        {
            context = _context;
        }

        // POST: ludo/mkgame (This is creates new Game with an ID using GUID) 
        [HttpPost("newludogame")]
        public IActionResult CreateNewGame()
        {
            Guid guid = context.AddGame();
            guid.ToString().Replace('-','a');
            return Ok(guid);
        }

        // DELETE: api/ludo/{IDofTheGame(GUID ID)}/regame (This is just REMOVE a game) 
        [HttpDelete("{id}/deletgame")]
        public IActionResult RemoveGame(Guid id)
        {
            if (!context.RemoveGame(id))
            {
                return NotFound(id);
            }
            else
            {
                return Ok();
            }
        }

        // POST: ludo/{IDofTheGame(GUID ID)}/players/addplayer?name={input}&colorID={input<=1,4=>}
        [HttpPost("{id}/players/addplayer")]
        public IActionResult AddPlayer(Guid id, string name, int colorID)
        {
            if (context.AddPlayer(id, name, colorID) == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(context.GetGameDetail(id).GetPlayer(colorID));
            }
        }

        // DELETE: ludo{{IDofTheGame(GUID ID)}/players
        [HttpDelete("{id}/players")]
        public IActionResult RemovePlayer(Guid id, int colorID)
        {
            if (!context.RemovePlayer(id, colorID))
            {
                return NotFound(new KeyValuePair<Guid, int>(id, colorID));
            }

            return Ok(new KeyValuePair<Guid, int>(id, colorID));
        }

        // GET: ludo/getallgames
        [HttpGet("getallgames")]
        public IActionResult GetAllGames()
        {
            return Ok(context.GetAllGames());
        }

        // GET: ludo/{{IDofTheGame(GUID ID)}/getgamedetails
        [HttpGet("{id}/getgamedetails")]
        public IActionResult GetGameDetails(Guid id)
        {
            if (context.GetGameDetail(id) == null)
            {
                return NotFound(id);
            }

            return Ok(context.GetGameDetail(id));
        }

        // GET: ludo/{{IDofTheGame(GUID ID)}/players/getplayers
        [HttpGet("{id}/players/getplayers")]
        public IActionResult GetAllPlayers(Guid id)
        {
            if (context.GetAllPlayers(id) == null)
            {
                return NotFound(id);
            }

            return Ok(context.GetAllPlayers(id));
        }

        // GET: ludo/{{IDofTheGame(GUID ID)}/players?colorID={input}
        [HttpGet("{id}/players")]
        public IActionResult GetPlayerDetails(Guid id, int colorID)
        {
            if (context.GetPlayerDetails(id, colorID) == null)
            {
                return NotFound(id);
            }

            return Ok(context.GetPlayerDetails(id, colorID));
        }

        // PUT: ludo/{{IDofTheGame(GUID ID)}/changeplayerdetails
        [HttpPut("{id}/changeplayerdetails")]
        public IActionResult ChangePlayerDetails(Guid id,int oldColorID, string name, int colorID)
        {
            var foo = context.ChangePlayerDetails(id, oldColorID, name, colorID);

            if (foo == null)
            {
                return NotFound(foo);
            }

            return Ok(foo);
        }

        // PUT: ludo/{{IDofTheGame(GUID ID)}/startgame
        [HttpPut("{id}/startgame")]
        public IActionResult StartGame(Guid id)
        {
            return Ok(context.StartGame(id));
        }

        // GET: ludo/{{IDofTheGame(GUID ID)}/rolldice
        [HttpGet("{id}/rolldice")]
        public IActionResult RollDice(Guid id)
        {
            return Ok(context.RollDice(id));
        }

        // PUT: ludo/{{IDofTheGame(GUID ID)}/movepiece
        [HttpPut("{id}/movepiece")]
        public IActionResult MovePiece(Guid id, int pieceId, int numberOfFields)
        {
            return Ok(context.MovePiece(id, pieceId, numberOfFields));
        }

        // PUT: ludo/{{IDofTheGame(GUID ID)}/endturn
        [HttpPut("{id}/endturn")]
        public IActionResult EndTurn(Guid id)
        {
            context.EndTurn(id);
            return Ok();
        }

        // GET: ludo/{{IDofTheGame(GUID ID)}/getwinner
        [HttpGet("{id}/getwinner")]
        public IActionResult GetWinner(Guid id)
        {
            return Ok(context.GetWinner(id));
        }
    }
}