namespace CPRG211FinalProject.Components.Utils;

public class Membership
{
    private string Name { get; set; }
    private string Type { get; set; }
    private int Id { get; set; }
    private int Price { get; set; }

    public Membership(string name, string type, int id, int price)
    {
        Name = name;
        Type = type;
        Id = id;
        Price = price;
    }

    public override string ToString()
    {
        return $"Name: {Name}\nType: {Type}\nId: {Id}\nPrice: {Price}";
    }
}