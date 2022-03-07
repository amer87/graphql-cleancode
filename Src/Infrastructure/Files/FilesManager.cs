using Com.Application.Common;
using Com.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Infrastructure.Files
{
    public class FilesManager : IFilesManager
    {
        private readonly AppSettings _settings;

        public FilesManager(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<KeyValuePair<string, string>> UploadFileAsync(IFormFile file, CancellationToken cancellationToken)
        {
            return await UploadFileAsync("\\Shops\\", file, cancellationToken);
        }

        public async Task<string> GetFileAsync(string storageDirectory, string fileName)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), $"{_settings.LocalStoragePath}\\{storageDirectory}\\{fileName}");

            if (!File.Exists(file))
            {
                return string.Empty;
            }

            byte[] fileBytes = await File.ReadAllBytesAsync(file);
            return Convert.ToBase64String(fileBytes, 0, fileBytes.Length);
        }

        public async Task<KeyValuePair<string, string>> UploadFileAsync(string directory, IFormFile file, CancellationToken cancellationToken)
        {
            var basePath = Directory.GetCurrentDirectory();
            var uploadDirectoryPath = Path.Combine(basePath, $"{_settings.LocalStoragePath}\\{directory}\\");
            if (!Directory.Exists(uploadDirectoryPath))
            {
                Directory.CreateDirectory(uploadDirectoryPath);
            }

            string nameWithoutExtention = Path.GetFileNameWithoutExtension(file.FileName);
            string extention = Path.GetExtension(file.FileName);
            string randomFile = nameWithoutExtention +
                                "_" +
                                Guid.NewGuid().ToString().Substring(0, 4) + extention;

            var targetFile = Path.GetFullPath(Path.Combine(uploadDirectoryPath, randomFile));
            using (var stream = new FileStream(targetFile, FileMode.Create))
            {

                await file.CopyToAsync(stream, cancellationToken);
            }

            return new KeyValuePair<string, string>(randomFile, extention);
        }

        public async Task<bool> RemoveFileAsync(string directory, string fileName, CancellationToken cancellationToken)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), $"{_settings.LocalStoragePath}\\{directory}\\{fileName}");
            if (!File.Exists(file))
            {
                return false;
            }

            await Task.Run(() => File.Delete(file));
            return true;
        }
    }
}
