namespace CPRG211FinalProject.Components.Utils;

public class Customer
{
    private string FirstName { get; set; }
    private string LastName { get; set; }
    private string Email { get; set; }
    private string PhoneNumber { get; set; }
    private List<Membership> Memberships { get; set; }

    public Customer(string firstName, string lastName, string email, string phoneNumber, List<Membership> memberships)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Memberships = memberships;
    }
    
    public override string ToString()
    {
        return $"First Name: {FirstName}\nLast Name: {LastName}\nEmail: {Email}\nPhoneNumber: {PhoneNumber}";
    }
}