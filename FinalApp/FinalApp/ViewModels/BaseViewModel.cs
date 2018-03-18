using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using FinalApp.Model;

using Root.Services.Sqlite;

namespace FinalApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public IDataStore<Employee> DataStore => DependencyService.Get<IDataStore<Employee>>() ?? new DataStore<Employee>("Data.db3");
        public IDataStore<Admin> DataStoreAdmin => DependencyService.Get<IDataStore<Admin>>() ?? new DataStore<Admin>("Data.db3");
        public BaseViewModel() {

            DataStore.CreateTableAsync();
            DataStoreAdmin.CreateTableAsync();
        }

        #region the property 
        public INavigation _nav;
        public ContentPage CurrentPage { get; set; }
        string title = string.Empty;
        public bool isBusy = false;
        public bool pageIsBusy = false;
        #endregion





        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
       
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #region NotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value,
          [CallerMemberName]string propertyName = "",
          Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region setting of the property
        public bool PageIsBusy
        {
            get
            {
                if (PageIsBusy == true)
                {
                    CurrentPage.IsEnabled = false;
                }
                else
                {
                    CurrentPage.IsEnabled = true;
                }
                return pageIsBusy;
            }
            set
            {
                SetProperty(ref pageIsBusy, value);
            }
        }


  
        #endregion

        #region the navigation handler members

        public virtual void OpenPage()
        {
            if (CurrentPage != null)
            {
                CurrentPage.BindingContext = this; // using this we don't need to do the binding
                _nav.PushAsync(CurrentPage);
            }
        } 

        #endregion

    }
}
