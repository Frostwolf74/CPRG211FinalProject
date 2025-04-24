namespace CPRG211FinalProject.Components.Utils;

public class EquipmentObj(string serialNumber, string productNumber, string description, string location)
{
    public string SerialNumber { get; set; } = serialNumber;
    public string ProductNumber { get; set; } = productNumber;
    public string Description { get; set; } = description;
    public string Location { get; set; } = location;

    public override string ToString()
    {
        return $"Serial Number: {SerialNumber}\nProduct Number: {ProductNumber}\nDescription: {Description}\nLocation: {Location}";
    }
}