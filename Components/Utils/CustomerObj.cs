namespace CPRG211FinalProject.Components.Utils;

public class CustomerObj(string firstName, string lastName, string email, int phoneNumber, List<MembershipObj> memberships)
{
    private string FirstName { get; set; } = firstName;
    private string LastName { get; set; } = lastName;
    private string Email { get; set; } = email;
    private int PhoneNumber { get; set; } = phoneNumber;
    private List<MembershipObj> Memberships { get; set; } = memberships;

    public override string ToString()
    {
        return $"First Name: {FirstName}\nLast Name: {LastName}\nEmail: {Email}\nPhoneNumber: {PhoneNumber}";
    }
}