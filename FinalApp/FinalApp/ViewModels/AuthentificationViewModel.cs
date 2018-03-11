﻿using System;
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
          
        }


        #endregion

        public ICommand InscriptionCommand => new Command(() =>
        {
                var ss = DependencyService.Get<InscriptionViewModel>() ?? (new InscriptionViewModel(_nav));
            
           
        });


        #region button to login
        public ICommand EnterEmployeeCommand => new Command(async () =>
        {
            IEnumerable<Employee> _employeelist = await DataStore.GetAllAsync() as IEnumerable<Employee>;

            foreach (Employee item in _employeelist)
            {
                if (item.Text == text && item.Description == description)
                {
                    var ss = DependencyService.Get<ListViewModel>() ?? (new ListViewModel(_nav));
                  
                }
            }
        });
        #endregion



        #region the function to enter the home page 

        public ICommand RemoveEmployeeCommand => new Command(async () =>
        {
            IEnumerable<Employee> _employeelist = await DataStore.GetAllAsync() as IEnumerable<Employee>;

            foreach (Employee item in _employeelist)
            {
                int d = await DataStore.DeleteAsync(item);
                Console.WriteLine("delete employee " + d);                                 
            }
        });




        #endregion

    }
}

