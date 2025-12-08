namespace ConsoleAppProject.Menus;

public interface IAsyncDemo
{
    List<string> MenuOptions();
    Task ShowAsync();
    Task<bool> HandleMenuChoiceAsync(int choice);
}
