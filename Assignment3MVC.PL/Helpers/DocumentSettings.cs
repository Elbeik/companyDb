using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;

namespace Assignment3MVC.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1. Get Located Folder Path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            //2. Get File Name And Make it Uniqe
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            //3. Get File Path
            string filePath = Path.Combine(folderPath, fileName);
            //4. Save File as Stream (Stream : Data Per Time )
            using var fs = new FileStream(filePath, FileMode.CreateNew);
            file.CopyTo(fs);
            return fileName;
        }
        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }






    }
    
}
