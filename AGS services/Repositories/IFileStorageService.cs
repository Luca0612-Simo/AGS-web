using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_services.Repositories
{
    public interface IFileStorageService
    {
    Task<string> UploadFileAsync(IFormFile file);
    string GetFileUrl(string key);
    }
}
