using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.Model;
using Newtonsoft.Json;
namespace Matri.ViewModel
{
    public class EditProfileViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        public EditProfileViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        //public override void ViewAppearing()
        //{
        //    Task.Run(async () => { await AppearingAsync(); });
        //}

        //private async Task AppearingAsync()
        //{
        //    var sessionToken = await SecureStorage.GetAsync("Token");
        //    var userDetails = await _serviceManager.GetUserData(new Guid(sessionToken));

        //    //await _navigationService.Navigate<EditBasicViewModel, Profile>(userDetails);
        //    //await _navigationService.Navigate<EditReligionViewModel, Profile>(userDetails);
        //    //await _navigationService.Navigate<EditPhysicalViewModel, Profile>(userDetails);
        //    //await _navigationService.Navigate<EditAcademicsViewModel, Profile>(userDetails);
        //    //await _navigationService.Navigate<EditContactViewModel, Profile>(userDetails);
        //    //await _navigationService.Navigate<EditFamilyViewModel, Profile>(userDetails);
        //    //await _navigationService.Navigate<EditLifeStyleViewModel,Profile>(userDetails);
        //    //await _navigationService.Navigate<EditPhotoViewModel, Profile>(userDetails);
        //    //await _navigationService.Navigate<EditHoroscopeViewModel, Profile>(userDetails);
        //    //await _navigationService.Navigate<EditExpectationsViewModel, Profile>(userDetails);

        //}
    }
}

