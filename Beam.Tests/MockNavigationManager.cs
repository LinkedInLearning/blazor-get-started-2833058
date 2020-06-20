using Microsoft.AspNetCore.Components;

public class MockNavigationManager : NavigationManager
{
    public MockNavigationManager()
    {
        Initialize("http://localhost/","http://localhost/");
    }

    protected override void NavigateToCore(string uri, bool forceLoad)
    {
        throw new System.NotImplementedException();
    }

}