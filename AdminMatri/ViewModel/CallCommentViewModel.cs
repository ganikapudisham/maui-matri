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
        await _serviceManager.UpdateLeadCall(lead);
        //TODO Close Dialog
    }
}
