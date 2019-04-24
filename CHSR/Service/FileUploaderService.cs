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
        public async void UploadFile(IFormFile file, string directoryPath)
        {
            if (file != null || file.Length > 0)
            {
                var profilePicturePath = Path.Combine(directoryPath, file.FileName);

                using (var stream = new FileStream(profilePicturePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }
    }
}
