using Microsoft.AspNetCore.Http;


namespace Core.Utilities.Helpers.FileHelper;

public interface IFileHelperService 
{
    string Upload(IFormFile file);
    string Update(IFormFile file, string filePath);
    void Delete(string root);
}