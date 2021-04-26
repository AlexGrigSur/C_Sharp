using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Drawing.Structures;

namespace NavProject_Drawing.MainForm
{
    class DrawClass
    {
        public DrawClass(int _radius) => radius = _radius;

        private int radius;
        //public Image LoadLevel(int currentLevel, ref Map map, out int panelX, out int panelY)
        //{
        //    Level floor = map.GetFloor(currentLevel);

        //    panelX = (floor.ScreenResX - 100 == int.MaxValue) ? 900 : floor.ScreenResX;
        //    panelY = (floor.ScreenResY - 100 == int.MaxValue) ? 700 : floor.ScreenResY;

        //    Image picture = new Bitmap(panelX, panelY);


        //    foreach (Node nodeIter in floor.GetNodeListOnFloor().Keys) // draw all Nodes
        //        picture = DrawNode(picture, floor.GetNodeOnFloor(nodeIter).X, floor.GetNodeOnFloor(nodeIter).Y, 255, ((nodeIter.type == 0) ? "" : nodeIter.name)); // ternary operator to not draw text on corridor nodes

        //    Dictionary<Node, List<Node>> edgesCopy = new Dictionary<Node, List<Node>>(map.GetFloor(currentLevel).GetEdgesList());

        //    Dictionary<Node, List<Node>> removed = new Dictionary<Node, List<Node>>();

        //    foreach (Node FirstNodeLine in edgesCopy.Keys) // draw All 
        //        foreach (Node SecondNodeLine in edgesCopy[FirstNodeLine])
        //        {
        //            picture = DrawLine(picture, floor.GetNodeOnFloor(FirstNodeLine).X, floor.GetNodeOnFloor(FirstNodeLine).Y, (FirstNodeLine.type == 0) ? "" : FirstNodeLine.name, floor.GetNodeOnFloor(SecondNodeLine).X, floor.GetNodeOnFloor(SecondNodeLine).Y, (SecondNodeLine.type == 0) ? "" : SecondNodeLine.name);
        //            if (!removed.ContainsKey(SecondNodeLine)) removed.Add(SecondNodeLine, new List<Node>());
        //            removed[SecondNodeLine].Add(FirstNodeLine);
        //        }

        //    foreach (Node i in removed.Keys)
        //        foreach (Node j in removed[i])
        //            map.GetFloor(currentLevel).AddSingleEdge(i, j);

        //    edgesCopy.Clear();
        //    removed.Clear();

        //    return picture;
        //}
        //public Image DrawNode(Image picture, int X, int Y, int transparent = 255, string nodeName = "")
        //{
        //    using (Graphics G = Graphics.FromImage(picture))
        //    {
        //        Color colorFirst, colorSecond;
        //        colorFirst = Color.FromArgb(transparent, Color.Black);
        //        colorSecond = Color.FromArgb(transparent, Color.Orange);
        //        Pen pen = new Pen(colorFirst);
        //        Brush brush = new SolidBrush(colorFirst);
        //        G.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
        //        G.FillEllipse(brush, X - radius, Y - radius, 2 * radius, 2 * radius);
        //        brush = new SolidBrush(colorSecond);
        //        G.FillRectangle(brush, X, Y, 1, 1);
        //        if (nodeName != "") G.DrawString(nodeName.Substring(0, ((nodeName.Length < 4) ? nodeName.Length : 4)), new Font("Microsoft Sans Serif", 15f), new SolidBrush(colorFirst), X + radius + 2, Y - radius);
        //    }
        //    return picture;
        //}
        //public Image DrawLine(Image picture, int X1, int Y1, string nodeName1, int X2, int Y2, string nodeName2)
        //{
        //    using (Graphics G = Graphics.FromImage(picture))
        //    {
        //        Color lineColor;
        //        lineColor = Color.Purple;
        //        G.DrawLine(new Pen(lineColor, 4), X1, Y1, X2, Y2);
        //    }
        //    picture = DrawNode(picture, X1, Y1, 255, nodeName1);
        //    picture = DrawNode(picture, X2, Y2, 255, nodeName2);

        //    return picture;
        //}
        //public Image HighlighterNode(Image picture, int X, int Y)
        //{
        //    using (Graphics G = Graphics.FromImage(picture))
        //    {
        //        Pen pen = new Pen(Color.Red);
        //        G.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
        //    }
        //    return picture;
        //}
        //public List<int> SearchNodesOnScreen(Image picture, int e_X, int e_Y, int radius)
        //{
        //    List<int> coord = new List<int>();
        //    bool returnFlag = false;
        //    using (Bitmap bmp = new Bitmap(picture, picture.Width, picture.Height))
        //    {
        //        for (int y = e_Y - radius; y >= 0 && y <= picture.Height && y <= e_Y + radius && !returnFlag; ++y)
        //            for (int x = e_X - radius; x >= 0 && x <= picture.Width && x <= e_X + radius; ++x)
        //                if (bmp.GetPixel(x, y).ToArgb() == Color.Orange.ToArgb())
        //                {
        //                    coord.Add(x);
        //                    coord.Add(y);
        //                    returnFlag = true;
        //                    break;
        //                }
        //    }
        //    return coord;
        //}

        //public Image RouteBuilder(Image picture, List<Point> nodes)
        //{
        //    using (Graphics G = Graphics.FromImage(picture))
        //    {
        //        Pen pen = new Pen(Color.FromArgb(155, Color.Red));
        //        Brush brush = new SolidBrush(Color.FromArgb(155, Color.Red));
        //        foreach (var i in nodes)
        //        {
        //            G.DrawEllipse(pen, i.X - radius, i.Y - radius, 2 * radius, 2 * radius);
        //            G.FillEllipse(brush, i.X - radius, i.Y - radius, 2 * radius, 2 * radius);
        //        }

        //        pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        //        pen.Width = 6.0F;

        //        for (int i = 0; i < nodes.Count - 1; ++i)
        //        {
        //            float xDiff = nodes[i + 1].X - nodes[i].X;
        //            float yDiff = nodes[i + 1].Y - nodes[i].Y;
        //            double angle = Math.Atan2(yDiff, xDiff);


        //            double distance = Math.Sqrt((nodes[i + 1].X - nodes[i].X) * (nodes[i + 1].X - nodes[i].X) + (nodes[i + 1].Y - nodes[i].Y) * (nodes[i + 1].Y - nodes[i].Y)) - radius + 2;

        //            int newCoordX = nodes[i].X + Convert.ToInt32(Math.Cos(angle) * distance);
        //            int NewCoordY = nodes[i].Y + Convert.ToInt32(Math.Sin(angle) * distance);

        //            Point newPoint = new Point(newCoordX, NewCoordY);
        //            G.DrawLine(pen, nodes[i].X, nodes[i].Y, newPoint.X, newPoint.Y);
        //        }
        //    }
        //    return picture;
        //}
    }
}
