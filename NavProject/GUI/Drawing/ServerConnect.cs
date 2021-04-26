using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace NavProject_Drawing
{
    interface IGetCommand
    {
        public HttpResponseMessage Send();
    }
    interface IPostCommand
    {
        public HttpResponseMessage Send();
    }
    interface IPutCommand
    {
        public HttpResponseMessage Send();
    }
    interface IDeleteCommand
    {
        public HttpResponseMessage Send();
    }

    class ServerConnect
    {
        HttpClient client = new HttpClient();
        public HttpResponseMessage Get(IGetCommand command)
        {
            client.Timeout = TimeSpan.FromSeconds(100);
            command.Send();
        }
        public HttpResponseMessage Post(IPostCommand command)
        {
           HttpResponseMessage responseMessage = command.Send();
        }
        public HttpResponseMessage Put(IPutCommand command)
        {
            HttpResponseMessage responseMessage = command.Send();
        }
        public HttpResponseMessage Delete(IDeleteCommand command)
        {
            command.Send();
        }
    }
}
