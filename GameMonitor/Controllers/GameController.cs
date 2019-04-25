using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameMonitor.ActionFilters;
using GameMonitor.Data;
using GameMonitor.Filters;
using GameMonitor.Models;

namespace GameMonitor.Controllers
{
    public class GameController : ApiController
    {
        private GameMonitorDbContext db = new GameMonitorDbContext();
        private GameDTO _game = new GameDTO();

        [ApiAuthenticationFilter(false)]
        public HttpResponseMessage Get()
        {
            try
            {
                List<GameDTO> games = _game.GetGames();
                return Request.CreateResponse(HttpStatusCode.OK, games);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, exception);
            }
        }

        [ApiAuthenticationFilter(false)]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                GameDTO game = _game.GetGame(id);
                return Request.CreateResponse(HttpStatusCode.OK, game);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, exception);
            }
        }

        [ApiAuthenticationFilter(false)]
        [Route("api/gameByName/{name}")]
        public HttpResponseMessage Get(string name)
        {
            try
            {
                List<GameDTO> games = _game.GetGames(name);
                return Request.CreateResponse(HttpStatusCode.OK, games);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, exception);
            }
        }

        [ApiAuthenticationFilter(true)]
        public HttpResponseMessage Post([FromBody] GameDTO clientGame)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _game.PostGame(clientGame);
                    return Request.CreateResponse(HttpStatusCode.OK, "Game added");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Something went wrong...");
            }
            catch(Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, exception);
            }
        }

        [ApiAuthenticationFilter(true)]
        public HttpResponseMessage Put(int id, [FromBody] GameDTO clientGame)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _game.UpdateGame(id, clientGame);
                    return Request.CreateResponse(HttpStatusCode.OK, clientGame.Name + " Updated");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Something went wrong...");

            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, exception);
            }
        }

        [ApiAuthenticationFilter(true)]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _game.DeleteGame(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Game was deleted");
            }
            catch (Exception exception)
            {
                //log error
                return Request.CreateResponse(HttpStatusCode.NotFound, exception);
            }
        }
    }
}
