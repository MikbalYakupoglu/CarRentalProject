﻿using Core.Entities;

namespace Entities.DTOs;

public class RentalDetailsDto : IDto
{
    public int RentalId { get; set; }
    public string BrandName { get; set; }
    public string CustomerName { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}