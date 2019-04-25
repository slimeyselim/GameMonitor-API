using System;

namespace GameMonitor.Data.Entities
{
    public class Token
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        //Token() { } if have this then protects from creating other ways, need to make explicit for all
    }
}
