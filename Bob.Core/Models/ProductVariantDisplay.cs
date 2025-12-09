using Avalonia.Media.Imaging;
using Bob.Core.Domain;
using Bob.Core.Systems;
using Bob.Core.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using static LinqToDB.Sql;

namespace Bob.Core.Models
{
    public class ProductVariantDisplay : ObservableObject
    {
        public int VariantId { get; set; }

        private string _color;
        public string Color
        {
            get => _color;
            set
            {
                if (SetProperty(ref _color, value))
                    OnVariantChanged?.Invoke(this);
            }
        }

        private string _size;
        public string Size
        {
            get => _size;
            set
            {
                if (SetProperty(ref _size, value))
                    OnVariantChanged?.Invoke(this);
            }
        }

        private decimal _finalPrice;
        public decimal FinalPrice
        {
            get => _finalPrice;
            set => SetProperty(ref _finalPrice, value);
        }

        private int _stock;
        public int Stock
        {
            get => _stock;
            set => SetProperty(ref _stock, value);
        }

        public List<string> AvailableColors { get; set; } = new();
        public List<string> AvailableSizes { get; set; } = new();

        // Callback to parent ProductDisplay
        public Action<ProductVariantDisplay> OnVariantChanged { get; set; }
    }



    public class ProductDisplay : ObservableObject
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public int TypeId { get; set; }
        public string Motive { get; set; }

        public List<ProductVariantDisplay> Variants { get; set; } = new();

        private ProductVariantDisplay _selectedVariant;
        public ProductVariantDisplay SelectedVariant
        {
            get => _selectedVariant;
            set
            {
                if (_selectedVariant != value)
                {
                    _selectedVariant = value;
                    OnPropertyChanged(nameof(SelectedVariant));

                    if (_selectedVariant != null)
                    {
                        var colorFolder = _selectedVariant.Color.ToLower(); // make sure folder names match
                        Img = new Bitmap($"assets/{FileUtils.GetProductFolder(TypeId)}/{colorFolder}/{Motive}.png");
                    }
                }
            }
        }

        private Bitmap _img;
        public Bitmap Img
        {
            get => _img;
            set
            {
                _img = value;
                OnPropertyChanged(nameof(Img));
            }
        }


        public IRelayCommand AddToCartCommand => new RelayCommand(AddToCart);

        private async void AddToCart()
        {
            if (SelectedVariant == null)
                return;

            if (SelectedVariant.Stock <= 0)
            {
                return;
            }

            await CartSystem.AddToCart(SelectedVariant.VariantId);
        }
    }
}
