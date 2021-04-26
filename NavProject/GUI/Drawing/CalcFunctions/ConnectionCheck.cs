using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NavProject_Drawing.Structures;

namespace NavProject_Drawing.CalcFunctions
{
    class ConnectionCheck
    {
        public bool isNavAble { get; set; }
        public ConnectionCheck(Map map) => Manager(map);
        public async void Manager(Map map)
        {
            //await Task.Run(() =>
            //{
            //    new DijkstraForConnectivitySplit().Run(map);
            //    isNavAble = new DijkstraForMapConnectivity().Run(map);
            //});
        }
    }
}
