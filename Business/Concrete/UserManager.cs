using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Results;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using User = Core.Entities.Concrete.User;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);

            //return new SuccessDataResult<List<OperationClaim>>(result);
        }

        public IResult Add(User user)
        {
            //BusinessRules.Run(
            //    _authService.UserExists(user.Email)
            //    );


            _userDal.Add(user);
            return new SuccessResult(Messages.SuccessAdded);
        }


        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);

            //if (result == null)
            //{
            //    return new ErrorDataResult<User>(Messages.UserNotFound);
            //}
            //return new SuccessDataResult<User>(result);
        }

    }
}
