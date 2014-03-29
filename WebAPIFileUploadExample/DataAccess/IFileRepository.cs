using DataAccess.Model;

namespace DataAccess
{
    public interface IFileRepository
    {
        void AddFileDescriptions(FileResult fileResult);
    }
}
