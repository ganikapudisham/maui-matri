﻿using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Model;
using Matri.CustomExceptions;

namespace Matri.ViewModel
{
    public partial class SearchByIdViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        public SearchByIdViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [ObservableProperty]
        public string profileId;

        [ObservableProperty]
        public bool isBusy;

        [RelayCommand]
        public async Task Find() //3 letter prefix is mandatory
        {
            try
            {
                IsBusy = true;
                var sessionToken = await SecureStorage.GetAsync("Token");
                var targetProfileId = await _serviceManager.ConvertNumberToGuid(new Guid(sessionToken), ProfileId); ;

                //log the current user as visitor for the tapped profile
                await _serviceManager.CreateProfileVisitor(new Guid(sessionToken), targetProfileId);

                var profileDetailsInput = new ProfileDetailsInput();
                profileDetailsInput.LoggedInId = new Guid(sessionToken);
                profileDetailsInput.TargetProfileId = targetProfileId;

                var profileDetailsParams = new Dictionary<string, object> { { "ProfileDetailsInput", profileDetailsInput } };
                IsBusy = false;
                await Shell.Current.GoToAsync("profiledetails", profileDetailsParams);
            }
            catch (MatriInternetException exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
            catch (Exception exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception?.Message, "OK");
            }
        }

    }
}

