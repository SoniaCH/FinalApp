using FinalApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalApp.ViewModels
{
   public class DetailsViewModel:BaseViewModel
    {

        private Employee employee;
        public Employee Employee {
            get { return employee; }
            set { employee = value;
                OnPropertyChanged();
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        #region constructor

        public DetailsViewModel()
        {
            Title = Employee.Text;
        }



        public DetailsViewModel(INavigation nav,Employee employee)
        {
            this.Employee = employee;
            this.Text = employee.Text;
            this.Description = employee.Description;
            Title = employee.Text;
            _nav = nav;
            CurrentPage = DependencyInject<Views.DetailsViewPage>.Get();
            OpenPage();
            

        }
        #endregion

        //BackToListCommand
        #region  Return to previeus page command

        public ICommand BackToListCommand => new Command(async () =>
        {
            await _nav.PopAsync();
        });

        #endregion

    }
}
