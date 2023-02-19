using JobChannel.Mobile.Domain.BO;
using JobChannel.Mobile.MVVM.ViewsModels;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace JobChannel.Mobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly MainVM vm;

        public MainPage()
        {
            vm = new MainVM(this);
            InitializeComponent();

            DataContext = vm;
            MinDateCalendar.MinDate= new DateTimeOffset(DateTime.Now);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() => vm.RefreshRegions());
            Task.Run(() => vm.RefreshContracts());
            Task.Run(() => vm.RefreshJobs());
        }

        private void TokenBoxRegion_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                vm.RefreshSuggestedRegions(TokenBoxRegion.Text);
            }
        }

        private void TokenBoxContract_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                vm.RefreshSuggestedContracts(TokenBoxContract.Text);
            }
        }

        private void TokenBoxJob_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                vm.RefreshSuggestedJobs(TokenBoxJob.Text);
            }
        }
    }
}