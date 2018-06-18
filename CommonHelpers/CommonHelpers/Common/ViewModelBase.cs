namespace CommonHelpers.Common
{
    public class ViewModelBase : BindableBase
    {
        private string title;
        private bool isBusy;
        private string isBusyMessage;
        
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string IsBusyMessage
        {
            get => isBusyMessage;
            set => SetProperty(ref isBusyMessage, value);
        }
    }
}