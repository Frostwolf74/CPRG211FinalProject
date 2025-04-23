namespace CPRG211FinalProject.Components.Utils;

public class EquipmentObj(string serialNumber, string productNumber, string description, string location)
{
    private string SerialNumber { get; set; } = serialNumber;
    private string ProductNumber { get; set; } = productNumber;
    private string Description { get; set; } = description;
    private string Location { get; set; } = location;

    public override string ToString()
    {
        return $"Serial Number: {SerialNumber}\nProduct Number: {ProductNumber}\nDescription: {Description}\nLocation: {Location}";
    }
}