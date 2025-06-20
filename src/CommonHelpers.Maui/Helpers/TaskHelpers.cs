namespace CommonHelpers.Maui.Helpers;

public static class TaskHelpers
{
    public static bool RunOnUiThread(Action a)
    {
        try
        {
            return Application.Current != null && Application.Current.Dispatcher.Dispatch(a);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RunOnUiThread Helper Exception: {ex.Message}");
            return false;
        }
    }
}