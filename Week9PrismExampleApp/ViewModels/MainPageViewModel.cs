using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Http;
using static Week9PrismExampleApp.Models.WeatherItemModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Week9PrismExampleApp.Models;

[assembly: InternalsVisibleTo("Week9PrismExampleUnitTests")]
namespace Week9PrismExampleApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
		public DelegateCommand NavToNewPageCommand { get; set; }
		public DelegateCommand GetWeatherForLocationCommand { get; set; }
		public DelegateCommand<WeatherItem> NavToMoreInfoPageCommand { get; set; }

		private string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set { SetProperty(ref _buttonText, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _locationEnteredByUser;
        public string LocationEnteredByUser
        {
            get { return _locationEnteredByUser; }
            set { SetProperty(ref _locationEnteredByUser, value); }
        }

        private ObservableCollection<BreweryDbModel.Brewery> _weatherCollection = new ObservableCollection<BreweryDbModel.Brewery>();
        public ObservableCollection<BreweryDbModel.Brewery> WeatherCollection
        {
            get { return _weatherCollection; }
            set { SetProperty(ref _weatherCollection, value); }
        }

        INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavToNewPageCommand = new DelegateCommand(NavToNewPage);
            GetWeatherForLocationCommand = new DelegateCommand(GetWeatherForLocation);
            NavToMoreInfoPageCommand = new DelegateCommand<WeatherItem>(NavToMoreInfoPage);

            Title = "Xamarin Forms Application + Prism";
            ButtonText = "Add Name";
        }

        private async void NavToMoreInfoPage(WeatherItem weatherItem)
        {
            var navParams = new NavigationParameters();
            navParams.Add("WeatherItemInfo", weatherItem);
            await _navigationService.NavigateAsync("MoreInfoPage", navParams);
        }

        internal async void GetWeatherForLocation()
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(
                string.Format($"http://api.brewerydb.com/v2/breweries?key={ApiKeys.ApiKey}"));
            var response = await client.GetAsync(uri);
            BreweryDbModel.MainPacket<BreweryDbModel.Brewery> weatherData = null;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                weatherData = JsonConvert.DeserializeObject<BreweryDbModel.MainPacket<BreweryDbModel.Brewery>>(content); //WeatherItem.FromJson(content);
            }
            if (weatherData != null)
            {
                foreach (var brewery in weatherData.Data)
                {
                    WeatherCollection.Add(brewery);
                }
            }

        }

        public async Task<BreweryDbModel.MainPacket<BreweryDbModel.Brewery>> Breweries(int page, int established)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(
                string.Format($"http://api.brewerydb.com/v2/breweries?p={page}&established={established}&key={ApiKeys.ApiKey}"));
            var response = await client.GetAsync(uri);
            BreweryDbModel.MainPacket<BreweryDbModel.Brewery> weatherData = null;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                weatherData = JsonConvert.DeserializeObject<BreweryDbModel.MainPacket<BreweryDbModel.Brewery>>(content); //WeatherItem.FromJson(content);
            }
            return weatherData;
        }

        private async void NavToNewPage()
        {
            var navParams = new NavigationParameters();
            navParams.Add("NavFromPage", "MainPageViewModel");
            await _navigationService.NavigateAsync("SamplePageForNavigation", navParams);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}

