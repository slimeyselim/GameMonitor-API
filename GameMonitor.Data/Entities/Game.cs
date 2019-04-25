using System.Collections.Generic;

namespace GameMonitor.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DesiredPrice { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public Game()
        {
            Items = new List<Item>();
        }
    }
}
