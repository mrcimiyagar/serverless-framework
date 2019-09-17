using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Models
{
    public class UploadingFilesList
    {
        public IFormFile[] FilesContent { get; set; }
        public UploadingFileMeta[] FilesMetaData { get; set; }
    }
}