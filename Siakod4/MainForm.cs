using Siakod4.Figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        List<Vertice> selected;

        //костылим :)
        string searchFile = "search.txt";
        string cyclFile = "cycl.txt";
        string dijkstraFile = "dijkstra.txt";

        public MainForm()
        {
            InitializeComponent();
            vertices = new List<Vertice>();
            edges = new List<Edge>();
            table = new DataTable();
            selected = new List<Vertice>();
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
                    edges.Add(new Edge(first, second, (int)weightNum.Value));
                    first.Deselect();
                    second.Deselect();

                    selected.Remove(first);
                    selected.Remove(second);
                }
            }
        }

        private void graphPanel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var v in vertices)
                if (v.isPointInFigure(e.X, e.Y))
                {
                    if (v.Selected)
                    {
                        selected.Remove(v);
                        v.Deselect();
                    }
                    else
                    {
                        selected.Add(v);
                        v.Select();
                    }
                        

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
                        table.Rows[i][j] = vertices[i].getLink(vertices[j]).Weight;
                    else
                        table.Rows[i][j] = string.Empty;
        }

        string getStringVertices(IEnumerable<Vertice> list, string delimiter = ", ")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var elem in list)
                sb.Append(elem.ToString() + delimiter);

            if(list.Count() > 0)
                sb.Remove(sb.Length - delimiter.Length, delimiter.Length);

            return sb.ToString();
        }

        private bool DepthFirstSearch(IList<Vertice> vertices, StreamWriter sw, bool isVisit = true, bool isShow = true)
        {
            if (vertices.Count > 0)
            {
                var stack = new Stack<Vertice>();
                var visited = new List<Vertice>();               
                //посещаем первую вершину
                stack.Push(vertices[0]);
                visited.Add(vertices[0]);
                if (isVisit)
                {
                    sw.WriteLine($"Помещаем вершину {vertices[0]} в стек и помечаем ее как посещенную");
                    VisitNode(vertices[0], isShow);
                }           

                Vertice node;
                sw.WriteLine("Пока стек не пустой: ");
                while (stack.Count > 0)
                {
                    node = stack.Peek();
                    bool flag = false;
                    for (int i = 0; i < vertices.Count; i++)
                        if (node.HasLink(vertices[i]) && !visited.Contains(vertices[i]))
                        {
                            //посещаем вершину
                            visited.Add(vertices[i]);
                            stack.Push(vertices[i]);
                            
                            if (isVisit)
                            {
                                sw.WriteLine($"У вершины стека ({node}) находим первую смежную вершину ({vertices[i]}), заносим в стек и помечаем как посещенную. Текущий стек: [{getStringVertices(stack)}]");
                                VisitEdge(node, vertices[i], isShow);
                                VisitNode(vertices[i], isShow);
                            }
                            flag = true;
                            break;
                        }

                    if (!flag)
                    {
                        sw.Write($"Вершина стека ({node}) не имеет непосещенных смежных вершин, соответственно вынимаем ее из стека. ");
                        stack.Pop();
                        sw.WriteLine($"Текущий стек: [{getStringVertices(stack)}]");
                    }
                }
                sw.WriteLine($"Стек пуст. Завершаем алгоритм. Было посещено {visited.Count} из {vertices.Count} вершин");
                return visited.Count == vertices.Count;
            }
            return false;
        }

        /// <summary>
        /// Поиск в ширину
        /// </summary>
        private bool BreadthFirstSearch(IList<Vertice> vertices, StreamWriter sw, bool isVisit = true, bool isShow = true)
        {
            if (vertices.Count > 0)
            {
                var queue = new Queue<Vertice>();
                var visited = new List<Vertice>();

                //посещаем первую вершину
                queue.Enqueue(vertices[0]);
                visited.Add(vertices[0]);
                if(isVisit)
                {
                    sw.WriteLine($"Помещаем вершину {vertices[0]} в очередь. Помечаем эту вершину как посещенную");
                    VisitNode(vertices[0], isShow);
                }
                    

                Vertice node;
                sw.WriteLine("Пока очередь не пуста: ");
                while (queue.Count > 0)
                {
                    //убираем из очереди
                    node = queue.Dequeue();
                    sw.WriteLine($"Вынимаем из очереди текущую вершину ({node}). Ищем непосещенные смежные вершины текущей вершины в порядке возрастания: ");
                    //среди связанных ищем не посещенные в порядке возрастания
                    for (int i = 0; i < vertices.Count; i++)
                        if(node.HasLink(vertices[i]) && !visited.Contains(vertices[i]))
                        {
                            //посещаем вершину
                            visited.Add(vertices[i]);
                            queue.Enqueue(vertices[i]);
                            if (isVisit)
                            {
                                sw.WriteLine($"Заносим в очередь смежную вершину {vertices[i]} и помечаем ее как посещенную. Очередь: [{getStringVertices(queue)}]");
                                VisitEdge(node, vertices[i], isShow);
                                VisitNode(vertices[i], isShow);
                            } 
                        }
                    sw.WriteLine("Смежных непосещенных вершин больше нет у текущей вершины.");
                }
                sw.WriteLine($"Очередь пуста. Было посещено {visited.Count} из {vertices.Count} вершин. Завершаем алгоритм.");
                return visited.Count == vertices.Count;
            }
            return false;
        }

        private void EulerianPath(IList<Vertice> vertices, StreamWriter sw)
        {
            status.Text = "";
            var sw1 = new StreamWriter(searchFile);
            sw.Write("Проверяем граф на связность. ");
            //if (BreadthFirstSearch(vertices, sw1, false))
            if (DepthFirstSearch(vertices, sw1, false))
            {
                sw.WriteLine("Алгоритм обхода в глубину прошел все вершины графа. Граф связный.");

                sw.Write("Проверяем вершины на четность смежных вершин. ");
                //проверка четности вершин
                foreach (var v in vertices)
                {
                    if (v.Edges.Count % 2 != 0)
                    {
                        sw.Write($"Вершина {v.Text} имеет нечетное количество ребер. Граф не содержит эйлерова цикла, так как не все вершины имеют четную степень");
                        status.Text = "Граф не содержит эйлерова цикла, так как не все вершины имеют четную степень";
                        sw.Close();
                        return;
                    }
                }
                sw.WriteLine("Все вершины имеют четную степень.");
                sw.WriteLine("Начинаем поиск эйлерового цикла.");

                var stack = new Stack<Vertice>();
                var cycl = new Stack<Vertice>();

                stack.Push(vertices[0]);
                sw.WriteLine($"Помещаем во временный стек начальную вершину ({vertices[0]}).");
                sw.WriteLine("Пока временный стек не пустой: ");
                Vertice node;
                while (stack.Count > 0)
                {
                    node = stack.Peek();
                    var nodeEdges = new List<Edge>(node.GetNotDeletedEdges());
                    if (nodeEdges.Count() > 0)
                    {                       
                        var u = nodeEdges[0].GetLinkVertice(node);
                        stack.Push(u);
                        //удаляем ребро
                        nodeEdges[0].isDeleted = true;

                        sw.WriteLine($"У вершины стека ({node}) находим первую смежную вершину ({u}), заносим ее во временный стек. Ребро, соединяющее эти вершины, удаляем из графа. Временный стек: [{getStringVertices(stack)}]");
                    }
                    else
                    {
                        cycl.Push(stack.Pop());
                        sw.WriteLine($"Вершина стека ({node}) не имеет смежных вершин. Вынимаем ее из временного стека и помещаем в стек, хранящий эйлеров цикл [{getStringVertices(cycl)}].");

                    }
                }

                //возвращаем ребра
                foreach (var e in edges)
                    e.isDeleted = false;
                ShowCycl(cycl);

                sw.WriteLine($"Эйлеров цикл: {getStringVertices(cycl, " - ")}.");
            }
            else
            {
                sw.WriteLine("Граф не содержит эйлерова цикла, так как граф несвязный.");
                status.Text = "Граф не содержит эйлерова цикла, так как граф несвязный";
            }
            sw1.Close();
        }

        private void CanBeTree()
        {
            var sw = new StreamWriter(searchFile);
            if (BreadthFirstSearch(vertices, sw, true, false))
            {
                if(!edges.Where((e) => !e.Selected).Any())
                {
                    status.Text = "Граф уже является деревом";
                    return;
                }

                foreach (var v in vertices)
                {
                    DeselectAll();
                    v.TempRemoveSelf();

                    var tempVertices = new List<Vertice>(GetNotDeletedVertices());
                    if (BreadthFirstSearch(tempVertices, sw, true, false))
                    {
                        var tempEdges = GetNotDeletedEdges();
                        if (!tempEdges.Where((e) => !e.Selected).Any())
                        {
                            status.Text = $"Если удалить вершину {v.Text}, то граф станет деревом";
                            v.RestoreSelf();
                            statusObhod.Text = "";
                            graphPanel.Refresh();
                            return;
                        }
                    }

                    v.RestoreSelf();
                }

                status.Text = "Удалив одну вершину, нельзя получить дерево из заданного графа";
            } 
            else
            {
                status.Text = "Нельзя получить дерево из заданного графа, т.к. граф несвязный";
            }
            statusObhod.Text = "";
            graphPanel.Refresh();
        }

        private IEnumerable<Vertice> GetNotDeletedVertices()
        {
            foreach (var v in vertices)
                if (!v.isDeleted)
                    yield return v;
        }

        private IEnumerable<Edge> GetNotDeletedEdges()
        {
            foreach (var e in edges)
                if (!e.isDeleted)
                    yield return e;
        }

        private void ShowCycl(Stack<Vertice> cycl)
        {
            status.Text = "";
            Vertice temp = null;
            foreach (var c in cycl)
            {
                if(temp != null)
                    VisitEdge(temp, c);
                temp = c;
                status.Text += $"-> {c.Text} ";
            }
            graphPanel.Refresh();
        }

        private void VisitNode(Vertice v, bool isShow = true)
        {
            v.Select();
            statusObhod.Text += $"-> {v.Text} ";
            if (isShow)
            {
                graphPanel.Refresh();
                Thread.Sleep(700);
            } 
        }

        private void VisitEdge(Vertice v1, Vertice v2, bool isShow = true)
        {
            var edge = v1.getLink(v2);
            VisitEdge(edge, isShow);
        }

        private void VisitEdge(Edge edge, bool isShow = true)
        {
            edge.Select();
            if (isShow)
            {
                graphPanel.Refresh();
                Thread.Sleep(700);
            }
            
        }

        private void runObhod_Click(object sender, EventArgs e)
        {
            statusObhod.Text = "";
            var sw = new StreamWriter(searchFile);
            DepthFirstSearch(vertices, sw);
            //BreadthFirstSearch(vertices, sw);
            sw.Close();
        }

        private void deselectBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            graphPanel.Refresh();
        }

        private void DeselectAll()
        {
            selected.Clear();
            foreach (var v in vertices)
                v.Deselect();
            foreach (var edge in edges)
                edge.Deselect();
        }

        private void eCyclBtn_Click(object sender, EventArgs e)
        {
            var sw = new StreamWriter(cyclFile);
            EulerianPath(vertices, sw);
            sw.Close();
        }

        private void shortestPathBtn_Click(object sender, EventArgs e)
        {
            if(selected.Count >= 2)
            {
                var start = int.Parse(selected[0].Text) - 1;
                var end = int.Parse(selected[1].Text) - 1;
                DeselectAll();

                var size = vertices.Count;
                int[,] matrix = new int[size, size];

                //копируем матрицу смежности
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        if (table.Rows[i][j].ToString() != "")
                            matrix[i, j] = int.Parse(table.Rows[i][j].ToString());
                        else
                            matrix[i, j] = -1;

                using (var sw = new StreamWriter(dijkstraFile))
                {
                    sw.WriteLine("Матрица смежности графа: ");
                    for (int i = 0; i < size; i++)
                    {
                        sw.Write("[");
                        for (int j = 0; j < size; j++)
                        {
                            if(matrix[i, j] == -1)
                                sw.Write("-");
                            else
                                sw.Write($"{matrix[i, j]}");

                            if (j != size - 1)
                                sw.Write(", ");
                        }
                        sw.WriteLine("]");
                    }
                        

                   ShortestPath(start, end, matrix, sw);
                }
                graphPanel.Refresh();
            }     
        }

        string stringInts(IEnumerable<int> array, string delimiter = ", ", bool isPlus1 = false)
        {
            var sb = new StringBuilder();

            foreach (var el in array)
                if(isPlus1)
                    sb.Append(stringNum(el+1) + delimiter);
                else
                    sb.Append(stringNum(el) + delimiter);

            if(array.Count() > 0)
                sb.Remove(sb.Length - delimiter.Length, delimiter.Length);
            return sb.ToString();
        }

        string stringNum(int num)
        {
            if (num != int.MaxValue)
                return num.ToString();
            else
                return "inf";
        }

        private void ShortestPath(int start, int end, int[,] matrix, StreamWriter sw)
        {
            sw.WriteLine($"Начальная вершина - {start+1}, конечная - {end+1}.");
            status.Text = "";
            int size = matrix.GetLength(0);
            //посещенные вершины
            bool[] visited = new bool[size];
            int visitedCount = 0;

            //метки вершин
            int[] dist = new int[size];
            for (int i = 0; i < size; i++)
            {
                dist[i] = int.MaxValue;
            }
            sw.WriteLine($"Задаем метки всех вершин бесконечностью: [{stringInts(dist)}].");

            //находим расстояния от начальной вершины к остальным
            dist[start] = 0;
            sw.WriteLine($"Метку начальной вершины {start+1} задаем нулем.");
            sw.WriteLine($"Метки: [{stringInts(dist)}]");

            int current = start;
            while (visitedCount <= size)
            {
                visited[current] = true;
                visitedCount++;

                sw.Write($"Посещаем вершину {current+1}. ");

                if (current == end)
                {
                    sw.WriteLine($"Конечная вершина посещена. Длина пути = {dist[end]}.");
                    break;
                }

                sw.WriteLine($"Пересчитываем метки непосещенных смежных вершин: ");
                bool flag = false;
                for (int i = 0; i < size; i++)
                    if(!visited[i] && matrix[current, i] != -1)
                    {
                        flag = true;
                        sw.Write($"Метка вершины {i + 1} = min({stringNum(dist[current])} + {matrix[current, i]}, {stringNum(dist[i])}) = ");
                        var newDist = dist[current] + matrix[current, i];
                        dist[i] = Math.Min(newDist, dist[i]);

                        sw.WriteLine($"{stringNum(dist[i])}.");
                    }
                if(!flag)
                    sw.WriteLine($"Смежных непосещенных вершин нет.");

                sw.WriteLine($"Метки: [{stringInts(dist)}]");

                int min = int.MaxValue;
                for (int i = 0; i < size; i++)
                    if(!visited[i] && dist[i] < min)
                    {
                        min = dist[i];
                        current = i;
                    }

                sw.WriteLine($"Минимальное значение метки среди непосещенных вершин = {min}, соответственно текущая вершина = {current+1}");
            }       

            if (dist[end] == int.MaxValue)
            {
                sw.WriteLine($"Между вершинами {start + 1} и {end + 1} нет пути.");
                status.Text += $"Между вершинами {start + 1} и {end + 1} нет пути";
                return;
            }


            //восстанавливаем путь
            sw.WriteLine("Восстанавливаем путь с конца: ");
            sw.WriteLine($"Добавляем в стек пути конечную вершину вершину {end+1}.");
            var path = new Stack<int>();
            path.Push(end);
            while (path.Peek() != start)
            {
                sw.WriteLine($"Текущая вершина {path.Peek()+1}. Ищем подходящую смежную вершину: ");
                int i;
                for (i = 0; i < size; i++)
                    if (matrix[path.Peek(), i] != -1)
                    {
                        sw.Write($"({i+1}): ");
                        var temp = dist[path.Peek()] - matrix[path.Peek(), i];
                        if (dist[i] == temp)
                        {
                            sw.WriteLine($"Метка вершины {i+1} = {stringNum(dist[i])} = метка вершины ({path.Peek()+1}) - длина ребра ({path.Peek()+1}-{i+1}) = {dist[path.Peek()]}-{matrix[path.Peek(), i]} = {stringNum(dist[i])}. Подходящая вешина найдена. Добавляем вершину {i+1} в стек пути. Путь: [{stringInts(path, isPlus1: true)}]");
                            path.Push(i);
                            break;
                        }
                        else
                            sw.WriteLine($"Метка вершины {i + 1} = {stringNum(dist[i])} != метка вершины ({path.Peek() + 1}) - длина ребра ({path.Peek() + 1}-{i + 1}) = {dist[path.Peek()]}-{matrix[path.Peek(), i]} = {stringNum(temp)}. Не подходит.");
                    }
                          
            }

            sw.WriteLine($"Вершина {start+1} является начальной. Путь восстановлен.");
            sw.WriteLine($"Кратчайший путь между вершинами {start+1} и {end+1}: {stringInts(path, "-", true)}. Длина пути = {dist[end]}.");

            foreach (var v in path)
                status.Text += $"<- {v + 1}";

            int sum = dist[end];
            status.Text += $" | Расстояние = {sum}";
        }
    }
}
