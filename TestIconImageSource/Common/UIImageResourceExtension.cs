using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestIconImageSource.Common
{
    public class UIImageResourceExtension : IMarkupExtension<ImageSource>
    {
        public string ResourceName { get; set; }

        public ImageSource ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(ResourceName))
            {
                return null;
            }

            var assembly = GetType().GetTypeInfo().Assembly;
            var assemblyName = assembly.GetName().Name;
            return ImageSource.FromStream(() => assembly.GetManifestResourceStream(assemblyName + "." + ResourceName));
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<ImageSource>).ProvideValue(serviceProvider);
        }
    }
}
