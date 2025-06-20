namespace CommonHelpers.Maui.Mvvm;

public interface IPageViewModel
{
    void OnAppearing();

    void OnDisappearing();

    void OnNavigatingFrom(NavigatingFromEventArgs args);

    void OnNavigatedFrom(NavigatedFromEventArgs args);

    void OnNavigatedTo(NavigatedToEventArgs args);

    bool OnBackButtonRequested();
}