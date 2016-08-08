using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using LEAP.Apps.Workbench.Core.ViewModels;
using Prism.Regions;

namespace LEAP.Apps.Workbench.Plugins.ImageViewerExample.ViewModels
{
    public class ViewImagePageViewModel : BaseViewModel, INavigationAware
    {
        public ViewImagePageViewModel()
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // set context based on inbound request. 
            var imageData = navigationContext.Parameters["imageData"] as Bitmap;

            MemoryStream ms = new MemoryStream();
            imageData.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(ms.ToArray());
            bImg.EndInit();

            //img is an Image control.
            ImageLoaded = bImg;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public BitmapImage ImageLoaded
        {
            get { return GetValue(() => ImageLoaded); }
            set { SetValue(() => ImageLoaded, value); }
        }
    }
}
