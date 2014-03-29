using System;
using System.Linq;
using DataAccess.Model;

namespace DataAccess
{
    public class FileRepository : IFileRepository
    {

        public void AddFileDescriptions(FileResult fileResult)
        {
            using (var db = new FileContext())
            {
                for(int i=0; i< fileResult.FileNames.Count(); i++)
                {
                    var fileDescription = new FileDescription
                    {
                        ContentType = fileResult.ContentTypes[i],
                        FileName = fileResult.FileNames[i],
                        Name = fileResult.Names[i],
                        CreatedTimestamp = fileResult.CreatedTimestamp,
                        UpdatedTimestamp = fileResult.UpdatedTimestamp,
                        Description = fileResult.Description
                    };

                    db.FileDescriptions.Add(fileDescription);
                }

                db.SaveChanges();

            }
        }

    }
}

