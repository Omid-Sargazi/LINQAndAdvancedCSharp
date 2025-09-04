namespace Patterns.StructuralPatterns
{
    public interface IFileSystemComponent
    {
        string Name { get; }
        long GetSize();
        void Display(string indent = " ");
    }

    public class File : IFileSystemComponent
    {

        public string Name { get; }
        private long _size;

        public File(string name, long size)
        {
            Name = name;
            _size = size;
        }

        public void Display(string indent = " ")
        {
            Console.WriteLine($"{indent} {Name}{_size} bytes");
        }

        public long GetSize()
        {
            return _size;
        }
    }

    public class Directory : IFileSystemComponent
    {
        public string Name { get; }
        private List<IFileSystemComponent> _children = new List<IFileSystemComponent>();

        public Directory(string name)
        {
            Name = name;
        }

        public void AddComponent(IFileSystemComponent component)
        {
            _children.Add(component);
        }

        public void RemoveComponent(IFileSystemComponent component)
        {
            _children.Remove(component);
        }

        public void Display(string indent = " ")
        {
            Console.WriteLine($"{indent} {Name}");
            foreach (var component in _children)
            {
                component.Display(indent + " ");
            }
        }

        public long GetSize()
        {
            long totalSize = 0;
            foreach (var component in _children)
            {
                totalSize += component.GetSize();
            }
            return totalSize;
        }

        
    }

    public class ClientComponent
    {
        public static void Run()
        {
            var file1 = new File("tetx1", 1500);
            var file2 = new File("image", 1600);
            var file3 = new File("image", 1700);
            var file4 = new File("data", 1500);
            var file5 = new File("notes", 1500);

            var documents = new Directory("Document");
            var images = new Directory("images");
            var root = new Directory("root");

            documents.AddComponent(file1);
            documents.AddComponent(file4);
            documents.AddComponent(file5);
            images.AddComponent(file2);
            images.AddComponent(file3);

            root.AddComponent(documents);
            root.AddComponent(images);

            Console.WriteLine("Structure of files");
            root.Display();
            Console.WriteLine($"\n📊 Total size: {root.GetSize()} bytes");
        }
    }
}