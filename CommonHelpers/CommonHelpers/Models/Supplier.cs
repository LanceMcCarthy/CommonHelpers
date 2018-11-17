using CommonHelpers.Common;

namespace CommonHelpers.Models
{
    public class Supplier : BindableBase
    {
        private string _supplierName;
        private int _supplierId;

        public int SupplierId
        {
            get => _supplierId;
            set => SetProperty(ref _supplierId, value);
        }

        public string SupplierName
        {
            get => _supplierName;
            set => SetProperty(ref _supplierName, value);
        }
    }
}
