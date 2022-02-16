using Core.Entities.Abstract;

namespace Entities.DTOs;

public class CarImageDto : IDto
{
    public int CarId { get; set; }
    public string ImagePath { get; set; }   
}