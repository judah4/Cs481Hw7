using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

[assembly: InternalsVisibleTo("Week9PrismExampleUnitTests")]
namespace Week9PrismExampleApp.ViewModels
{
    public class EasyTestPageViewModel : BindableBase
    {
        private ObservableCollection<string> _collectionOfNames = new ObservableCollection<string>();
        public ObservableCollection<string> CollectionOfNames
        {
            get { return _collectionOfNames; }
            set { SetProperty(ref _collectionOfNames, value); }
        }

        public EasyTestPageViewModel()
        {

        }

        internal int ReturnOne()
        {
            return 1;
        }

        internal void AddItemToCollection(string entry)
        {
            CollectionOfNames.Add(entry);
        }

        internal int AddTwoNumbers(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
