using FinalApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalApp.ViewModels
{
    public class EditingViewModel:BaseViewModel
    {
        // Step 1:
        // the First property to be recovered from the UI

        public Employee Employee { get; set; }
        private int Id { get;  }
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

        #region  Saving  employee

        public ICommand SaveEmployeeCommand => new Command(async () =>
        {
            var employee = new Employee()
            {   Id = Id,
                Text = Text,
                Description = Description,
                IsVisible = false

            };

            int r = await DataStore.UpdateAsync(employee);
            Console.WriteLine("add employee " + employee.Text);

            await _nav.PopAsync();
        });

        #endregion

        #region constructor

        public EditingViewModel()
        {
            Title = "Edit ";
        }



        public EditingViewModel(INavigation nav,Employee employee)
        {
            this.Employee = employee;
            this.Text = Text;
            this.Description = Description;
            this.Id = employee.Id;

            Title = "Edit ";
            _nav = nav;
            CurrentPage = DependencyInject<Views.InscriptionViewPage>.Get();
            OpenPage();

        }





        #endregion


        public ICommand ReturnCommand => new Command(() =>
        {


            ////MessagingCenter.Send(this, "AddEmployee", Employee);
            var _employee = Employee as Employee;
            //ListEmployees.Add(_employee);
            DataStore.AddAsync(_employee);

            var ss = DependencyService.Get<AuthentificationViewModel>() ?? (new AuthentificationViewModel(_nav));


        });










    }

}
