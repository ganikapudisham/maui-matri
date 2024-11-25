using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Syncfusion.Maui.DataGrid;
using CommunityToolkit.Maui.Core;
using AdminMatri.FontsAwesome;
using Matri.Model;
using Syncfusion.Maui.Inputs;

//TODO : adding comment on popup and retrieving it back on the page
//TODO : save the call data
//TODO : add filter to fetch correct records by date
//TODO : add images to grid - whatsapp & comment icon

namespace AdminMatri.ViewModel
{
    public partial class LeadsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly IPopupService popupService;
        IServiceManager _serviceManager;

        [ObservableProperty]
        public bool flyoutIsPresented;

        [ObservableProperty]
        public bool isBusy = true;

        [ObservableProperty]
        public string comment = "";

        [ObservableProperty]
        public string floppyDisk = FontAwesomeIcons.FloppyDisk;

        public ObservableRangeCollection<Lead> Leads { get; private set; } = new ObservableRangeCollection<Lead>();

        public ObservableRangeCollection<Master> groupNames = new();

        public ObservableRangeCollection<Master> GroupNames
        {
            get { return groupNames; }
            set
            {
                groupNames = value;
            }
        }

        [ObservableProperty]
        private Master selectedGroupName;

        public LeadsViewModel(IServiceManager serviceManager, IPopupService popupService)
        {
            _serviceManager = serviceManager;
            this.popupService = popupService;
            IsBusy = true;
            Task.Run(async () =>
            {
                await this.GetLeads();
                var dbGroupNames = await _serviceManager.GetWhatsappGroups("");
                GroupNames.AddRange(dbGroupNames);
                SelectedGroupName = GroupNames[0];
            });
            IsBusy = false;
        }

        public async Task<bool> GetLeads()
        {
            IsBusy = true;
            var token = await SecureStorage.GetAsync("Token");
            try
            {
                var dbLeads = await _serviceManager.GetLeads(token);
                Leads.AddRange(dbLeads);
                IsBusy = false;
                return true;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                return false;
            }
        }

        [RelayCommand]
        public async Task CellTap(Object obj)
        {
            var args = obj as DataGridCellTappedEventArgs;
            if (args != null)
            {
                var rowIndex = args.RowColumnIndex.RowIndex;
                var rowData = args.RowData;
                var item = (Lead)rowData;
                var columnIndex = args.RowColumnIndex.ColumnIndex;
                var column = args.Column;

                if (columnIndex == 0)//number
                {
                    call(item.Number);
                }
                else if (columnIndex == 1)//whatsapp
                {
                    await whatsapp(item.Number);
                }
                else if (columnIndex == 2)//comment
                {
                    await this.popupService.ShowPopupAsync<CallCommentViewModel>(onPresenting: viewModel => viewModel.PerformUpdates(item, false));
                }
                else if (columnIndex == 5)
                {
                    await SaveCall(item);
                }
            }
        }

        private void call(string user)
        {
            if (PhoneDialer.Default.IsSupported)
            {
                PhoneDialer.Default.Open(user);
            }
        }

        private async Task whatsapp(string user)
        {
            try
            {
                bool supportsUri = await Launcher.Default.CanOpenAsync($"whatsapp://send?phone=+{user}");

                if (supportsUri)
                {
                    var message = "";
                    await Launcher.Default.OpenAsync($"whatsapp://send?phone=+{user}&text={message}");
                }

                else
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Failed to open WhatsApp.", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        public async Task SaveCall(object obj)
        {
            var lead = obj as Lead;
            await this.popupService.ShowPopupAsync<CallCommentViewModel>(onPresenting: viewModel => viewModel.PerformUpdates(lead, true));
            Leads.Clear();
            await this.GetLeads();
        }
    }
}
