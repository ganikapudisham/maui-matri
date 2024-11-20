﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Matri.Business;

namespace AdminMatri.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    IServiceManager _serviceManager;
    public LoginViewModel(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [ObservableProperty]
    public string eMobile;

    [ObservableProperty]
    public string ePassword;

    [ObservableProperty]
    public bool isBusy;

    [ObservableProperty]
    public bool isHiddenPassword = true;

    [ObservableProperty]
    public bool newVersionPromptVisibility = false;

    [ObservableProperty]
    public string newVersionAvailableMessage;

    [ObservableProperty]
    public string currentAppVersion;

    [ObservableProperty]
    public string bgColor = "White";

    [ObservableProperty]
    public string customerCareNumber = "";

    [RelayCommand]
    public async Task Login()
    {
        var session = await _serviceManager.AdminLoginAsync(EMobile, EPassword);

        await SecureStorage.SetAsync("Token", session.SessionToken.ToString());

        await Shell.Current.GoToAsync("//leads");
    }
}
