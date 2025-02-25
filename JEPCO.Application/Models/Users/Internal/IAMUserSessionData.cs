using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEPCO.Application.Models.Users.Internal
{
    public class IAMUserSessionData
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public long Start { get; set; }
        public long LastAccess { get; set; }
        public bool RememberMe { get; set; }

        public Dictionary<string, string> Clients { get; set; }
    }
}
