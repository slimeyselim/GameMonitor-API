using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameMonitor.Models
{
    public class ItemPutDTO
    {
        public decimal CurrentPrice { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Comments { get; set; }
    }
}