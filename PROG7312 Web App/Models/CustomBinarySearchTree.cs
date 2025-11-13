namespace PROG7312_Web_App.Models
{
    public class CustomBinarySearchTree
    {
        private class Node
        {
            public ServiceRequest Data { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(ServiceRequest data)
            {
                Data = data;
                Left = null;
                Right = null;
            }
        }

        private Node? _root;

        public CustomBinarySearchTree()
        {
            _root = null;
        }

        public void Insert(ServiceRequest data)
        {
            _root = InsertRecursive(_root, data);
        }


        private Node InsertRecursive(Node? node, ServiceRequest data)
        {
            if (node == null)
            {
                return new Node(data);
            }

            if (data.Id.CompareTo(node.Data.Id) < 0)
            {
                node.Left = InsertRecursive(node.Left, data);
            }
            else if (data.Id.CompareTo(node.Data.Id) > 0)
            {
                node.Right = InsertRecursive(node.Right, data);
            }

            return node;
        }

        public ServiceRequest? Find(Guid id)
        {
            return FindRecursive(_root, id);
        }

        private ServiceRequest? FindRecursive(Node? node, Guid id)
        {
            if (node == null)
            {
                return null;
            }

            if (id.CompareTo(node.Data.Id) == 0)
            {
                return node.Data;
            }

            if (id.CompareTo(node.Data.Id) < 0)
            {
                return FindRecursive(node.Left, id);
            }

            return FindRecursive(node.Right, id);
        }

        public List<ServiceRequest> GetAllSorted()
        {
            var list = new List<ServiceRequest>();
            InOrderTraversal(_root, list);
            return list;
        }

        private void InOrderTraversal(Node? node, List<ServiceRequest> list)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, list);
                list.Add(node.Data);
                InOrderTraversal(node.Right, list);
            }
        }
    }
}
