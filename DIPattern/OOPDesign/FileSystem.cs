namespace DIPattern.OOPDesign
{
    public interface IFileSystem
    {
        void Open();
        void Delete();
        void Save();
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
}