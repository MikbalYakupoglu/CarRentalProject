using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using User = Core.Entities.Concrete.User;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal, IUserOperationClaimDal userOperationClaimDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);

        }

        public IResult Add(User user)
        {
            
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
