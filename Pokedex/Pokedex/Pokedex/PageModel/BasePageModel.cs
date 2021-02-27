using FreshMvvm;
using Pokedex.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex
{
    public class BasePageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        public SQLiteConnection Db { get; set; }
        public BasePageModel()
        {
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "pokemon.db3");
            Db = new SQLiteConnection(dbPath);
            Db.CreateTable<Pokemon>();

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsBusy { get; set; }
        public async Task DisplayAlert(string message)
        {
            await CoreMethods.DisplayAlert("", message, "Ok");
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(storage, value))
            {
                storage = value;
                this.OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
