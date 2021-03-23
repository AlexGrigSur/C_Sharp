using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace NavProject_Drawing
{
    class ServerConnect
    {
        HttpClient client = new HttpClient();
        public void Send()
        {

        }

        public void Auth(string login, string password)
        {
            client.GetAsync("");
        }
    }
}
