using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Week9PrismExampleApp.ViewModels
{
    public class SamplePageForNavigationViewModel : BindableBase, INavigationAware
    {
        INavigationService _navigationService;

        public DelegateCommand GoBackCommand { get; set; }

		private string _pageFromText;
		public string PageFromText
		{
			get { return _pageFromText; }
			set { SetProperty(ref _pageFromText, value); }
		}

        public SamplePageForNavigationViewModel(INavigationService navigationService)
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

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
			if (parameters.ContainsKey("NavFromPage"))
			{
				var pageFrom = (string)parameters["NavFromPage"];
				PageFromText = $"Navigated from {pageFrom}";
			}
        }
    }
}
