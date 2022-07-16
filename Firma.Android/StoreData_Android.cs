using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firma.Droid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(StoreData_Android))]
namespace Firma.Droid
{
    class StoreData_Android : IStorage
    {
        [Obsolete]
        public string SaveImage(byte[] bytedata)
        {
            string path = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Firma_" + DateTime.UtcNow.ToBinary().ToString() + ".jpg");
            File.WriteAllBytes(path, bytedata);
            return Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath;

        }
    }
}