using FinalApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalApp.ViewModels
{
   public  class InscriptionViewModel : BaseViewModel
    {
        public Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Employee> ListEmployees { get; set; }
        public Command SubmitCommand { get; set; }

        // Step 1:
        // the First property to be recovered from the UI
        private String text;
        public String Text //paramètre de Binding au niveau du view
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        //The second property to be recovered from the UI
        private String description;
        public String Description //paramètre de Binding au niveau du view
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }



        #region constructor

        public InscriptionViewModel()
        {
            Title = "Inscription ";
        }

        public InscriptionViewModel(Employee employee)
        {
            Title = "Inscription ";
            this.Text = employee.Text;
            this.Description = employee.Description;

        }

        public InscriptionViewModel(INavigation nav)
        {
            Title = "Inscription ";

            employee = new Employee
            {
                Text = "Departement",
                Description = "test"
            };

            _nav = nav;
            CurrentPage = DependencyInject<Views.InscriptionViewPage>.Get();
            OpenPage();

        }





        #endregion

       
            public ICommand ReturnCommand => new Command(() =>
            {
                ////MessagingCenter.Send(this, "AddEmployee", Employee);
                //var _employee = employee as Employee;
                //ListEmployees.Add(_employee);
                //DataStore.AddAsync(_employee);

                var ss = DependencyService.Get<AuthentificationViewModel>() ?? (new AuthentificationViewModel(_nav));
                

            });

        #region the function to enter the home page 

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ListEmployees.Clear();
                var items = await DataStore.GetAllAsync(e => e.Text.Equals(Text) && e.Description.Equals(Description));
                foreach (var item in items)
                {
                    ListEmployees.Add(item);
                }
                if (ListEmployees != null)
                {
                    var ss = DependencyService.Get<ListViewModel>() ?? (new ListViewModel(_nav));
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



        #endregion


    }

}
