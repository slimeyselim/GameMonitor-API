using System.Collections.Generic;
using System.Linq;
using System.Net;
using GameMonitor.Data.Entities;
using GameMonitor.Data;
using System;
using GameMonitor.Utilities;
using System.Web.Configuration;

namespace GameMonitor.Models
{
    //need new dto for updating since required fields not required.

    public class ItemDTO
    {
        private static int LevenshteinValue = Convert.ToInt32(WebConfigurationManager.AppSettings["LevenshteinValue"]);

        public int Id { get; set; }
        public int GameId { get; set; }
        public string Source { get; set; }
        public string Link { get; set; }
        public decimal? CurrentPrice { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Comments { get; set; }

        public Game Game { get; set; }

        public decimal? GameDesiredPrice { get; set; }
        public string GameName { get; set; }
        //for post, might need separate obj?

        public List<ItemDTO> GetItems()
        {
            List<ItemDTO> items = new List<ItemDTO>();
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    items = db.Items
                         .Select(g => new ItemDTO
                         {
                             Id = g.Id,
                             GameId = g.GameId,
                             Source = g.Source,
                             Link = g.Link,
                             CurrentPrice = g.CurrentPrice,
                             DateUpdated = g.DateUpdated,
                             DateEnd = g.DateEnd,
                             Comments = g.Comments,
                             GameDesiredPrice = g.Game.DesiredPrice,
                             GameName = g.Game.Name
                         })
                         .ToList();
                }
                return items;
            }
            catch
            {
                //not best practice here, probably want to return object of game and status
                return items;
            }
        }

        public List<ItemDTO> GetItemsByGame(string gameName)
        {
            List <ItemDTO> items = new List<ItemDTO>();
            List<ItemDTO> filteredItems = new List<ItemDTO>();

            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    items = db.Items
                         .Select(g => new ItemDTO
                         {
                             Id = g.Id,
                             GameId = g.GameId,
                             Source = g.Source,
                             Link = g.Link,
                             CurrentPrice = g.CurrentPrice,
                             DateUpdated = g.DateUpdated,
                             DateEnd = g.DateEnd,
                             Comments = g.Comments,
                             GameDesiredPrice = g.Game.DesiredPrice,
                             GameName = g.Game.Name
                         })
                         .ToList();
                }

                filteredItems = items.FindAll(g => LevenshteinDistance.Compute(g.GameName, gameName) <= LevenshteinValue);

                return filteredItems;
            }
            catch
            {
                //not best practice here, probably want to return object of game and status
                return items;
            }
        }

        public ItemDTO GetItem(int id)
        {
            ItemDTO item = new ItemDTO();
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    item = db.Items
                         .Select(g => new ItemDTO
                         {
                             Id = g.Id,
                             GameId = g.GameId,
                             Source = g.Source,
                             Link = g.Link,
                             CurrentPrice = g.CurrentPrice,
                             DateUpdated = g.DateUpdated,
                             DateEnd = g.DateEnd,
                             Comments = g.Comments,
                             GameDesiredPrice = g.Game.DesiredPrice,
                             GameName = g.Game.Name
                         })
                         .Where(g => g.Id == id)
                         .SingleOrDefault();
                }
                return item;
            }
            catch
            {
                //not best practice here, probably want to return object of game and status
                return item;
            }
        }

        
        public HttpStatusCode PostItem(ItemDTO item)
        {
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    Item newItem = new Item();

                    newItem.GameId = item.GameId;
                    newItem.Source = item.Source;
                    newItem.Link = item.Link;
                    newItem.CurrentPrice = item.CurrentPrice;
                    newItem.DateUpdated = item.DateUpdated;
                    newItem.DateEnd = item.DateEnd;
                    newItem.Comments = item.Comments;

                    db.Items.Add(newItem);
                    db.SaveChanges();

                    return HttpStatusCode.OK;
                }
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public HttpStatusCode UpdateItem(int id, ItemPutDTO item)
        {
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    Item itemToUpdate = db.Items.SingleOrDefault(g => g.Id == id);

                    //if has value update fields. see what comes through as

                    itemToUpdate.CurrentPrice = item.CurrentPrice;
                    itemToUpdate.DateUpdated = DateTime.Now;

                    itemToUpdate.DateEnd = (item.DateEnd != null) ? item.DateEnd : itemToUpdate.DateEnd;
                    itemToUpdate.Comments = (item.Comments.Length > 0) ? item.Comments : itemToUpdate.Comments;

                    db.SaveChanges();

                    return HttpStatusCode.OK;
                }
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public HttpStatusCode DeleteItem(int id)
        {
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    Item itemToDelete = db.Items.SingleOrDefault(g => g.Id == id);

                    db.Items.Remove(itemToDelete);

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