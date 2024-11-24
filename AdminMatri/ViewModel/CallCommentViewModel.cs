using AdminMatri.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Business;
using System.Windows.Input;
using Matri.Model;

namespace AdminMatri.ViewModel;

public partial class CallCommentViewModel : ObservableObject
{

    IServiceManager _serviceManager;

    [ObservableProperty]
    public string comment;

    [ObservableProperty]
    public bool isSave = true;

    public Lead lead = new Lead();

    public CallCommentViewModel(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public void PerformUpdates(Lead _lead, bool isSave)
    {
        lead = _lead;
        Comment = lead.Comment;
        IsSave = isSave;
    }

    [RelayCommand]
    public async Task Save()
    {
        lead.Comment = Comment;

        if (lead.IsCalled == false)
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please call & check call", "Ok");
            return;
        }
        else if (string.IsNullOrEmpty(lead.Comment))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please add comment", "Ok");
            return;
        }
        else
        {
            var success = await _serviceManager.UpdateLeadCall(lead);

            if (success)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Record is updated", "Ok");
            }
            //TODO Close Dialog
        }
    }
}
