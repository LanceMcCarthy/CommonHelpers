using CommonHelpers.Common;

namespace CommonHelpers.Maui.Mvvm;

public class PageViewModelBase : ViewModelBase, IViewModel
{
    public virtual void OnAppearing() {}

    public virtual void OnDisappearing() {}

    public virtual void OnNavigatingFrom(NavigatingFromEventArgs args){}

    public virtual void OnNavigatedFrom(NavigatedFromEventArgs args){}

    public virtual void OnNavigatedTo(NavigatedToEventArgs args){}

    public virtual bool OnBackButtonRequested() => false;
}