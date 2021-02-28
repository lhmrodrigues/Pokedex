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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
