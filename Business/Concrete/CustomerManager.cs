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
            throw new NotImplementedException();
        }

        public IResult Update(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
