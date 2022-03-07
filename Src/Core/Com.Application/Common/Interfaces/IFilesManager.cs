using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Application.Common.Interfaces;

public interface IFilesManager
{
    Task<KeyValuePair<string, string>> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
    Task<KeyValuePair<string, string>> UploadFileAsync(string directory, IFormFile file, CancellationToken cancellationToken);
    Task<bool> RemoveFileAsync(string directory, string fileName, CancellationToken cancellationToken);
    Task<string> GetFileAsync(string storageDirectory, string fileName);
}
