using System;
using System.Collections.Generic;
using System.Text;
using CommonHelpers.Common;

namespace CommonHelpers.Models
{
    public class Category : BindableBase
    {
        private string _categoryName;
        private int _categoryId;

        public int CategoryId
        {
            get => _categoryId;
            set => SetProperty(ref _categoryId, value);
        }

        public string CategoryName
        {
            get => _categoryName;
            set => SetProperty(ref _categoryName, value);
        }
    }
}
