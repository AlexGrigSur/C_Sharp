using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Navigator.Structures;

namespace NavProject_Navigator.CalcFunctions
{
    interface IRouteAlg
    {
        public List<Node> Calc(Map map, ConnectivityComponents currentComponent, Node startNode, Node endNode);
    }
}
