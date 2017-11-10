using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Moq;
using Prism.Navigation;
using Week9PrismExampleApp.ViewModels;

namespace Week9PrismExampleUnitTests
{
    [TestFixture()]
    public class ApiTesting
    {
        MainPageViewModel easyTestPageViewModel;

        [SetUp]
        public void Init()
        {
            var navigationServiceMock = new Mock<INavigationService>();
            easyTestPageViewModel = new MainPageViewModel(navigationServiceMock.Object);
        }

        [Test]
        public async Task TestApiData()
        {
            var result = await easyTestPageViewModel.Breweries(1, "2010");

            Assert.AreEqual(true, result.Data.Count > 0);
            foreach (var brewery in result.Data)
            {
                Assert.AreEqual(true, brewery.Name != null);
            }


        }

        
    }
}
