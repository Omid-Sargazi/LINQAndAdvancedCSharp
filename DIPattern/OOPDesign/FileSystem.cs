namespace DIPattern.OOPDesign
{
    public interface IFileSystem:IFileDeletable,IFileOpenable,IFileSavable
    {
        
    }

    public class AudioFile : IFileSystem
    {
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class ImageFile : IFileSystem
    {
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class ReadOnlyConfigFile : IFileOpenable, IFileDeletable
    {
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }

    public interface IFileOpenable
    {
        void Open();
    }

    public interface IFileSavable
    {
        void Save();
    }

    public interface IFileDeletable
    {
        void Delete();
    }
}