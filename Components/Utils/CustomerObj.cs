namespace CPRG211FinalProject.Components.Utils;

public class CustomerObj(int Id, string firstName, string lastName, string email, string? phoneNumber, List<MembershipObj>? memberships)
{
    public int Id { get; set; } = Id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public string? PhoneNumber { get; set; } = phoneNumber;
    public List<MembershipObj>? Memberships { get; set; } = memberships;

    public override string ToString()
    {
        return $"Id: {Id}\nFirst Name: {FirstName}\nLast Name: {LastName}\nEmail: {Email}\nPhoneNumber: {PhoneNumber}";
    }
}