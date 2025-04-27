namespace CPRG211FinalProject.Components.Utils;
public class CustomerObj
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<MembershipObj> Memberships { get; set; } = new();

    public CustomerObj(int id, string firstName, string lastName, string email, string? phoneNumber, List<MembershipObj>? memberships)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Memberships = memberships ?? new List<MembershipObj>();
    }

    public override string ToString()
    {
        string membershipName = string.Join(", ", Memberships.Select(m => m.Name));

        return $"Name: {FirstName} {LastName}, Email: {Email}, Phone: {PhoneNumber}, Memberships: {membershipName}";
    }
}

