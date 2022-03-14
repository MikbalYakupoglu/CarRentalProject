using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.DTOs;

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private IUserService _userService;
    private ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    [TransactionScopeAspect]
    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByMail(userForLoginDto.Email);

        if (userToCheck == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        if (!HashingHelper.VerifyPassowrd(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(userToCheck,Messages.SuccessfulLogin);
    }

    [TransactionScopeAspect]
    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
    {
        var result = BusinessRules.Run(
            UserExists(userForRegisterDto.Email)
        );

        if (!result.Success)
        {
            return new ErrorDataResult<User>(result.Message);
        }


        byte[] passwordHash,passwordSalt;
        HashingHelper.CreatePasswordHash(userForRegisterDto.Password,out passwordHash,out passwordSalt);

        var user = new User
        {
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            Email = userForRegisterDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };

        _userService.Add(user);
        return new SuccessDataResult<User>(user, Messages.SuccessfulRegister);

    }

    public IResult UserExists(string email)
    {
        var result = _userService.GetByMail(email);

        if (result != null)
        {
            return new ErrorResult(Messages.UserAlreadyExist);
        }

        return new SuccessResult();
    }

    [TransactionScopeAspect]
    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = _userService.GetClaims(user);
        var accessToken = _tokenHelper.CreateToken(user, claims);

        return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
    }

}