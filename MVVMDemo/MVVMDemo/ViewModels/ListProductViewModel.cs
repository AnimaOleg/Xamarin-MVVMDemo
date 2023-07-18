using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism.Mvvm; // BindableBase
using MVVMDemo.Models;

using Xamarin.Forms;

namespace MVVMDemo.ViewModels
{
    public class ListProductViewModel : BindableBase /*BaseViewModel*/
    {
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value); /*OnPropertyChanged();*/ 
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; /*OnPropertyChanged();*/ }
        }

        public ICommand GoToDetailsCommand { private set; get; }

        public INavigation Navigation { get; set; }

        public ListProductViewModel(INavigation navigation)
        {
            Navigation = navigation;
            GoToDetailsCommand = new Command<Type>(async (pageType) => await GoToDetails(pageType));

            Products = new ObservableCollection<Product>();

            Products.Add(new Product() { ID = 1, Name = "Leche", Price = 10.30, IsAvailable = true });
            Products.Add(new Product() { ID = 2, Name = "Chocolates", Price = 12.78, IsAvailable = false });
            Products.Add(new Product() { ID = 3, Name = "Galletas", Price = 8, IsAvailable = true });
        }

        async Task GoToDetails(Type pageType)
        {
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