using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Service
{
    public class FileUploaderService
    {
        /// <summary>
        /// Upload file to specified folder
        /// </summary>
        /// <param name="file"></param>
        /// <param name="directoryPath"></param>
        /// <exception cref="System.NullReferenceException">Thrown when file parameter is null </exception>
        public async void UploadFile(IFormFile file, string directoryPath)
        {
            var profilePicturePath = Path.Combine(directoryPath, file.FileName);

            using (var stream = new FileStream(profilePicturePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

        }

        public void RemoveFile(string FilePath)
        {

            
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

    }
}
