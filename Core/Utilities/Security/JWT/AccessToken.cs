using System.Security.Claims;
using Core.Entities.Concrete;
using Core.Entities.DTOs;

namespace Core.Utilities.Security.Jwt;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string[] Claims { get; set; }
}