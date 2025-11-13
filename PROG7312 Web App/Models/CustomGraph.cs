namespace PROG7312_Web_App.Models
{
    public class CustomGraph<T> where T : notnull
    {
        private readonly Dictionary<T, List<T>> _adjacencyList;

        public CustomGraph()
        {
            _adjacencyList = new Dictionary<T, List<T>>();
        }

        public void AddNode(T node)
        {
            if (!_adjacencyList.ContainsKey(node))
            {
                _adjacencyList[node] = new List<T>();
            }
        }

        public void AddEdge(T from, T to)
        {
            AddNode(from);
            AddNode(to);

            if (!_adjacencyList[from].Contains(to))
            {
                _adjacencyList[from].Add(to);
            }
        }

        public List<T> GetNeighbors(T node)
        {
            if (_adjacencyList.ContainsKey(node))
            {
                return _adjacencyList[node];
            }
            return new List<T>();
        }
    }
}