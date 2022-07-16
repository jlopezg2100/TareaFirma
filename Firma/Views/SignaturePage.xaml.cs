using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SignaturePad.Forms;
using System.IO;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
namespace Firma.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignaturePage : ContentPage
    {

        public byte[] bytefirma; 
        public SignaturePage()
        {
            InitializeComponent();
        }

        private async Task<bool> ValidationSignature()
        {
            if (String.IsNullOrWhiteSpace(Nombre.Text))
            {
                await this.DisplayAlert("Advertencia", "Debe describir un nombre", "OK");
                return false;
            }

            if (String.IsNullOrWhiteSpace(Descripcion.Text))
            {
                await this.DisplayAlert("Advertencia", "Debe escribir una descripción", "OK");
                return false;
            }

            if (Signature.IsBlank)
            {
                await this.DisplayAlert("Advertencia", "Escriba su firma", "OK");
                return false;
            }

            return true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            using (MemoryStream memory = new MemoryStream())
            {

                Stream stream = await Signature.GetImageStreamAsync(SignatureImageFormat.Png);
                stream.CopyTo(memory);
                bytefirma = memory.ToArray();
            }

            Stream image = await Signature.GetImageStreamAsync(SignatureImageFormat.Png);
            
            if (image == null)
                return;
            BinaryReader br = new BinaryReader(image);
            br.BaseStream.Position = 0;
            Byte[] All = br.ReadBytes((int)image.Length);
            byte[] imagen = (byte[])All;
            ImageSource imageSource = null;
            imageSource = ImageSource.FromStream(() => new MemoryStream(imagen));   

            bool storegaePermissionResp = await IsAvailStoragePermission();
            if (storegaePermissionResp)
            {
                var path = DependencyService.Get<IStorage>().SaveImage(imagen);
               
                await DisplayAlert("Message", "Firma Guardada", "Ok");
            }
            else
            {
                await DisplayAlert("Message", "El usuario denegó el permiso de almacenamiento", "Ok");
            }

            //Boton de Guardar
            if (await ValidationSignature())
            {
               

                try
                {
                    //var personas = (Models.Personas)BindingContext;

                    var Firma = new Models.ModelFirmas
                    {
                         nombre = this.Nombre.Text,
                         descripcion = this.Descripcion.Text,
                         ifirma = bytefirma,

                    };



                    var resultado = await App.BaseDatos.GrabarFirma(Firma);

                    if (resultado == 1)
                    {
                        await DisplayAlert("Agregado", "Ingresado Exitosamente", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo agregar", "OK");
                    }

                }
                    
                catch (Exception ex)
                {
                    await DisplayAlert("Exito", "Guardado Correctamente", "OK");
                    await Navigation.PushAsync(new SignaturePage());
                    Signature.Clear();
                }
            }
        }

        private async Task<bool> IsAvailStoragePermission()
        {


            bool response = false;
            Plugin.Permissions.Abstractions.PermissionStatus permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (permissionStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                var resp = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                if (resp.ContainsKey(Permission.Storage))
                {
                    permissionStatus = resp[Permission.Storage];
                }


                if (permissionStatus == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    response = true;
                }
                else if (permissionStatus == Plugin.Permissions.Abstractions.PermissionStatus.Denied)
                {
                    response = false;
                }
            }
            else
            {
                response = true;
            }
            return response;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new LFirmasPage());
        }
    }
}