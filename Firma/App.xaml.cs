using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Firma.Controllers;


namespace Firma
{
    public partial class App : Application
    {

        static BaseDatosSQLite basedatos;

        public static BaseDatosSQLite BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new BaseDatosSQLite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PM02.db3"));

                }

                return basedatos;
            }

        }


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.SignaturePage());
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
