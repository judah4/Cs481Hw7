using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Week9PrismExampleApp.Models;
using Xamarin.Forms.Xaml;
using static Week9PrismExampleApp.Models.WeatherItemModel;

namespace Week9PrismExampleApp.ViewModels
{
    public class MoreInfoPageViewModel : BindableBase, INavigatedAware
    {
		INavigationService _navigationService;

		public DelegateCommand GoBackCommand { get; set; }

        private Brewery _weatherItem;
        public Brewery Brewery
        {
            get { return _weatherItem; }
            set { SetProperty(ref _weatherItem, value); }
        }

        public MoreInfoPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;

			GoBackCommand = new DelegateCommand(GoBack);
		}

		private void GoBack()
		{
			_navigationService.GoBackAsync();
		}

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("WeatherItemInfo"))
            {
                Brewery = (Brewery)parameters["WeatherItemInfo"];
            }
        }
    }
}
