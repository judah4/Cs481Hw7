﻿using Prism.Commands;
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
		public DelegateCommand<Brewery> NavToMoreInfoPageCommand { get; set; }

        public DelegateCommand<Brewery> RemoveRowCommand { get; set; }

        public DelegateCommand RefreshListCommand { get; set; }

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

         private bool _refreshingList;
        public bool RefreshingList
        {
            get { return _refreshingList; }
            set { SetProperty(ref _refreshingList, value); }
        }

        

        private ObservableCollection<Brewery> _weatherCollection = new ObservableCollection<Brewery>();
        public ObservableCollection<Brewery> WeatherCollection
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
            NavToMoreInfoPageCommand = new DelegateCommand<Brewery>(NavToMoreInfoPage);
            RefreshListCommand = new DelegateCommand(OnRefreshListCommand);
            RemoveRowCommand = new DelegateCommand<Brewery>(OnRemoveRowCommand);

            Title = "Xamarin Forms Application + Prism";
            ButtonText = "Add Name";
            LocationEnteredByUser = "2010";

            
            //WeatherCollection.Add(new Brewery() {Description = "Tester", Name = "test brewski", Established = "2017", IsOrganic = "N"});
        }

        private void OnRefreshListCommand()
        {
            GetWeatherForLocation();
            RefreshingList = false;
        }

        private async void NavToMoreInfoPage(Brewery weatherItem)
        {
            var navParams = new NavigationParameters();
            navParams.Add("WeatherItemInfo", weatherItem);
            await _navigationService.NavigateAsync("MoreInfoPage", navParams);
        }

        private void OnRemoveRowCommand(Brewery weatherItem)
        {
            WeatherCollection.Remove(weatherItem);
        }

        internal async void GetWeatherForLocation()
        {
            WeatherCollection.Clear();


            if(string.IsNullOrEmpty(LocationEnteredByUser))
                return;

            MainPacket<Brewery> weatherData = await Breweries(1, "2010");

            if (weatherData != null)
            {

                foreach (var brewery in weatherData.Data.Take(10))
                {
                    WeatherCollection.Add(brewery);
                }
            }

        }

        public async Task<MainPacket<Brewery>> Breweries(int page, string established)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(
                string.Format($"http://api.brewerydb.com/v2/breweries?p={page}&established={established}&key={ApiKeys.ApiKey}"));
            var response = await client.GetAsync(uri);
            MainPacket<Brewery> weatherData = null;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                weatherData = JsonConvert.DeserializeObject<MainPacket<Brewery>>(content); //WeatherItem.FromJson(content);
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
            RefreshingList = true;
            GetWeatherForLocation();
            RefreshingList = false;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}

