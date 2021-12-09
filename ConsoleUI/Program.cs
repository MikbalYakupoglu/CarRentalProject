using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

CarManager carManager = new CarManager(new EfCarDal());
BrandManager brandManager = new BrandManager(new EfBrandDal());
ColorManager colorManager = new ColorManager(new EfColorDal());

//foreach (var cars in carManager.GetAll())
//{
//    Console.WriteLine("{0} - {1} - {2}$",cars.Id, cars.Description, cars.DailyPrice);
//}

//DoubleSpace();

var _carDto = from car in carManager.GetAll()
                   join brand in brandManager.GetAll()
                   on car.BrandId equals brand.Id
                   join color in colorManager.GetAll()
                   on car.ColorId equals color.Id
                   select new CarDto {CarId = car.Id,BrandName = brand.BrandName, ColorName = color.ColorName, DailyPrice = car.DailyPrice};



//foreach (var carsByColorId in carManager.GetCarsByColorId(2))
//{
//        Console.WriteLine("ID: {0} - Color: {1} -> {2}$", carsByColorId.Id, _carDto.FirstOrDefault(c=>c.CarId == carsByColorId.Id).ColorName, carsByColorId.DailyPrice);
//}

//DoubleSpace();


//brandManager.Add(new Brand
//{
//    BrandName = "a".ToUpper()
//});

//brandManager.Delete(new Brand
//{
//    BrandName = "tofaş".ToUpper()
//});



static void DoubleSpace()
{
    Console.WriteLine();
    Console.WriteLine();
}


class CarDto
{
    public int CarId { get; set; }
    public string BrandName { get; set; }
    public string ColorName { get; set; }
    public decimal DailyPrice { get; set; }
}

