﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailsDto> GetRentalDetails()
        {
            using (var context = new CarRentalContext())
            {
                var result = from rental in context.Rentals
                    join customer in context.Customers
                        on rental.CustomerId equals customer.CustomerId
                    join user in context.Users
                        on customer.UserId equals user.UserId
                    join car in context.Cars
                        on rental.CarId equals car.CarId
                    select new RentalDetailsDto()
                    {
                        RentalId = rental.RentalId,
                        CarId = car.CarId,
                        CustomerName = $"{user.FirstName} {user.LastName}",
                        CarDescription = car.Description,
                        RentDate = rental.RentDate,
                        ReturnDate = rental.ReturnDate
                    };

                return result.ToList();
            }
        }
    }
}
