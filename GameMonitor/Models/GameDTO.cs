using System.Collections.Generic;
using System.Linq;
using System.Net;
using GameMonitor.Data.Entities;
using GameMonitor.Data;
using GameMonitor.Utilities;
using System.Web.Configuration;
using System;

namespace GameMonitor.Models
{
    public class GameDTO
    {
        private static int LevenshteinValue = Convert.ToInt32(WebConfigurationManager.AppSettings["LevenshteinValue"]);

        public int Id { get; set;}
        public string Name { get; set; }
        public decimal DesiredPrice { get; set; }

        public virtual List<Item> Items { get; set; }
       
        public List<GameDTO> GetGames()
        {
            List<GameDTO> games = new List<GameDTO>();
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    games = db.Games
                         .Select(g => new GameDTO
                         {
                             Id = g.Id,
                             Name = g.Name,
                             DesiredPrice = g.DesiredPrice,
                             Items = g.Items.ToList()
                         })
                         .ToList();

                }
                return games;
            }
            catch
            {
                //not best practice here, probably want to return object of game and status
                return games;
            }
        }

        public List<GameDTO> GetGames(string name)
        {
            List<GameDTO> games = new List<GameDTO>();
            List<GameDTO> filteredGames = new List<GameDTO>();

            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    games = db.Games
                         .Select(g => new GameDTO
                         {
                             Id = g.Id,
                             Name = g.Name,
                             DesiredPrice = g.DesiredPrice,
                             Items = g.Items.ToList()
                         })
                         .ToList();
                }
                filteredGames = games.FindAll(g => LevenshteinDistance.Compute(g.Name, name) <= LevenshteinValue);  

                return filteredGames;
            }
            catch
            {
                //not best practice here, probably want to return object of game and status
                return games;
            }
        }

        public GameDTO GetGame(int id)
        {
            GameDTO game = new GameDTO();

            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    game = db.Games
                         .Select(g => new GameDTO
                         {
                             Id = g.Id,
                             Name = g.Name,
                             DesiredPrice = g.DesiredPrice,
                             Items = g.Items.ToList()
                         })
                         .Where(g => g.Id == id)
                         .SingleOrDefault();
                }
                return game;
            }
            catch
            {
                //not best practice here, probably want to return object of game and status
                return game;
            }
            
        }

        public HttpStatusCode PostGame(GameDTO game)
        {
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {           
                    Game newGame = new Game();

                    newGame.Name = game.Name;
                    newGame.DesiredPrice = game.DesiredPrice;

                    db.Games.Add(newGame);
                    db.SaveChanges();

                    return HttpStatusCode.OK;
                }
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public HttpStatusCode UpdateGame(int id, GameDTO game)
        {
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {   
                    Game gameToUpdate = db.Games.SingleOrDefault(g => g.Id == id);

                    gameToUpdate.Name = game.Name;
                    gameToUpdate.DesiredPrice = game.DesiredPrice;

                    db.SaveChanges();

                    return HttpStatusCode.OK;
                }
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public HttpStatusCode DeleteGame(int id)
        {
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    Game gameToDelete = db.Games.SingleOrDefault(g => g.Id == id);

                    db.Games.Remove(gameToDelete);

                    db.SaveChanges();

                    return HttpStatusCode.OK;
                }
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}