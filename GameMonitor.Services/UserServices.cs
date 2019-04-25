using System.Collections.Generic;
using System.Linq;
using System.Net;
using GameMonitor.Data.Entities;
using GameMonitor.Data;
using System;

namespace GameMonitor.Services
{
    public class UserServices : IUserServices
    {
        public int Authenticate(string username, string password)
        {
            try
            {
                using (GameMonitorDbContext db = new GameMonitorDbContext())
                {
                    //does this return an int?
                    int userId = db.Users.Where(u => u.Username == username)
                                         .Where(u => u.Password == password)
                                         .Select(u => u.Id).Single();

                    if (userId > 0)
                    {
                        return userId;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
