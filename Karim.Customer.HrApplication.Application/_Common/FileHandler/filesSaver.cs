using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karim.Customer.HrApplication.Application._Common.FileHandler
{
    internal static class filesSaver
    {
        public static async Task<string> SaveFiles(IFormFile file, IWebHostEnvironment env)
        {
            //Create File Name
            string FileName = $"{Guid.NewGuid()}_{file.FileName}";
            //Create Name For Folder That Hold My Files
            string FolderPath = Path.Combine(env.WebRootPath, "Resources"); //Just For Create The Folder and File Path
            //Check If Folder Exits If Not Create It
            if (!Directory.Exists(FolderPath))
            {
                //Create Folder
                Directory.CreateDirectory(FolderPath);
            }
            //Create Full Path To Save My File
            string FullPath = Path.Combine(FolderPath, FileName);
            //Check File Exist Or Not
            if (File.Exists(FullPath)) throw new Exception("This File Already Exist");
            //Save The File
            using(var stream = new FileStream(FullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream); //Saving The File It Self On the Created Path
            }
            return $"/Resources/{FileName}";
        }

        public static bool DeleteFile(string filePath, IWebHostEnvironment env)
        {
            bool returnedValue = false;
            //Create Full Path
            //1. Environment Path c://program/wwwroot
            //2. File Path /Resources/filename.jpg
            string fullPath = $"{env.WebRootPath}{filePath}";
            //Check if the file exists
            if (!File.Exists(fullPath)) returnedValue = false;
            //Delete the file
            File.Delete(fullPath);
            //Check if the file still exists
            if(File.Exists(fullPath)) returnedValue = false;
            else returnedValue = true;
            return returnedValue;
        }
    }
}
