namespace CPRG211FinalProject.Components.Utils;

public class Equipment
{
    private string SerialNumber { get; set; }
    private string ProductNumber { get; set; }
    private string Description { get; set; }
    private string Location { get; set; }

    public Equipment(string serialNumber, string productNumber, string description, string location)
    {
        SerialNumber = serialNumber;
        ProductNumber = productNumber;
        Description = description;
        Location = location;
    }
    public override string ToString()
    {
        return $"Serial Number: {SerialNumber}\nProduct Number: {ProductNumber}\nDescription: {Description}\nLocation: {Location}";
    }
}