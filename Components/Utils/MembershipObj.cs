namespace CPRG211FinalProject.Components.Utils;

public class MembershipObj(string name, string type, int id, int price)
{
    public string Name { get; set; } = name;
    public string Type { get; set; } = type;
    public int Id { get; set; } = id;
    public int Price { get; set; } = price;

    public override string ToString()
    {
        return $"Name: {Name}\nType: {Type}\nId: {Id}\nPrice: {Price}";
    }
}