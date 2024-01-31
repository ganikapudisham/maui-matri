using CommunityToolkit.Mvvm.ComponentModel;
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
            //this.CustomMarkers = new ObservableRangeCollection<MapMarker>();
            //this.GetPinLocation();
            Task.Run(async () => { await LoadTasks(); });
        }

        public async Task LoadTasks()
        {
            var userApp = DependencyService.Get<IAppVersionProvider>();
            var affiliateContactDetails = await _serviceManager.GetAffiliateContactDetails(userApp.AppWebsite);
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
        }

        /// <summary>
        /// Gets or sets the CustomMarkers collection.
        /// </summary>
            //public ObservableRangeCollection<MapMarker> CustomMarkers
            //{
            //    get
            //    {
            //        return this.customMarkers;
            //    }

            //    set
            //    {
            //        this.customMarkers = value;
            //    }
            //}

            /// <summary>
            /// Gets or sets the geo coordinate.
            /// </summary>
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

        /// <summary>
        /// This method is for getting the pin location detail.
        /// </summary>
        //private void GetPinLocation()
        //{
        //    this.CustomMarkers.Add(
        //        new LocationMarker
        //        {
        //            PinImage = "Pin.png",
        //            Header = Name.Value,
        //            Address = $"{Address1.Value} {Address2.Value} {Address3.Value} {PinCode.Value}",
        //            EmailId = Email.Value,
        //            PhoneNumber = Telephone1.Value,
        //            CloseImage = FontAwesomeIcons.FaTimes,
        //            //Latitude = "17.439930",
        //            //Longitude = "78.498276"
        //        });

        //    foreach (var marker in this.CustomMarkers)
        //    {
        //        this.GeoCoordinate = new Point(Convert.ToDouble(marker.Latitude, CultureInfo.CurrentCulture), Convert.ToDouble(marker.Longitude, CultureInfo.CurrentCulture));
        //    }
        //}

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

        public void SendEmailCommand()
        {
            Task.Run(async () => { await SendEmailCommandAsync(); });
        }

        public void SendWhatsAppCommand()
        {
            Task.Run(async () => { await SendWhatsAppCommandAsync(); });
        }

        //public override async Task Initialize()
        //{
        //    await base.Initialize();

        //    var sessionToken = await SecureStorage.GetAsync("Token");
        //    AppDetails.Value = await _serviceManager.GetAppDetails(new Guid(sessionToken));
        //}

        private async Task SendEmailCommandAsync()
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
                var sessionToken = await SecureStorage.GetAsync("Token");
                await _serviceManager.ContactUs(new Guid(sessionToken), contactUs);
                //await _userDialogs.AlertAsync("Thank You, We will get back to you at the earliest.");
            }
            catch (MatriInternetException exception)
            {
                //await _userDialogs.AlertAsync(exception.Message);
            }
            catch (Exception exception)
            {
                //await _userDialogs.AlertAsync("Failed To Send Message, Please Try Again");
            }
        }

        private async Task SendWhatsAppCommandAsync()
        {
            var validationSuccess = await Validate();

            if (!validationSuccess) return;

            try
            {
                //Chat.Open($"+91{AppDetails.WAAdminNumber}", $"{ContactUsName} {ContactUsEmail} {ContactUsMessage}");
            }
            catch (Exception ex)
            {
                //await _userDialogs.AlertAsync(ex.Message);
            }
        }

        private async Task<bool> Validate()
        {
            if (string.IsNullOrWhiteSpace(ContactUsName))
            {
                //await _userDialogs.AlertAsync("Please Specify Your Name");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ContactUsEmail))
            {
                //await _userDialogs.AlertAsync("Please Specify Your Email");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ContactUsMessage))
            {
                //await _userDialogs.AlertAsync("Please Specify Message");
                return false;
            }

            return true;
        }
    }
}
