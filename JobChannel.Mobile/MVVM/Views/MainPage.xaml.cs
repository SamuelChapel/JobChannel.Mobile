using JobChannel.Mobile.Domain.BO;
using JobChannel.Mobile.MVVM.ViewsModels;
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
        }

        private void TokenBoxRegion_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                vm.RefreshSuggestedRegions(TokenBoxRegion.Text);
            }
        }

        private void RegionList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem != null && e.ClickedItem is Region r)
            {
                TokenBoxRegion.Text = string.Empty;

                if (TokenBoxRegion.Items.Count < 6)
                {
                    TokenBoxRegion.AddTokenItem(r);
                }

                vm.RefreshSuggestedRegions();
                TokenBoxRegion.Focus(FocusState.Programmatic);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() => vm.RefreshRegions());
        }
    }
}