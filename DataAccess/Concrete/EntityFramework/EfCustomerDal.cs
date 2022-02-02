using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer,CarRentalContext>, ICustomerDal
    {
        public List<CustomerDetailsDto> GetCustomerDetails()
        {
            using (var context = new CarRentalContext())
            {
                var result = from user in context.Users
                    join customer in context.Customers
                        on user.UserId equals customer.UserId
                    select new CustomerDetailsDto()
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = $"{user.FirstName} {user.LastName}",
                        CompanyName = customer.CompanyName
                    };

                return result.ToList();
            }
        }
    }
}
