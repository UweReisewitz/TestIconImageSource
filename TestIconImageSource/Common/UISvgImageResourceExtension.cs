using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestIconImageSource.Common
{

    public class UISvgImageResourceExtension : IMarkupExtension<ImageSource>
    {
        private static readonly Dictionary<string, SKData> imagedataDictionary = new Dictionary<string, SKData>();

        public string ResourceName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ImageSource ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(ResourceName))
            {
                return null;
            }

            if (!imagedataDictionary.TryGetValue(ResourceName, out var imagedata))
            {
                var assembly = GetType().GetTypeInfo().Assembly;
                var assemblyName = assembly.GetName().Name;

                var skSvg = new SkiaSharp.Extended.Svg.SKSvg(new SKSize(Width, Height));

                skSvg.Load(assembly.GetManifestResourceStream(assemblyName + "." + ResourceName));

                var bitmap = new SKBitmap(Width, Height);
                var canvas = new SKCanvas(bitmap);
                canvas.Clear(SKColors.Transparent);
                canvas.DrawPicture(skSvg.Picture);
                canvas.Flush();
                canvas.Save();

                imagedata = SKImage.FromBitmap(bitmap).ToRasterImage().Encode(SKEncodedImageFormat.Png, 100);
                imagedataDictionary.Add(ResourceName, imagedata);
            }
            return ImageSource.FromStream(() => imagedata.AsStream());
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<ImageSource>).ProvideValue(serviceProvider);
        }
    }
}
