using CommonHelpers.Common;

namespace CommonHelpers.Models
{
    public class Product : BindableBase
    {
        private int _productId;
        private string _productName;
        private int? _supplierId;
        private int? _categoryId;
        private string _quantityPerUnit;
        private bool _discontinued;
        private short? _reorderLevel;
        private short? _unitsOnOrder;
        private short? _unitsInStock;
        private decimal? _unitPrice;

        public int ProductId
        {
            get => _productId;
            set => SetProperty(ref _productId, value);
        }

        public string ProductName
        {
            get => _productName;
            set => SetProperty(ref _productName, value);
        }

        public int? SupplierId
        {
            get => _supplierId;
            set => SetProperty(ref _supplierId, value);
        }

        public int? CategoryId
        {
            get => _categoryId;
            set => SetProperty(ref _categoryId, value);
        }

        public string QuantityPerUnit
        {
            get => _quantityPerUnit;
            set => SetProperty(ref _quantityPerUnit, value);
        }

        public decimal? UnitPrice
        {
            get => _unitPrice;
            set => SetProperty(ref _unitPrice, value);
        }

        public short? UnitsInStock
        {
            get => _unitsInStock;
            set => SetProperty(ref _unitsInStock, value);
        }

        public short? UnitsOnOrder
        {
            get => _unitsOnOrder;
            set => SetProperty(ref _unitsOnOrder, value);
        }

        public short? ReorderLevel
        {
            get => _reorderLevel;
            set => SetProperty(ref _reorderLevel, value);
        }

        public bool Discontinued
        {
            get => _discontinued;
            set => SetProperty(ref _discontinued, value);
        }
    }
}
