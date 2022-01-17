using Business.Concrete;
using Business.Constants;
using Core.Utilities.Helpers.FileHelper;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;


CarManager carManager = new CarManager(new EfCarDal());
BrandManager brandManager = new BrandManager(new EfBrandDal());
ColorManager colorManager = new ColorManager(new EfColorDal());
RentalManager rentalManager = new RentalManager(new EfRentalDal());
UserManager userManager = new UserManager(new EfUserDal());

//CarDetails(carManager);
//ColorTest(colorManager);

//rentalManager.Add(new Rental()
//{
//    CarId = 6,
//    CustomerId = 6,
//    RentDate = new DateTime(2021, 12, 12)
//});


//carManager.Add(new Car()
//{
//    ColorId = 2,
//    BrandId = 5,
//    ModelYear = 2017,
//    DailyPrice = 130,
//    Description = ""

//});

//foreach (var item in brandManager.GetAll().Data)
//{
//    Console.WriteLine("{0} -> {1}",item.BrandId,item.BrandName);
//}




static void DoubleSpace()
{
    Console.WriteLine();
    Console.WriteLine();
}


static void CarTest(CarManager carManager1)
{
    foreach (var cars in carManager1.GetAll().Data)
    {
        Console.WriteLine("{0} - {1} - {2}$", cars.CarId, cars.Description, cars.DailyPrice);
    }
}

static void CarDetails(CarManager carManager1)
{
    var result = carManager1.GetCarDetails();

    if (result.Success)
    {
        foreach (var carDetails in result.Data)//.OrderBy(c => c.DailyPrice))
        {
            Console.WriteLine("{0} - {1} / {2} --> {3}", carDetails.CarId, carDetails.BrandName, carDetails.ColorName,
                carDetails.DailyPrice);
        }
        Console.WriteLine(Messages.ItemsListed);
    }
    else
    {
        Console.WriteLine(Messages.DataNotFound);
    }


}

void ColorTest(ColorManager colorManager1)
{
    foreach (var colors in colorManager1.GetAll().Data)
    {
        Console.WriteLine("{0} - {1}", colors.ColorId, colors.ColorName);
    }
}