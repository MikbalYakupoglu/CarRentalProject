using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;


CarManager carManager = new CarManager(new EfCarDal());
BrandManager brandManager = new BrandManager(new EfBrandDal());
ColorManager colorManager = new ColorManager(new EfColorDal());



foreach (var carDetails in carManager.GetCarDetails().OrderBy(c=>c.DailyPrice))
{
    Console.WriteLine("{0} - {1} / {2} --> {3}",carDetails.CarId,carDetails.BrandName,carDetails.ColorName,carDetails.DailyPrice);
}














static void DoubleSpace()
{
    Console.WriteLine();
    Console.WriteLine();
}


static void CarTest(CarManager carManager1)
{
    foreach (var cars in carManager1.GetAll())
    {
        Console.WriteLine("{0} - {1} - {2}$", cars.Id, cars.Description, cars.DailyPrice);
    }
}