using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiGateway.Utils;
using BackEnd.DbContexts;
using BackEnd.Models;
using BackEnd.Tools;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace BackEnd.Controllers
{
    public class FileController : Controller
    {
        [Route("~/api/file/create_or_update")]
        [HttpPost]
        public ActionResult<Packet> CreateOrUpdate([FromBody] UploadingFilesList filesList)
        {
            using (var dbContext = new DatabaseContext())
            {
                if (!Security.AuthenticateUser(Request.Headers[Security.Authorization], dbContext, out var session))
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 1
                    };
                }
                
                dbContext.Entry(session.)
                
                for (var counter = 0; counter < filesList.FilesContent.Length; counter++)
                {
                    var content = filesList.FilesContent[counter];
                    var metaData = filesList.FilesMetaData[counter];

                    if (System.IO.File.Exists(metaData.FilePath))
                    {
                        System.IO.File.Delete(metaData.FilePath);
                    }

                    using (var fs = System.IO.File.Create(metaData.FilePath))
                    {
                        content.CopyTo(fs);
                        fs.Close();
                    }
                }

                return new Packet()
                {
                    Status = ResponseStatus.Success
                };
            }
        }

        [Route("~/api/file/delete")]
        [HttpPost]
        public ActionResult<Packet> Delete([FromBody] Packet packet)
        {
            
        }
    }
}