using Core.Entities.Abstract;

namespace Entities.Concrete;

public class Payment : IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string FirstNameOnCard { get; set; }
    public string LastNameOnCard { get; set; }
    public string CreditCardNumber { get; set; }
    public int ExpMonth { get; set; }
    public int ExpYear { get; set; }
    public string CVV { get; set; }
    public DateTime Date { get; set; }

}