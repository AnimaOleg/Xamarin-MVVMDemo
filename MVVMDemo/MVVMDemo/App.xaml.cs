using System;
using System.IO;
using Xamarin.Forms;

namespace MVVMDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListProductView());
        }
    }
}