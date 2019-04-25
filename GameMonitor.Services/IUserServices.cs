using System;
using System.Collections.Generic;
using System.Text;

namespace GameMonitor.Services
{
    public interface IUserServices
    {
        int Authenticate(string userName, string password);
    }
}
