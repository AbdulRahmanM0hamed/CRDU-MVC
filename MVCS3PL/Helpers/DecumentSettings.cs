using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVCS3PL.Helpers
{
    public class DecumentSettings
    {
        public static string UploadImage(IFormFile file, string FolderName)
        {
            // 1) get located folder path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);

            // 2) get file name and make it unique

            string FileName = $"{Guid.NewGuid()}{file.FileName}";
            // 3) get file path 
            string FilePath = Path.Combine(FolderPath, FileName);

            // 4) make stream
            var fs = new FileStream(FilePath, FileMode.CreateNew);
            file.CopyTo(fs);

            return FileName;
        }


        public static void DeleteFile(string fileName, string FolderName)
        {
            if (fileName is not null && FolderName is not null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }


    }





}
