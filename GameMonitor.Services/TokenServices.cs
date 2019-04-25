using System;
using System.Linq;
using GameMonitor.Data;
using GameMonitor.Data.Entities;

namespace GameMonitor.Services
{
    public class TokenServices : ITokenServices
    {
        //15 mins. in example in web.config
        double expirationTime = 900;

        public Token GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(expirationTime);

            Token tokenDomain = new Token
            {
                UserId = userId,
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };

            using (GameMonitorDbContext db = new GameMonitorDbContext())
            {
                db.Tokens.Add(tokenDomain);
                db.SaveChanges();
            }

            return tokenDomain;
        }

        public bool ValidateToken(string tokenId)
        {
            using (GameMonitorDbContext db = new GameMonitorDbContext())
            {
                var token = db.Tokens
                        .Where(t=> t.ExpiresOn>DateTime.Now)
                        .SingleOrDefault(t => t.AuthToken == tokenId);

                if(token != null)
                {
                    token.ExpiresOn = DateTime.Now.AddSeconds(expirationTime);
                    db.SaveChanges();

                    return true;
                }
                return false;
            }
        }

        public bool Kill(string tokenId)
        {
            using (GameMonitorDbContext db = new GameMonitorDbContext())
            {
                var tokens = db.Tokens
                    .Where(t => t.AuthToken == tokenId)
                    .ToList();

                db.Tokens.RemoveRange(tokens);
                db.SaveChanges();

                var IsNotDeleted = db.Tokens.Select(t => t.AuthToken == tokenId).ToList();

                if (IsNotDeleted.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool DeleteByUserId(int userId)
        {
            using (GameMonitorDbContext db = new GameMonitorDbContext())
            {
                var tokens = db.Tokens
                    .Where(t => t.UserId == userId)
                    .ToList();

                db.Tokens.RemoveRange(tokens);
                db.SaveChanges();

                var IsNotDeleted = db.Tokens.Select(t => t.UserId == userId).ToList();

                if (IsNotDeleted.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
