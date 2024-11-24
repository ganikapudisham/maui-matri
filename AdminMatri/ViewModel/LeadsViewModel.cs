using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Syncfusion.Maui.DataGrid;
using CommunityToolkit.Maui.Core;
using Android.Widget;
using System;

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

        public ObservableRangeCollection<Lead> Leads { get; private set; } = new ObservableRangeCollection<Lead>();

        public LeadsViewModel(IServiceManager serviceManager, IPopupService popupService)
        {
            _serviceManager = serviceManager;
            this.popupService = popupService;
            IsBusy = true;
            Task.Run(async () => { await this.GetLeads(); });
            IsBusy = false;
        }

        public async Task<bool> GetLeads()
        {
            IsBusy = true;
            var token = await SecureStorage.GetAsync("Token");
            try
            {
                var dbLeads = await _serviceManager.GetLeads(token);
                //Leads.AddRange(dbLeads);
                foreach (var ld in dbLeads)
                {
                    Leads.Add(new Lead
                    {
                        Number = ld,
                        Whatsapp = "whatsapp.png",
                        Comment = "",
                        IsMarriageBureau = true,
                        MarkedAsCalled = true
                    });
                }
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
                    await this.popupService.ShowPopupAsync<CallCommentViewModel>(onPresenting: viewModel => viewModel.PerformUpdates($"{item.Comment}", false));
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

        [RelayCommand]

        public async Task SaveCall(object number)
        {
            this.popupService.ShowPopup<CallCommentViewModel>(onPresenting: viewModel => viewModel.PerformUpdates($"{number.ToString()}", true));
        }
    }

    public class Lead
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Whatsapp { get; set; }
        public string Comment { get; set; }
        public bool IsMarriageBureau { get; set; }
        public bool MarkedAsCalled { get; set; }
    }
}
