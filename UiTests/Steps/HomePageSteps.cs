using BuisenessDomainInteractions.PageObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace UiTests.Steps
{
    [Binding]
    public class HomePageSteps
    {
        private HomePage _homepage;

        public HomePageSteps(HomePage homePage)
        {
            _homepage = homePage;
        }

        [Given(@"a user navigated to the base url")]
        public void GivenAUserNavigatedToTheBaseUrl()
        {
            _homepage.Navigate();
        }

        [Then(@"a user is on a home page")]
        public void ThenAUserIsOnAHomePage()
        {
            var isOnHomePage = _homepage.IsUserOnTheHomePage();
            isOnHomePage.Should().BeTrue();
        }
        [When(@"a user go to Workspaces")]
        public void WhenAUserGoToWorkspaces() 
        {
            _homepage.GoToWorkspaces();
        }
    }
}
