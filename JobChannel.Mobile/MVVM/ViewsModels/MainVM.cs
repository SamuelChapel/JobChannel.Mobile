using JobChannel.Mobile.Domain.BO;
using JobChannel.Mobile.Domain.Responses;
using JobChannel.Mobile.MVVM.Models;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace JobChannel.Mobile.MVVM.ViewsModels
{
    public class MainVM : ViewModelBase
    {
        private Page AssociatedPage;
        public string Titre { get; set; }

        public IncrementalLoadingCollection<JobOfferM, JobOfferFindResponse> JobOffers = new IncrementalLoadingCollection<JobOfferM, JobOfferFindResponse>();

        public ObservableCollection<Region> Regions = new ObservableCollection<Region>();
        public ObservableCollection<Region> SelectedRegions = new ObservableCollection<Region>();
        public ObservableCollection<Region> SearchRegions = new ObservableCollection<Region>();

        public MainVM(Page page)
        {
            AssociatedPage = page;
            Enumerable.Range(0, 15).Select(i => new Region(i, "Region " + i, "R" + i)).ToList().ForEach(region => Regions.Add(region));
        }

        public async void Test()
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride switch
            {
                "fr_FR" => "en-US",
                "en-US" => "fr-FR",
                _ => "fr-FR"
            };
            
            await Task.Delay(50);

            AssociatedPage.Frame.Navigate(typeof(MainPage));
        }

        public void RefreshFilter(string text = null)
        {
            SearchRegions.Clear();

            var regions = Regions.Except(SelectedRegions);

            if(!String.IsNullOrEmpty(text))
            {
                var textLower = text.ToLower();
                regions = regions.Where(r => r.Name.ToLower().Contains(textLower)).ToList();
            }

            regions.ToList().ForEach(region => SearchRegions.Add(region));
        }

        public void AddSelectedRegion(Region r)
        {
            SelectedRegions.Add(r);
            Regions.Remove(r);
        }
    }
}
