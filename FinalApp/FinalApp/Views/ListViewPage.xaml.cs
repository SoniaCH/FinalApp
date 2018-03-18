using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalApp.Model;
using FinalApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewPage : ContentPage
	{

        ListViewModel viewModel;
        public ListViewPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ListViewModel();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (viewModel.ListEmployees.Count == 0)
        //        viewModel.LoadEmployeesCommand.Execute(null);
        //}


        #region for showing the expandble list
        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e) //when click on an item in the listview

        {

            var employee = e.Item as Employee; //get the product which raise the itemclicked event

            var vm = BindingContext as ListViewModel; //call instance of ViewModel class (as is used to convert object)

            vm?.ShowOrHideEmployee(employee); //if vm not null call the method which refresh the list

        }
        #endregion


    }
}