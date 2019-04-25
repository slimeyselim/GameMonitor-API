using System;

namespace GameMonitor.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Source { get; set; }
        public string Link { get; set; }
        public decimal? CurrentPrice { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateEnd { get; set; }
        //might use this for p+p ....
        public string Comments { get; set; }

        public Game Game { get; set; }

        public decimal CurrentPricePlusPostage(decimal currentPrice, string comment)
        {
            try
            {
                decimal commentToDecimal = Convert.ToDecimal(comment);
                return currentPrice + commentToDecimal;
            }
            catch (InvalidCastException error)
            {
                Console.WriteLine("Error in Converting Comment to Decimal", error);
                return currentPrice;
            }
            
        }
    }
}
