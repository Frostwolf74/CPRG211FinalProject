namespace CPRG211FinalProject.Components.Utils;

public class MembershipObj(string name, string type, int id, int price)
{
    private string Name { get; set; } = name;
    private string Type { get; set; } = type;
    private int Id { get; set; } = id;
    private int Price { get; set; } = price;

    public override string ToString()
    {
        return $"Name: {Name}\nType: {Type}\nId: {Id}\nPrice: {Price}";
    }
}