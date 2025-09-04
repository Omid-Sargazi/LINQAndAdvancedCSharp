namespace Patterns.StructuralPatterns
{
    public abstract class Component
    {
        public string Name { get; }

        public Component(string name)
        {
            Name = name;
        }

        public virtual void Add(Component component) => throw new NotSupportedException("add is not supporetd");
        public virtual void Remove(Component component) => throw new NotSupportedException("Remove is not supporetd");
        public virtual Component? GetChild(int index) => throw new NotSupportedException("GetChild is not supported");

        public abstract void Operation(int depth = 0);
    }

    public class Leaf : Component
    {
        public Leaf(string name) : base(name)
        {
        }

        public override void Operation(int depth = 0)
        {
            Console.WriteLine(new string('-', depth) + "Leaf: " + Name);
        }
    }

    public class Client
    {
        public static void Run()
        {
            var root = new Composite("Root");
            var leaf1 = new Leaf("Leaf A");
            var leaf2 = new Leaf("Leaf B");
            var subTree = new Composite("SubTree");
            subTree.Add(new Leaf("Leaf C1"));
            subTree.Add(new Leaf("Leaf C2"));

            root.Add(leaf1);
            root.Add(leaf2);
            root.Add(subTree);

            root.Operation();
        }
    }

    public class Composite : Component
    {
        private readonly List<Component> _children = new List<Component>();
       
        public Composite(string name) : base(name)
        {
        }

        public override void Add(Component component)
        {
            _children.Add(component);
        }

        public override void Remove(Component component)
        {
            _children.Remove(component);
        }

        public override Component? GetChild(int index)
        {
            if (index < 0 || index >= _children.Count) return null;
            return _children[index];
        }

        public override void Operation(int depth = 0)
        {
            Console.WriteLine(new string('-', depth) + " Composite: " + Name);

            foreach (var child in _children)
            {
                child.Operation(depth + 2);
            }
        }
    }
}