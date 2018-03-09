using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using FinalApp.Model;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FinalApp.ViewModels
{
    public class ListViewModel : BaseViewModel
    {

        public ObservableCollection<Employee> ListEmployees { get; set; }

        public Command LoadEmployeesCommand { get; set; }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ListEmployees.Clear();
                var items = await DataStore.GetAllAsync();
                foreach (var item in items)
                {
                    ListEmployees.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public ListViewModel(INavigation nav)
        {
            Title = "PAGE 2 NAVIGATION ";
            ListEmployees = new ObservableCollection<Employee>();

            
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _nav = nav;
            CurrentPage = DependencyInject<Views.ListViewPage>.Get();
            OpenPage();
        }
    }
}
