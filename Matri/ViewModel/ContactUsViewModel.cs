using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Model;
using Matri.Model.Email;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace Matri.ViewModel
{
    public partial class ContactUsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        #region Fields

        //private ObservableRangeCollection<MapMarker> customMarkers;

        private Point geoCoordinate;

        #endregion

        IServiceManager _serviceManager;
        public ContactUsViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            Task.Run(async () => { await LoadTasks(); });
        }

        public async Task LoadTasks()
        {
            var affiliateContactDetails = await _serviceManager.GetAffiliateContactDetails("christianjodi.com");
            Name = affiliateContactDetails.Name;
            Telephone1 = affiliateContactDetails.Telephone1;
            Telephone2 = affiliateContactDetails.Telephone2;
            LandLine = affiliateContactDetails.Landline;
            Website = affiliateContactDetails.Website;
            Email = affiliateContactDetails.Email;
            Address1 = affiliateContactDetails.Address1;
            Address2 = affiliateContactDetails.Address2;
            Address3 = affiliateContactDetails.Address3;
            PinCode = affiliateContactDetails.Pincode;
            var lon = affiliateContactDetails.Longitude;
            var lat = affiliateContactDetails.Latitude;

            var sessionToken = await SecureStorage.GetAsync("Token");
            AppDetails = await _serviceManager.GetAppDetails(new Guid(sessionToken));
        }

        public Point GeoCoordinate
        {
            get
            {
                return this.geoCoordinate;
            }

            set
            {
                this.geoCoordinate = value;
            }
        }

        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string telephone1;
        [ObservableProperty]
        public string telephone2;

        [ObservableProperty]
        public string landLine;
        [ObservableProperty]
        public string website;
        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public string address1;
        [ObservableProperty]
        public string address2;
        [ObservableProperty]
        public string address3;
        [ObservableProperty]
        public string pinCode;

        [ObservableProperty]
        public string contactUsName;
        [ObservableProperty]
        public string contactUsEmail;
        [ObservableProperty]
        public string contactUsMessage;
        [ObservableProperty]
        public Model.App appDetails;
        [ObservableProperty]
        public bool isBusy;

        [RelayCommand]
        public async Task SendEmail()
        {
            var validationSuccess = await Validate();
            if (!validationSuccess) return;

            var contactUs = new ContactUs
            {
                Name = ContactUsName,
                Email = ContactUsEmail,
                Message = ContactUsMessage
            };

            try
            {
                IsBusy = true;
                var sessionToken = await SecureStorage.GetAsync("Token");
                await _serviceManager.ContactUs(new Guid(sessionToken), contactUs);
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Thank You, We will revert at the earliest.", "OK");
                IsBusy = false;
            }
            catch (MatriInternetException exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
            catch (Exception exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Failed To Send Message, Please Try Again", "OK");
            }
        }

        [RelayCommand]
        public async Task SendWhatsApp()
        {
            var validationSuccess = await Validate();
            if (!validationSuccess) return;

            try
            {
                bool supportsUri = await Launcher.Default.CanOpenAsync($"whatsapp://send?phone=+91{AppDetails.WAAdminNumber}");

                if (supportsUri)
                {
                    var message = $"{ContactUsName} {ContactUsEmail} {ContactUsMessage}";
                    await Launcher.Default.OpenAsync($"whatsapp://send?phone=+91{AppDetails.WAAdminNumber}&text={message}");
                }

                else
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Failed to open WhatsApp.", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        private async Task<bool> Validate()
        {
            if (string.IsNullOrWhiteSpace(ContactUsName))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Your Name", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ContactUsEmail))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Your Email", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ContactUsMessage))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Message", "OK");
                return false;
            }
            return true;
        }
    }
}
