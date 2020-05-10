using Siakod4.Figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Siakod4
{
    public partial class MainForm : Form
    {
        List<Vertice> vertices;
        List<Edge> edges;
        DataTable table;

        public MainForm()
        {
            InitializeComponent();
            vertices = new List<Vertice>();
            edges = new List<Edge>();
            table = new DataTable();
            dataGrid.DataSource = table;

            //механизм рефлексии
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic, null, graphPanel, new object[] { true });
        }

        private void AddVertice(int x, int y)
        {
            vertices.Add(new Vertice(x, y, 70, 70, (vertices.Count + 1).ToString()));
        }

        private void AddEdge()
        {
            var stack = new Stack<Vertice>();

            foreach (var v in vertices)
                if (v.Selected)
                    stack.Push(v);
           
            if(stack.Count >= 2)
            {
                var first = stack.Pop();
                var second = stack.Pop();

                if(!first.HasLink(second))
                {
                    edges.Add(new Edge(first, second));
                    first.Deselect();
                    second.Deselect();
                }
            }
        }

        private void graphPanel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var v in vertices)
                if (v.isPointInFigure(e.X, e.Y))
                {
                    v.RevertSelection();
                    graphPanel.Refresh();
                    return;
                }
            foreach (var edge in edges)
                if (edge.isPointInFigure(e.X, e.Y))
                {
                    edge.RevertSelection();
                    graphPanel.Refresh();
                    return;
                }

            AddVertice(e.X, e.Y);
            RefreshTable();
            graphPanel.Refresh();
        }

        private void graphPanel_Paint(object sender, PaintEventArgs e)
        {
            foreach (var edge in edges)
                edge.Paint(e.Graphics);

            foreach (var v in vertices)
                v.Paint(e.Graphics);
        }

        private void joinBtn_Click(object sender, EventArgs e)
        {
            AddEdge();
            RefreshTable();
            graphPanel.Refresh();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < vertices.Count; )
            {
                if (vertices[i].Selected)
                {
                    vertices[i].RemoveLinks();
                    vertices.RemoveAt(i);
                }
                else
                    i++;
            }

            for (int i = 0; i < edges.Count; )
            {
                if (edges[i].Selected || edges[i].isDeleted)
                {
                    edges[i].DeleteSelf();
                    edges.RemoveAt(i);
                }
                else
                    i++;
            }

            RefreshTable();
            graphPanel.Refresh();
        }

        private void RefreshTable()
        {
            if(table.Rows.Count != vertices.Count)
            {
                //очищаем все
                table.Columns.Clear();
                table.Rows.Clear();

                //добавляем столбцы
                for (int i = 0; i < vertices.Count; i++)
                    table.Columns.Add(new DataColumn((i + 1).ToString()));

                //добавляем строки
                for (int i = 0; i < vertices.Count; i++)
                {
                    table.Rows.Add(table.NewRow());
                    //задаем заголовок строки
                    dataGrid.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }

            //отображаем связи
            var length = vertices.Count;
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    if (vertices[i].HasLink(vertices[j]))
                    {
                        table.Rows[i][j] = '+';
                        table.Rows[j][i] = '+';
                    }
                    else
                    {
                        table.Rows[i][j] = string.Empty;
                        table.Rows[j][i] = string.Empty;
                    }
        }

        /// <summary>
        /// Поиск в ширину
        /// </summary>
        private bool BreadthFirstSearch(IList<Vertice> vertices)
        {
            if(vertices.Count > 0)
            {
                var queue = new Queue<Vertice>();
                var visited = new List<Vertice>();

                //посещаем первую вершину
                queue.Enqueue(vertices[0]);
                visited.Add(vertices[0]);
                VisitNode(vertices[0]);

                Vertice node;
                while (queue.Count > 0)
                {
                    //убираем из очереди
                    node = queue.Dequeue();
                    //среди связанных ищем не посещенные в порядке возрастания
                    for (int i = 0; i < vertices.Count; i++)
                        if(node.HasLink(vertices[i]) && !visited.Contains(vertices[i]))
                        {
                            //посещаем вершину
                            visited.Add(vertices[i]);
                            queue.Enqueue(vertices[i]);
                            VisitEdge(node, vertices[i]);
                            VisitNode(vertices[i]);
                        }
                }

                return visited.Count == vertices.Count;
            }
            return false;
        }

        private void VisitNode(Vertice v)
        {
            v.Select();
            statusObhod.Text += $"-> {v.Text} ";
            graphPanel.Refresh();
            Thread.Sleep(700);
        }

        private void VisitEdge(Vertice v1, Vertice v2)
        {
            var edge = v1.getLink(v2);
            VisitEdge(edge);
        }

        private void VisitEdge(Edge edge)
        {
            edge.Select();
            graphPanel.Refresh();
            Thread.Sleep(700);
        }

        private void runObhod_Click(object sender, EventArgs e)
        {
            statusObhod.Text = "";
            BreadthFirstSearch(vertices);
        }

        private void deselectBtn_Click(object sender, EventArgs e)
        {
            foreach (var v in vertices)
                v.Deselect();
            graphPanel.Refresh();
        }
    }
}
