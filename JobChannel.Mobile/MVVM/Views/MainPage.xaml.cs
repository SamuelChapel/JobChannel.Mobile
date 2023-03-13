using JobChannel.Mobile.MVVM.ViewsModels;
using Microsoft.Toolkit.Uwp.UI;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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
            if (args.NewDate != args.OldDate && MaxDateCalendar.Date.HasValue && MinDateCalendar.Date.HasValue)
            {
                MaxDateCalendar.MinDate = MinDateCalendar.Date.Value;

                if (MaxDateCalendar.Date < MaxDateCalendar.MinDate)
                    MaxDateCalendar.Date = MaxDateCalendar.MinDate;

                //vm.SearchJobOffers();
            }
        }

        private void MaxDateCalendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate != args.OldDate && MaxDateCalendar.Date.HasValue && MinDateCalendar.Date.HasValue)
            {
                if (MaxDateCalendar.Date < MinDateCalendar.Date)
                {
                    MinDateCalendar.Date = MaxDateCalendar.Date.Value;
                }

                //vm.SearchJobOffers();
            }
        }

        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            MinDateCalendar.Date = MinDateCalendar.MinDate.Date;
            MaxDateCalendar.Date = MaxDateCalendar.MaxDate;

            vm.ResetFilters();
        }

        private void TokenBoxRegion_FocusEngaged(Control sender, FocusEngagedEventArgs args)
        {
            vm.RefreshSuggestedRegions(TokenBoxRegion.Text);
        }

        private void TokenBox_TokenItemAdded(TokenizingTextBox sender, object args)
        {
            vm.SearchJobOffers();
        }

        private void TokenBox_TokenItemRemoved(TokenizingTextBox sender, object args)
        {
            vm.SearchJobOffers();
        }

        private void SaveFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveFilters();
        }

        private void Expander_Expanded(object sender, EventArgs e)
        {
            List<Expander> expanders = JobOfferListView.FindDescendants().OfType<Expander>().ToList();
            expanders.Remove(sender as Expander);
            expanders.ForEach(expander => expander.IsExpanded = false);
        }

        private void SelectedRegion_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var stackPanel = sender as StackPanel;

        }
    }
}