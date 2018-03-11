using FinalApp.Model;
using FinalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InscriptionViewPage : ContentPage
	{

        public InscriptionViewPage()
        {
            InitializeComponent();
        }

        //async void Save_Clicked(object sender, EventArgs e)
        //{
        //    MessagingCenter.Send(this, "AddEmployee", Employee);
        //    //await Navigation.PopModalAsync();

        //    var ss = DependencyService.Get<AuthentificationViewModel>() ?? (new AuthentificationViewModel());
        //}
    }
}