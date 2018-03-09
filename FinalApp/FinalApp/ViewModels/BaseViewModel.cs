using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using FinalApp.Model;
using SL;

namespace FinalApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public IDataStore<Employee> DataStore => DependencyService.Get<IDataStore<Employee>>() ?? new DataStore<Employee>("Data.db3");

        public BaseViewModel() {

            DataStore.CreateTableAsync();
        }

        #region the property 
        public INavigation _nav;
        public ContentPage CurrentPage { get; set; }
        public string title = "";
        public bool isBusy = false;
        #endregion

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
        public bool IsBusy
        {
            get
            {
                if (IsBusy == true)
                {
                    CurrentPage.IsEnabled = false;
                }
                else
                {
                    CurrentPage.IsEnabled = true;
                }
                return isBusy;
            }
            set
            {
                SetProperty(ref isBusy, value);
            }
        }


        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty(ref title, value);
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
