using FinalApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalApp.ViewModels
{
    public class EditingViewModel : BaseViewModel
    {
        // Step 1:
        // the First property to be recovered from the UI

        public Employee Employee { get; set; }
        private string text;
        public string Text //paramètre de Binding au niveau du view
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        private int id;
        public int Id //paramètre de Binding au niveau du view
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }

        }

        //The second property to be recovered from the UI
        private string description;
        public string Description //paramètre de Binding au niveau du view
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        #region  Saving  employee

        public ICommand SaveEmployeeCommand => new Command(async () =>
        {
            var employee = new Employee()
            {
                Text = Text,
                Description = Description,
                IsVisible = false

            };

            await DataStore.AddAsync(employee);
            Console.WriteLine("add employee " + employee.Text);

            //await _nav.PopAsync();
            var ss = DependencyService.Get<ListViewModel>() ?? (new ListViewModel(_nav));
        });

        #endregion

        #region  Editing  employee

        public ICommand EditEmployeeCommand => new Command(async () =>
        {

            var employee = new Employee()
            {
                Id = Id,
                Text = Text,
                Description = Description,
                IsVisible = false

            };

            await DataStore.UpdateAsync(employee);
            Console.WriteLine("edit the employee {0} : {1}", employee.Id,employee.Text);

           // await _nav.PopAsync();
            var ss = DependencyService.Get<ListViewModel>() ?? (new ListViewModel(_nav));
        });

        #endregion

        #region constructor

        public EditingViewModel()
        {
            Title = "Adding or Editing ";
        }



        public EditingViewModel(INavigation nav)
        {
            this.Employee = Employee;

            Title = "Add employee ";
            _nav = nav;
            CurrentPage = DependencyInject<Views.EditingViewPage>.Get();
            OpenPage();
        }

        public EditingViewModel(INavigation nav,Employee employee)
        {
            Title = "Edit Employee ";
            _nav = nav;
            CurrentPage = DependencyInject<Views.EditingViewPage>.Get();
            OpenPage();
            // taken the information from the previeus page
            this.Employee = employee;
            this.Id = employee.Id;            
            this.Text = employee.Text;
            this.Description = employee.Description;

        }
        #endregion 

        #region the return button
        public ICommand ReturnCommand => new Command(async () =>
        {

            await _nav.PopAsync();


        });

        #endregion
    }

}
