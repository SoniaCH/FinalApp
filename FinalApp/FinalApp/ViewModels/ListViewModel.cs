using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using FinalApp.Model;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using FinalApp.Views;
using System.Linq;


namespace FinalApp.ViewModels
{
   
    public class ListViewModel : BaseViewModel
    {
        public Command LoadEmployeesCommand { get; set; }

        private Employee _selectedEmployee; // the selected employee
        private Employee _oldEmployee;
  

        public ObservableCollection<Employee> _listEmployees;
        public ObservableCollection<Employee> ListEmployees
        {
            get
            {
                return _listEmployees;
            }
            set
            {
                _listEmployees = value;
                OnPropertyChanged();
            }

        }

        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged();
                  

                }
            }
        }



        #region  adding employee
        public ICommand AddEmloyeeCommand => new Command(() =>
        {
            var ss = DependencyService.Get<EditingViewModel>() ?? (new EditingViewModel(_nav));
        });
        #endregion


        #region SetTheVisibilityCommand method   



        public void ShowOrHideEmployee(Employee employee)
        {
         
                if (_oldEmployee == employee)
                {
                    employee.IsVisible = !employee.IsVisible;
                    // update the property of the old employee
                    UpdateEmployee(employee);

                }
                else if (_oldEmployee != null)
                {
                    _oldEmployee.IsVisible = false;
                    // update the old employee
                    UpdateEmployee(_oldEmployee);

                }
               else {
                employee.IsVisible = true;
                // update the selected employee;
                UpdateEmployee(employee);
            }


        }



        private void UpdateEmployee(Employee employee)
        {
            var index = ListEmployees.IndexOf(employee);
            ListEmployees.Remove(employee);
            ListEmployees.Insert(index, employee);
        }

        #endregion



        #region constructor
        public ListViewModel()
        {
            Title = "List  ";
            this.ListEmployees = _listEmployees;
            ListEmployees = new ObservableCollection<Employee>();

            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
        }

        public ListViewModel(INavigation nav)
        {
            Title = "List of the employee ";
            this.ListEmployees = _listEmployees;
            this.SelectedEmployee = _selectedEmployee;
         
            _nav = nav;
            CurrentPage = DependencyInject<Views.ListViewPage>.Get();
            OpenPage();

            ListEmployees = new ObservableCollection<Employee>();
            this.SearchText = SearchText;

            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            Console.WriteLine("the number of employee " + ListEmployees.Count);
            if (ListEmployees.Count == 0)
            { LoadEmployeesCommand.Execute(null); }
        }

        #endregion



        #region  Loading  employee
        async Task ExecuteLoadEmployeesCommand()
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

        #endregion

        #region going to the page to see the details of the employee
        public ICommand DetailsEmployeeCommand => new Command(async () =>
        {
            var ss = DependencyService.Get<DetailsViewModel>() ?? (new DetailsViewModel(_nav, _selectedEmployee));

        });
        #endregion


        #region deleting the employee
        public ICommand DeleteEmployeeCommand => new Command(async () =>
        {
            await DataStore.DeleteAsync(_selectedEmployee);
            ListEmployees.Remove(_selectedEmployee);


        });
        #endregion

        #region Editing the employee
        public ICommand EditEmployeeCommand => new Command(async () =>
        {
            var ss = DependencyService.Get<EditingViewModel>() ?? (new EditingViewModel(_nav, _selectedEmployee));

        });
        #endregion


        #region Command and associated methods for SearchCommand
        //step1: add an attribute for word to search
        private string _searchText;
         public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }

        }

        ////step 2: create the command method for searching
       public ICommand SearchTextChangedCommand => new Command(() =>
        {
            ObservableCollection<Employee> _employeesListFound = new ObservableCollection<Employee>();
           
               if (IsBusy)
                    return;

                IsBusy = true;

                try
                {
                    List<Employee> entities = (from e in ListEmployees where e.Text.Contains(SearchText) select e).ToList();

                    if (entities != null && entities.Any())

                    {
                        _employeesListFound = new ObservableCollection<Employee>(entities);
                    }


                    ListEmployees.Clear();

                    foreach (var item in _employeesListFound)
                    {
                        ListEmployees.Add(item);
                     Console.WriteLine("edit the employee {0} : {1}", item.Id, item.Text);
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


            




        });

        #endregion


    }
}

