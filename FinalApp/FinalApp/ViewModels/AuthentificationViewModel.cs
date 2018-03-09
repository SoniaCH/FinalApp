using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using FinalApp.Model;
using FinalApp.Views;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FinalApp.ViewModels
{
   public class AuthentificationViewModel:BaseViewModel
    {

        public Action DisplayInvalidLoginPrompt;// display alerte

        public Action DisplayValidLoginPrompt;

        public ObservableCollection<Employee> ListEmployees { get; set; }
       

      

        public AuthentificationViewModel()
        {
            Title = "Authentification";
        }


      
        #region defining the atribute 
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

        #endregion
        #region constructor
        public AuthentificationViewModel(INavigation nav)
        {
             Title = "First Page";
             _nav = nav;
             CurrentPage = DependencyInject<AuthentificationViewPage>.Get();
             OpenPage();


            MessagingCenter.Subscribe<InscriptionViewPage, Employee>(this, "AddEmployee", async (obj, employee) =>
            {
                var _employee = employee as Employee;
                ListEmployees.Add(_employee);
                await DataStore.AddAsync(_employee);
            });

        }


        public ICommand SubmitCommand => new Command(() =>
        {
            

          
                var ss = DependencyService.Get<ListViewModel>() ?? (new ListViewModel(_nav));
            
               
           

        });

    



        #endregion

        public ICommand InscriptionCommand => new Command(() =>
        {
                var ss = DependencyService.Get<InscriptionViewModel>() ?? (new InscriptionViewModel(_nav));
            
           
        });



        #region the function to enter the home page 

        




        #endregion

    }
}

