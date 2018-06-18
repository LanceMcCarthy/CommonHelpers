using System;
using CommonHelpers.Common;

namespace CommonHelpers.Models
{
    public class MenuItem : BindableBase
    {
        private int id;
        private string title;
        private string iconPath;
        private Type targetType;

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string IconPath
        {
            get => iconPath;
            set => SetProperty(ref iconPath, value);
        }

        public Type TargetType
        {
            get => targetType;
            set => SetProperty(ref targetType, value);
        }
    }
}
