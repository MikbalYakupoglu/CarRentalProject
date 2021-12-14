using System.Runtime.InteropServices;
using Business.Abstract;
using Business.Constants;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
         private IRentalDal _rentalDal;
        
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {

            var result = _rentalDal.GetAll().FindLast(r => r.CarId == rental.CarId);

            if (result == null)
            {
                return new ErrorResult(Messages.DataNotFound);
            }
            if (result.ReturnDate == null)
            { 
                return new ErrorResult(Messages.CarNotOnRent);
            }
            _rentalDal.Add(rental);
                return new SuccessResult(Messages.SuccessAdded);
                
        }

        public IResult Delete(Rental rental)
        {
            /*
            var result = _rentalDal.Get(r => r.RentalId == rental.RentalId);

            if (_rentalDal.GetAll().Where(r=> r.CarId == rental.CarId) == result)
            {
                _rentalDal.Delete(result);
                return new SuccessResult(Messages.SuccessDeleted);
            }
            */
            return new ErrorResult(Messages.DataNotFound);
        }

        public IResult Update(Rental rental)
        {
            var result = _rentalDal.GetAll().FindLast(r => r.CarId == rental.CarId);

            rental.RentalId = result.RentalId;
            if (result.ReturnDate.Value.Year < 2000 || result.ReturnDate == null)
            {
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.SuccessUpdated);
            }

            return new ErrorResult(Messages.DataNotFound);

        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.ItemsListed);
        }
    }
}
