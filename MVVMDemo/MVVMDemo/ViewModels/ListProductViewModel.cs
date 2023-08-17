using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Mvvm; // BindableBase
using MVVMDemo.Models;

namespace MVVMDemo.ViewModels
{
    public class ListProductViewModel : BindableBase /*BaseViewModel*/
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }

        public ICommand GoToDetailsCommand { private set; get; }
        public INavigation Navigation { get; set; }

        public ListProductViewModel(INavigation navigation)
        {
            Navigation = navigation;
            GoToDetailsCommand = new Command<Type>(async (pageType) => await GoToDetails(pageType));

            Products = new ObservableCollection<Product>
            {
                new Product() { ID = 1, Name = "Leche", Price = 10.30, IsAvailable = true },
                new Product() { ID = 2, Name = "Chocolates", Price = 12.78, IsAvailable = false },
                new Product() { ID = 3, Name = "Galletas", Price = 8, IsAvailable = true }
            };
        }

        async Task GoToDetails(Type pageType)
        {
            Console.WriteLine(SelectedProduct);
            if (SelectedProduct != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = new ProductViewModel()
                {
                    IsAvailable = SelectedProduct.IsAvailable,
                    Name = SelectedProduct.Name,
                    Price = SelectedProduct.Price
                };

                await Navigation.PushAsync(page);
            }
        }

    }
}