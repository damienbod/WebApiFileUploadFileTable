using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccess;
using DataAccess.Model;

namespace WebAPIDocumentationHelp.Controllers
{
    [RoutePrefix("api/test")]
    public class FileUploadController : ApiController
    {
        private IFileRepository _fileRepository = new FileRepository();
        private static readonly string ServerUploadFolder = "\\\\N275\\mssqlserver\\WebApiFileTable\\WebApiUploads_Dir"; //Path.GetTempPath();

        [Route("files")]
        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileResult> UploadFiles()
        {
            var streamProvider = new MultipartFormDataStreamProvider(ServerUploadFolder);
            await Request.Content.ReadAsMultipartAsync(streamProvider);

           
            var files =  new FileResult
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName.Replace(ServerUploadFolder + "\\","")).ToList(),
                Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName).ToList(),
                ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType).ToList(),
                Description = streamProvider.FormData["description"],
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow, 
                DownloadLink = "TODO, will implement when file is persisited"
            };
            _fileRepository.AddFileDescriptions(files);
            return files;
        }
    }
}

