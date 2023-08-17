using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Prism.Mvvm; // BindableBase
using MVVMDemo.Models;
using Prism.Commands;
using Prism.Navigation.Xaml;

namespace MVVMDemo.ViewModels
{
    public class ProductViewModel : BindableBase /*BaseViewModel*/
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private bool _isAvailable;

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { SetProperty(ref _isAvailable, value); }
        }

        public ICommand ClearCommand { private set; get; }
        public DelegateCommand SendEmailCommand { private set; get; }
        public DelegateCommand GuardarCommand { private set; get; }
        //public DelegateCommand BorrarCommand { private set; get; }

        //public ICommand GoToListCommand { private set; get; }

        /*public INavigation Navigation { get; set; }
        public ProductViewModel(INavigation navigation)
        {
            //ClearCommand = new Command(Clear);
            //SendEmailCommand = new DelegateCommand(async () => await SendEmail());
            //GuardarCommand = new DelegateCommand(Guardar);

            Navigation = navigation;
            GoToListCommand = new Command<Type>(async (pageType) => await GoToList(pageType, navigation));
        }

        async Task GoToList(Type pageType, INavigation navigation)
        {
            var page = (Page)Activator.CreateInstance(pageType);

            page.BindingContext = new ListProductViewModel(navigation);
            {
                //IsAvailable = SelectedProduct.IsAvailable,
                //Name = SelectedProduct.Name,
                //Price = SelectedProduct.Price
                //Products.Add

                //_products
            };

            //await Navigation.PushAsync(ListProductViewModel(navigation));
        }*/



        public ProductViewModel()
        {
            ClearCommand = new Command(Clear);
            SendEmailCommand = new DelegateCommand(async () => await SendEmail());
            GuardarCommand = new DelegateCommand(Guardar);
        }







        void Guardar()
        {
            Console.WriteLine("Guardar: " + Name + " _ "  + Price);
        }
        void Borrar() { Console.WriteLine("Borrado: " + Name + " _ " + Price); }
        void Clear()
        {
            try
            {
                Name = string.Empty;
                Price = 0;
                IsAvailable = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Clear: " + e);
            }
        }
        async Task SendEmail()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "New Product!",
                    Body = $"Product Name: {Name}\nPrice: {Price:N2}\nAvailability? {(IsAvailable ? "Yes" : "No")}",
                    /*To = new List<string> { "luis@luisbeltran.mx" }*/
                    To = new List<string> { "oleg.tortajada@gmail.com" }
                };

                await Email.ComposeAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR SendEmail: " + ex);
            }
        }
    }
}