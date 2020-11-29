using System;
using System.Collections.Generic;
using System.Drawing;
using NavTest;

namespace NavTestNoteBookNeConsolb
{
    class DrawClass
    {
        public DrawClass(int _radius)
        {
            radius = _radius;
        }
        private int radius;
        public Image LoadLevel(int currentLevel, ref Map map, out int panelX, out int panelY)
        {
            Level floor = map.GetFloor(currentLevel);

            panelX = (floor.ScreenResX == int.MaxValue) ? 900 : floor.ScreenResX;
            panelY = (floor.ScreenResY == int.MaxValue) ? 700 : floor.ScreenResY;

            Image picture = new Bitmap(panelX, panelY);


            foreach (Node nodeIter in floor.GetNodeListOnFloor().Keys) // draw all Nodes
                picture = DrawNode(picture, floor.GetNodeOnFloor(nodeIter)[0], floor.GetNodeOnFloor(nodeIter)[1], 255, ((nodeIter.type == 0) ? "" : nodeIter.name)); // ternary operator to not draw text on corridor nodes
            
            Dictionary<Node, List<Node>> edgesCopy = new Dictionary<Node, List<Node>>(map.GetFloor(currentLevel).GetEdgesList());

            Dictionary<Node, List<Node>> removed = new Dictionary<Node, List<Node>>();

            foreach (Node FirstNodeLine in edgesCopy.Keys) // draw All 
                foreach (Node SecondNodeLine in edgesCopy[FirstNodeLine])
                {
                    picture = DrawLine(picture, floor.GetNodeOnFloor(FirstNodeLine)[0], floor.GetNodeOnFloor(FirstNodeLine)[1], (FirstNodeLine.type == 0) ? "" : FirstNodeLine.name, floor.GetNodeOnFloor(SecondNodeLine)[0], floor.GetNodeOnFloor(SecondNodeLine)[1], (SecondNodeLine.type == 0) ? "" : SecondNodeLine.name);
                    if (!removed.ContainsKey(SecondNodeLine)) removed.Add(SecondNodeLine, new List<Node>());
                    removed[SecondNodeLine].Add(FirstNodeLine);
                }

            foreach (Node i in removed.Keys)
                foreach (Node j in removed[i])
                    map.GetFloor(currentLevel).AddSingleEdge(i, j);

            edgesCopy.Clear();
            removed.Clear();

            return picture;
        }
        public Image DrawNode(Image picture, int X, int Y, int transparent = 255, string nodeName = "")
        {
            using (Graphics G = Graphics.FromImage(picture))
            {
                Color colorFirst, colorSecond;
                colorFirst = Color.FromArgb(transparent, Color.Black);
                colorSecond = Color.FromArgb(transparent, Color.Orange);
                Pen pen = new Pen(colorFirst);
                Brush brush = new SolidBrush(colorFirst);
                G.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
                G.FillEllipse(brush, X - radius, Y - radius, 2 * radius, 2 * radius);
                brush = new SolidBrush(colorSecond);
                G.FillRectangle(brush, X, Y, 1, 1);
                if (nodeName != "") G.DrawString(nodeName.Substring(0, ((nodeName.Length < 4) ? nodeName.Length : 4)), new Font("Microsoft Sans Serif", 15f), new SolidBrush(colorFirst), X + radius + 2, Y - radius);
            }
            return picture;
        }
        public Image DrawLine(Image picture, int X1, int Y1, string nodeName1, int X2, int Y2, string nodeName2)
        {
            using (Graphics G = Graphics.FromImage(picture))
            {
                Color lineColor;
                lineColor = Color.Purple;
                G.DrawLine(new Pen(lineColor), X1, Y1, X2, Y2);
            }
            picture = DrawNode(picture, X1, Y1, 255, nodeName1);
            picture = DrawNode(picture, X2, Y2, 255, nodeName2);

            return picture;
        }
        public Image HighlighterNode(Image picture, int X, int Y)
        {
            using (Graphics G = Graphics.FromImage(picture))
            {
                Pen pen = new Pen(Color.Red);
                G.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
            }
            return picture;
        }
        public List<int> SearchNodesOnScreen(Image picture, int e_X, int e_Y, int radius)
        {
            List<int> coord = new List<int>();
            bool returnFlag = false;
            using (Bitmap bmp = new Bitmap(picture, picture.Width, picture.Height))
            {
                for (int y = e_Y - radius; y >= 0 && y <= picture.Height && y <= e_Y + radius && !returnFlag; ++y)
                    for (int x = e_X - radius; x >= 0 && x <= picture.Width && x <= e_X + radius; ++x)
                        if (bmp.GetPixel(x, y).ToArgb() == Color.Orange.ToArgb())
                        {
                            coord.Add(x);
                            coord.Add(y);
                            returnFlag = true;
                            break;
                        }
            }
            return coord;
        }
    }
}
