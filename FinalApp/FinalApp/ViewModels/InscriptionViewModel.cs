using FinalApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SL;

namespace FinalApp.ViewModels
{
   public  class InscriptionViewModel : BaseViewModel
    {

        #region the property of the method
        // Step 1:
        // the First property to be recovered from the UI
        public Employee Employee { get; set; }
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
        #endregion


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
            await _nav.PopAsync();

        });

        #endregion

        #region constructor

        public InscriptionViewModel()
        {
            Title = "Inscription ";
        }

       

        public InscriptionViewModel(INavigation nav)
        {
            this.Employee = Employee;

            Title = "Inscription ";
            _nav = nav;
            CurrentPage = DependencyInject<Views.InscriptionViewPage>.Get();
            OpenPage();

        }
        #endregion
     
    }

}
