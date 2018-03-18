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

        #region the property of the method
        // Step 1:
        // the First property to be recovered from the UI
        public Employee Admin { get; set; }
        private String name;
        public String Name //paramètre de Binding au niveau du view
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        //The second property to be recovered from the UI
        private String password;
        public String Password //paramètre de Binding au niveau du view
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region  Saving  employee

        public ICommand SaveAdminCommand => new Command(async () =>
        {
            var admin = new Admin()
            {
                Name = Name,
                Password = Password,
            };

            await DataStoreAdmin.AddAsync(admin);
            Console.WriteLine("add admin " + admin.Name);
            await _nav.PopAsync();

        });

        #endregion

        #region constructor

        public InscriptionViewModel()
        {
            Title = "Inscription admin ";
        }

       

        public InscriptionViewModel(INavigation nav)
        {
            this.Admin = Admin;

            Title = "Inscription ";
            _nav = nav;
            CurrentPage = DependencyInject<Views.InscriptionViewPage>.Get();
            OpenPage();

        }
        #endregion
     
    }

}
