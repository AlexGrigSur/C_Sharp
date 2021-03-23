using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Navigator.Structures;

namespace NavProject_Navigator.CalcFunctions
{
    public struct ComponentsToCalc
    {
        public ConnectivityComponents connectivityComponent;
        public Node startNode;
        public Node endNode;
        public ComponentsToCalc(ConnectivityComponents _ConnectivityComponents, Node _startNode, Node _EndNode)
        {
            connectivityComponent = _ConnectivityComponents;
            startNode = _startNode;
            endNode = _EndNode;
        }
    }
}
