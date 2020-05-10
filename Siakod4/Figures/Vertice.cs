﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siakod4.Figures
{
    public class Vertice : Figure
    {
        public int X;
        public int Y;

        public int Width;
        public int Height;

        public string Text;
        List<Edge> Edges;


        public Vertice(int x, int y, int width, int height, string text)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Text = text;

            Edges = new List<Edge>();
        }

        public void RemoveEdge(Edge edge)
        {
            Edges.Remove(edge);
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public bool HasLink(Vertice vertice)
        {
            if (vertice == this)
                return false;

            foreach (var edge in Edges)
                if (edge.HasVertice(vertice))
                    return true;

            return false;
        }

        public Edge getLink(Vertice vertice)
        { 
            foreach (var edge in Edges)
                if (edge.HasVertice(vertice))
                    return edge;

            return null;
        }

        public void RemoveLinks()
        {
            while(Edges.Count > 0)
                Edges[0].DeleteSelf();
        }


        public override bool isPointInFigure(int x, int y)
        {
            return Math.Pow(x - this.X, 2) / Math.Pow(Width / 2, 2) + Math.Pow(y - this.Y, 2) / Math.Pow(Height / 2, 2) <= 1;
        }

        public override void Paint(Graphics g)
        {
            var brush = new SolidBrush(Color.LightGreen);
            var x1 = X - (Width / 2);
            var y1 = Y - (Height / 2);

            g.FillEllipse(brush, x1, y1, Width, Height);

            var font = new Font(FontFamily.GenericSansSerif, 16);
            g.DrawString(Text, font, new SolidBrush(Color.Black), X - 8, Y - 8);

            if (Selected)
            {
                var pen = new Pen(Color.Red, 3);
                g.DrawEllipse(pen, x1, y1, Width, Height);
            }
        }
    }
}