using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace TestIconImageSource.WPF
{
    public partial class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent(); 

            Forms.Init();
            LoadApplication(new App());
        }
    }
}