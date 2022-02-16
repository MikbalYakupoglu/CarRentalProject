using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;


namespace Core.Utilities.Helpers.FileHelper;

public interface IFileHelperService 
{
    IResult Upload(IFormFile file);
    IResult Update(IFormFile file,string sourcePath);
    IResult Delete(string root);
}