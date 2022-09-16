using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Extentions
{
    public static class FileExtentions
    {
        public static string SaveImageFolderAsFile(this IFormFile file, string folderPath, string extention = ".jpg")
        {
            var uploadedFile = StringExtentions.GenerateRandom() + extention;
            //var path = Path.Combine(GlobalConfiguration.FileUploadPath, folderPath, uploadedFile);
            var path = Path.Combine("pathForFileUpload", folderPath, uploadedFile);
            using (var stream = File.Create(path))
            {
                file.CopyTo(stream);
            }
            return uploadedFile;
        }
    }
}
