namespace CommonHelpers.Maui.Helpers;

public static class FocusElementHelper
{
    public static readonly BindableProperty IsFocusedProperty =
        BindableProperty.CreateAttached("IsFocused",  typeof(bool), typeof(FocusElementHelper), false, propertyChanged: OnIsFocusedPropertyChanged);

    public static bool GetIsFocused(BindableObject view)
    {
        return (bool)view.GetValue(IsFocusedProperty);
    }

    public static void SetIsFocused(BindableObject view, bool value)
    {
        view.SetValue(IsFocusedProperty, value);
    }

    static void OnIsFocusedPropertyChanged(BindableObject view, object oldValue, object newValue)
    {
        if (view is not VisualElement visual)
        {
            return;
        }

        var isFocused = (bool)newValue;

        if (isFocused && !visual.IsFocused)
        {
            visual.Focus();
        }
    }
}