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
            MinDateCalendar.MinDate = new DateTimeOffset(DateTime.Now.AddMonths(-6));
            MaxDateCalendar.MinDate = MinDateCalendar.MinDate;
            MinDateCalendar.MaxDate = new DateTimeOffset(DateTime.Today);
            MaxDateCalendar.MaxDate = MinDateCalendar.MaxDate;

            MinDateCalendar.Date = MinDateCalendar.MinDate;
            MaxDateCalendar.Date = MaxDateCalendar.MaxDate;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() => vm.RefreshRegions());
            Task.Run(() => vm.RefreshContracts());
            Task.Run(() => vm.RefreshJobs());

            Task.Run(() => vm.SearchJobOffers());
        }

        private void TokenBoxRegion_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
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

        private void MinDateCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            MaxDateCalendar.MinDate = MinDateCalendar.Date.Value;

            if (MaxDateCalendar.Date < MaxDateCalendar.MinDate)
                MaxDateCalendar.Date = MaxDateCalendar.MinDate;
        }

        private void MaxDateCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (MaxDateCalendar.Date < MinDateCalendar.Date)
            {
                MinDateCalendar.Date = MaxDateCalendar.Date;
            }
        }

        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            MinDateCalendar.Date = MinDateCalendar.MinDate;
            MaxDateCalendar.Date = MaxDateCalendar.MaxDate;

            TokenBoxContract.ClearAsync();
            TokenBoxJob.ClearAsync();
            TokenBoxRegion.ClearAsync();
        }

        private void TokenBoxRegion_FocusEngaged(Control sender, FocusEngagedEventArgs args)
        {
            vm.RefreshSuggestedRegions(TokenBoxRegion.Text);
        }

        private void TokenBoxRegion_TokenItemAdded(Microsoft.Toolkit.Uwp.UI.Controls.TokenizingTextBox sender, object args)
        {
            vm.SearchJobOffers();
        }

        private void TokenBoxRegion_TokenItemRemoved(Microsoft.Toolkit.Uwp.UI.Controls.TokenizingTextBox sender, object args)
        {
            vm.SearchJobOffers();
        }
    }
}