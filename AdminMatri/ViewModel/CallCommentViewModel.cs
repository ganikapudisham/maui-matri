using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Business;

namespace AdminMatri.ViewModel;

public partial class CallCommentViewModel : ObservableObject
{
    IServiceManager _serviceManager;

    [ObservableProperty]
    public string comment;

    [ObservableProperty]
    public bool isSave = true;

    public CallCommentViewModel(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public void PerformUpdates(string showComment, bool isSave)
    {
        //await _serviceManager.UpdateLeadCall(number_comment);
        Comment = showComment;
        IsSave = isSave;
    }

    [RelayCommand]
    public async void Save(string number_comment)
    {
        IsSave = true;
        await _serviceManager.UpdateLeadCall(number_comment);
       
    }
}
