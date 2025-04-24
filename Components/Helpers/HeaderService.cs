namespace CPRG211FinalProject.Components.Helpers;

public class HeaderService
{
    public string HeaderText { get; private set; } = "Main Page";
    
    public event Action? OnChange;
    
    public void SetHeader(string text)
    {
        HeaderText = text;
        OnChange?.Invoke();
    }
}