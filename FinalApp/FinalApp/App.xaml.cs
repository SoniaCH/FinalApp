using FinalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FinalApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();



            // step 1:call the viewModel of the page
            var model = DependencyInject<AuthentificationViewModel>.Get();
            // step 2: call the contentPage
            model.CurrentPage = DependencyInject<Views.AuthentificationViewPage>.Get();

            // step 3: do the relation between the View and the viewModel
            model.CurrentPage.BindingContext = model;  //(3)

            //étape de navigation
            var nav = new NavigationPage(model.CurrentPage);
            model._nav = nav.Navigation;
            MainPage = nav;


            
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
