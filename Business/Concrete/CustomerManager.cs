using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            var companyName = _customerDal.GetAll().Any(c => c.CompanyName == customer.CompanyName);
            var userId = _customerDal.GetAll().Any(c => c.UserId == customer.UserId);

            if (companyName && userId)
            {
                return new ErrorResult(Messages.ItemExist);
            }

            if (customer.CompanyName.Length < 2)
            {
                return new ErrorResult(Messages.ItemNameInValid);
            }

            _customerDal.Add(customer);
            return new SuccessResult(Messages.SuccessAdded);
        }

        public IResult Delete(Customer customer)
        {
             _customerDal.Delete(customer);
             return new SuccessResult(Messages.SuccessDeleted);
        }

        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.SuccessUpdated);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(),Messages.ItemsListed);
        }

        public IDataResult<List<CustomerDetailsDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailsDto>>(_customerDal.GetCustomerDetails(),Messages.ItemsListed);
        }
    }
}
