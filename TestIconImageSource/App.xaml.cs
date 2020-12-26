using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestIconImageSource
{
    public partial class App : Application
    {
        private readonly NavigationPage navigationPage;
        public App()
        {
            InitializeComponent();
            navigationPage = new NavigationPage(new MainPage());

            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
