using Core.Results;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelper;

public class FileHelperManager : IFileHelperService
{
    private static string _currentDirectory = Environment.CurrentDirectory + @"\\wwwroot";
    private static string _folderName = @"\\uploads\\";
    private static string _folderPath = _currentDirectory + _folderName;
    
    public string Upload(IFormFile file)
    {
        if (file.Length > 0)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string fileGuidName = Guid.NewGuid().ToString("B");
            string fileName = fileGuidName + fileExtension;

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            
            using (FileStream fileStream = File.Create(_folderPath + fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
                return (_folderPath + fileName).Replace("\\", "/");
            }
        }

        return null;
    }
    
    public string Update(IFormFile file, string fileName)
    {
        if (file.Length > 0)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            return Upload(file);
        }

        return null;
    }

    public void Delete(string root)
    {
        if (File.Exists(root))
        {
            File.Delete(root);
        }
    }

    public void GetPath()
    {
        
    }
}