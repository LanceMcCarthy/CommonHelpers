namespace CommonHelpers.Maui.Mvvm;

public class ContentPageBase: ContentPage
{
    protected override void OnAppearing()
    {
        if(BindingContext is PageViewModelBase viewModel)
        {
            viewModel.OnAppearing();
        }

        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        if(BindingContext is PageViewModelBase viewModel)
        {
            viewModel.OnDisappearing();
        }

        base.OnDisappearing();
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        if(BindingContext is PageViewModelBase viewModel)
        {
            viewModel.OnNavigatingFrom(args);
        }

        base.OnNavigatingFrom(args);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        if(BindingContext is PageViewModelBase viewModel)
        {
            viewModel.OnNavigatedFrom(args);
        }

        base.OnNavigatedFrom(args);
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if(BindingContext is PageViewModelBase viewModel)
        {
            viewModel.OnNavigatedTo(args);
        }

        base.OnNavigatedTo(args);
    }

    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is PageViewModelBase viewModel)
        {
            return viewModel.OnBackButtonRequested();
        }

        return base.OnBackButtonPressed();
    }
}