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
    public class ItemController : ApiController
    {
        private GameMonitorDbContext db = new GameMonitorDbContext();
        private ItemDTO _item = new ItemDTO();

        [ApiAuthenticationFilter(false)]
        public HttpResponseMessage Get()
        {
            try
            {
                List<ItemDTO> items = _item.GetItems();
                return Request.CreateResponse(HttpStatusCode.OK, items);
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
                ItemDTO item = _item.GetItem(id);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, exception);
            }
        }

        [ApiAuthenticationFilter(false)]
        [Route("api/itemByGameName/{name}")]
        public HttpResponseMessage Get(string name)
        {
            try
            {
                List<ItemDTO> items = _item.GetItemsByGame(name);
                return Request.CreateResponse(HttpStatusCode.OK, items);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, exception);
            }
        }

        [ApiAuthenticationFilter(true)]
        public HttpResponseMessage Post([FromBody] ItemDTO clientItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _item.PostItem(clientItem);
                    return Request.CreateResponse(HttpStatusCode.OK, "Item added");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Something went wrong...");
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, exception);
            }
        }

        [ApiAuthenticationFilter(true)]
        public HttpResponseMessage Put(int id, [FromBody] ItemPutDTO clientItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _item.UpdateItem(id, clientItem);
                    return Request.CreateResponse(HttpStatusCode.OK, "Item Updated");
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
                _item.DeleteItem(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Item was deleted");
            }
            catch (Exception exception)
            {
                //log error
                return Request.CreateResponse(HttpStatusCode.NotFound, exception);
            }
        }

    }
}
