using Core.Entities.Abstract;

namespace Entities.DTOs;

public class RentalDetailsDto : IDto
{
    public int RentalId { get; set; }
    public int CarId { get; set; }
    public string CarDescription { get; set; }
    public string CustomerName { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}