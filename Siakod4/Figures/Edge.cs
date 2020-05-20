using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siakod4.Figures
{
    public class Edge : Figure
    {
        Vertice First;
        Vertice Second;
       
        public int Weight;
        public bool isDeleted = false;

        public Edge(Vertice first, Vertice second, int weight)
        {
            First = first;
            Second = second;
            Weight = weight;

            First.AddEdge(this);
            Second.AddEdge(this);
        }

        public override bool isPointInFigure(int x, int y)
        {
            var x1 = First.X;
            var y1 = First.Y;
            var x2 = Second.X;
            var y2 = Second.Y;
            var distance =
                Math.Abs((y2 - y1) * x - (x2 - x1) * y + x2 * y1 - y2 * x1)
                / Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));

            return distance < 5;
        }

        public Vertice GetLinkVertice(Vertice current)
        {
            if (current == First)
                return Second;
            if (current == Second)
                return First;

            return null;
        }

        public bool HasVertice(Vertice vertice)
        {
            return vertice == First || vertice == Second;
        }

        public override void Paint(Graphics g)
        {
            var pen = new Pen(Color.Black, 3);

            if (Selected)
                pen.Color = Color.Red;

            g.DrawLine(pen, First.X, First.Y, Second.X, Second.Y);

            var font = new Font(FontFamily.GenericSansSerif, 16);
            var x = Math.Min(First.X, Second.X) + Math.Abs(Second.X - First.X) / 2;
            var y = Math.Min(First.Y, Second.Y) + Math.Abs(Second.Y - First.Y) / 2;
            g.DrawString(Weight.ToString(), font, new SolidBrush(Color.Black), x + 8, y);
        }

        public void DeleteSelf()
        {
            First.RemoveEdge(this);
            Second.RemoveEdge(this);
            isDeleted = true;
        }
    }
}
