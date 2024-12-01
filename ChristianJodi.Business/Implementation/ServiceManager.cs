#if Android
using Android.Content;
using Android.Provider;
#endif
using Matri.Data;
using Matri.Model;
using Matri.Model.Email;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.ComponentModel;
using System.Net;

namespace Matri.Business.Impl;

public class ServiceManager : IServiceManager
{
    IServiceRepository _serviceRepository;
    string ImagesRepo = Constants.API_URL_ImagesRepo;//"https://api.christianjodi.com/";

    //public event EventHandler<DownloadEventArgs> OnFileDownloaded;
    public ServiceManager(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }
    public async Task<Authentication> LoginAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username)) throw new ArgumentException($"Pin not provided");

        if (string.IsNullOrEmpty(password)) throw new ArgumentException($"Password not provided");

        var authenticationRequest = new Authentication { UserName = username, Password = password };
        var url = Constants.API_URL_Authentication; //"sessiontoken";
        var response = await _serviceRepository.PostAsync<Authentication, Authentication>(Guid.Empty.ToString(), url, authenticationRequest);
        return response;
    }
    public async Task<bool> ForgetPasswordAsync(string username)
    {
        string url = $"password/forgot?email={username}";
        var response = await _serviceRepository.GetAsync<bool>(Guid.Empty.ToString(), url);
        return response;
    }

    public async Task<RegisterUser> RegisterUserAsync(string firstName, string lastName, string userName,
        string password, string confirmPassword, string gender, DateTime birthDate, string website)
    {
        //var datePickerValue = "01-12-1999 00:00:00";
        //var APIbirthDateFormat = "2002-12-01";

        var BirthDateInApiFormat = $"{birthDate.Year}-{birthDate.Month:00}-{birthDate.Day:00}";

        if (string.IsNullOrEmpty(firstName)) throw new ArgumentException($"Please enter FirstName");

        if (string.IsNullOrEmpty(lastName)) throw new ArgumentException($"Please enter LastName");

        if (string.IsNullOrEmpty(userName)) throw new ArgumentException($"Please enter Mobile number");

        if (string.IsNullOrEmpty(password)) throw new ArgumentException($"Please enter Password");

        if (string.IsNullOrEmpty(password)) throw new ArgumentException($"Please enter Confirm Password");

        if (string.IsNullOrEmpty(gender)) throw new ArgumentException($"Please specify Gender");

        var newUser = new RegisterUser
        {
            FirstName = firstName,
            LastName = lastName,
            Mobile = userName,
            Password = password,
            ConfirmPassword = password,
            Gender = gender,
            Birthdate = BirthDateInApiFormat,
            AffiliatePortal = website
        };

        string url = Constants.API_URL_Register; //"mini/signup";

        var response = await _serviceRepository.PostAsync<RegisterUser, RegisterUser>(Guid.Empty.ToString(), url, newUser);

        return response;
    }

    public async Task<Paging<MiniProfile>> GetProfiles(string sessiontoken, int page = 1, int perpage = 5,
        string sortby = "Id", string order = "Desc", string filterby = "All", bool newProfiles = false)
    {
        var modifiedPofiles = new Paging<MiniProfile>();
        var userL = new List<MiniProfile>();

        var url = $"miniprofiles?perpage={perpage}&page={page}&sortby={sortby}&order={order}&filterby={filterby}&newProfiles={newProfiles}";

        var dbProfiles = await _serviceRepository.GetAsync<Paging<MiniProfile>>(sessiontoken.ToString(), url);

        foreach (var profile in dbProfiles.Data)
        {
            var profileToFormat = new MiniProfile();
            profileToFormat = profile;

            if (profile.ThumbNails.Any())
            {
                profileToFormat.ThumbNailUri = new Uri($"{ImagesRepo}/thumbs/{profile.ThumbNails[0].Name}");
            }

            if (!string.IsNullOrWhiteSpace(profile.BirthDate))
            {
                var year = Convert.ToInt16(profile.BirthDate.Substring(0, 4));
                int age = DateTime.Now.Year - year;
                profileToFormat.Age = $"{age}yrs";
            }
            var heightInFeet_And_Inches = profile.Height.Split("-")[0];
            var heightHasNoInches = heightInFeet_And_Inches.Split(" ").Length == 2;

            if (heightHasNoInches)
            {
                profileToFormat.Height = heightInFeet_And_Inches.Replace("ft", "\"").Trim();
            }
            else
            {
                profileToFormat.Height = heightInFeet_And_Inches.Replace("ft", "\'").Replace("in", "\"").Replace(" ", "").Trim();
            }

            userL.Add(profileToFormat);
        }
        modifiedPofiles.Data = userL;
        modifiedPofiles.MetaData = dbProfiles.MetaData;
        return modifiedPofiles;
    }

    public async Task<bool> LogoutAsync(string sessiontoken)
    {
        return await _serviceRepository.LogOut(sessiontoken);
    }

    public async Task<Profile> GetProfileById(string sessiontoken, Guid profileId)
    {
        var url = $"profiles/{profileId}";
        var profileDetails = await _serviceRepository.GetAsync<Profile>(sessiontoken.ToString(), url);
        foreach (var ph in profileDetails.Photos)
        {
            ph.Name = $"{ImagesRepo}/photos/{ph.Name}";
        }
        return profileDetails;
    }

    public async Task<Guid> ConvertNumberToGuid(string sessionToken, string profileId)
    {
        var url = $"profiles/user?id={profileId}";
        var profileGuid = await _serviceRepository.GetAsync<Guid>(sessionToken.ToString(), url);
        return profileGuid;
    }
    public async Task<Mini> GetShortDetailsOfLoggedInUser(string sessiontoken)
    {
        string url = Constants.API_URL_GetMiniDetailsOfLoggedInUser;// "mini";
        var miniDetailsOfLoggedInUser = await _serviceRepository.GetAsync<Mini>(sessiontoken.ToString(), url);
        miniDetailsOfLoggedInUser.ThumbNail = $"{ImagesRepo}/thumbs/{miniDetailsOfLoggedInUser.ThumbNail}";
        return miniDetailsOfLoggedInUser;
    }

    public async Task<AffiliateContactDetails> GetAffiliateContactDetails(string affiliatePortal)
    {
        var url = $"affiliates?affiliatePortal={affiliatePortal}";
        var affiliateContactDetails = await _serviceRepository.GetAsync<AffiliateContactDetails>(Guid.Empty.ToString(), url);
        return affiliateContactDetails;
    }

    public async Task<bool> ChangePasswordAsync(string guid, Password password)
    {
        var url = Constants.API_URL_ChangePassword; //"password/change";
        return await _serviceRepository.PutAsync<Password, bool>(guid.ToString(), url, password);
    }

    public async Task<bool> CreateProfileVisitor(string sessiontoken, Guid targetProfile)
    {
        var url = "";
        return await _serviceRepository.CreateProfileVisitor(sessiontoken, targetProfile);
    }

    public async Task<Paging<MiniProfile>> GetMarkedProfiles(string sessiontoken, string markedAs, int page = 1,
        int perpage = 5)
    {
        var modifiedPofiles = new Paging<MiniProfile>();
        var userL = new List<MiniProfile>();
        var url = $"profiles/marked?marked={markedAs}&perpage={perpage}&page={page}";
        var dbProfiles = await _serviceRepository.GetAsync<Paging<MiniProfile>>(sessiontoken.ToString(), url);

        foreach (var profile in dbProfiles.Data)
        {
            var profileToFormat = new MiniProfile();
            profileToFormat = profile;

            if (profile.ThumbNails.Any())
            {
                profileToFormat.ThumbNailUri = new Uri($"{ImagesRepo}/thumbs//{profile.ThumbNails[0].Name}");
            }

            if (!string.IsNullOrWhiteSpace(profile.BirthDate))
            {
                var year = Convert.ToInt16(profile.BirthDate.Substring(0, 4));
                int age = DateTime.Now.Year - year;
                profileToFormat.Age = $"{age}yrs";
            }
            userL.Add(profileToFormat);
        }
        modifiedPofiles.Data = userL;
        modifiedPofiles.MetaData = dbProfiles.MetaData;
        return modifiedPofiles;
    }

    public async Task<Paging<Visitor>> GetProfileVisitors(string sessiontoken, int startPage = 1,
        int pageSize = 5)
    {
        var visitors = new Paging<Visitor>();
        var url = $"visitors?startPage={startPage}&pageSize={pageSize}";
        visitors = await _serviceRepository.GetAsync<Paging<Visitor>>(sessiontoken.ToString(), url);
        var tempVisitors = new Paging<Visitor>();

        foreach (var visitor in visitors.Data)
        {
            var tempVisitor = new Visitor();
            tempVisitor = visitor;
            tempVisitor.FirstName = $"{visitor.FirstName}  - {visitor.WebsiteIdentifier}{visitor.Number}";
            tempVisitor.VisitedOn = $"Visited On {visitor.VisitedOn}";
            //tempVisitor.LastActiveOn = $"Active On {visitor.LastActiveOn}";
            //tempVisitor.VisitedTimes = $"Visited {visitor.VisitedTimes} Times"; //TODO
            tempVisitors.Data.Add(tempVisitor);
        }

        return tempVisitors;
    }

    public async Task<bool> MarkProfile(string sessionToken, Request request)
    {
        string url = "requests";
        return await _serviceRepository.PostAsync<Request, bool>(sessionToken.ToString(), url, request);
    }

    public async Task<Paging<RequestSent>> GetRequestSent(string sessiontoken, int page = 1, int perpage = 5)
    {
        var modifiedSentRequests = new Paging<RequestSent>();
        var requestSentLocal = new List<RequestSent>();
        var url = $"requests/sent?page={page}&perpage={perpage}";
        var requestsSent = await _serviceRepository.GetAsync<Paging<RequestSent>>(sessiontoken.ToString(), url);

        foreach (var reqSent in requestsSent.Data)
        {
            var _requestSent = new RequestSent();
            _requestSent = reqSent;
            _requestSent.ReceiverFirstName = $"{reqSent.ReceiverFirstName} {reqSent.WebsiteIdentifier}{reqSent.ReceiverNumber}";
            _requestSent.SentDate = $"Request sent on {reqSent.SentDate}";
            _requestSent.Type = $"{reqSent.Type}";
            //_requestSent.Type = $"{reqSent.Type}";Active On

            if (reqSent.ThumbNails.Any())
            {
                _requestSent.ThumbNailUri = new Uri($"{ImagesRepo}/thumbs//{reqSent.ThumbNails[0].Name}");
            }
            requestSentLocal.Add(_requestSent);
        }
        modifiedSentRequests.Data = requestSentLocal;
        modifiedSentRequests.MetaData = requestsSent.MetaData;
        return modifiedSentRequests;

    }

    public async Task<Paging<RequestReceived>> GetRequestReceived(string sessiontoken, int page = 1, int perpage = 5)
    {
        var modifiedReceivedRequests = new Paging<RequestReceived>();
        var requestReceivedLocal = new List<RequestReceived>();
        var url = $"requests/received?page={page}&perpage={perpage}";
        var requestsReceived = await _serviceRepository.GetAsync<Paging<RequestReceived>>(sessiontoken.ToString(), url);

        foreach (var reqSent in requestsReceived.Data)
        {
            var _requestReceived = new RequestReceived();
            _requestReceived = reqSent;
            _requestReceived.SenderFirstName = $"{reqSent.SenderFirstName} {reqSent.WebsiteIdentifier}{reqSent.SenderNumber}";
            _requestReceived.SentDate = $"Sent on {reqSent.SentDate}";
            _requestReceived.Type = $"{reqSent.Type}";
            //_requestSent.Type = $"{reqSent.Type}";Active On

            if (reqSent.ThumbNails.Any())
            {
                _requestReceived.ThumbNailUri = new Uri($"{ImagesRepo}/thumbs/{reqSent.ThumbNails[0].Name}");
            }
            requestReceivedLocal.Add(_requestReceived);
        }
        modifiedReceivedRequests.Data = requestReceivedLocal;
        modifiedReceivedRequests.MetaData = requestsReceived.MetaData;
        return modifiedReceivedRequests;

    }

    public async Task<bool> ContactUs(string sessiontoken, ContactUs contactUs)
    {
        return await _serviceRepository.PostAsync<ContactUs, bool>(sessiontoken.ToString(), Constants.API_URL_ContactUs, contactUs);
    }

    public async Task<MDD> GetMasterData(string sessiontoken)
    {
        return await _serviceRepository.GetAsync<MDD>(sessiontoken.ToString(), Constants.API_URL_MasterData);
    }

    public async Task<Paging<MiniProfile>> Search(string sessiontoken, SearchParameters searchParameters)
    {
        var modifiedPofiles = new Paging<MiniProfile>();
        var userL = new List<MiniProfile>();

        /* Search  */
        var url = $"profiles/search?perpage={searchParameters.PageSize}&page={searchParameters.StartPage}" +
            $"&maritalstatus={searchParameters.MaritalStatus}&mothertongue={searchParameters.MotherTongue}" +
            $"&religion={searchParameters.Religion}&caste={searchParameters.Caste}&subcaste={searchParameters.SubCaste}" +
            $"&community={searchParameters.Community}&denomination={searchParameters.Denomination}" +
            $"&photo={searchParameters.WithPhoto}&ageFrom={searchParameters.AgeFrom}&ageTo={searchParameters.AgeTo}" +
            $"&state={searchParameters.State}&districtRegion={searchParameters.DistrictRegion}" +
            $"&education={searchParameters.Education}&job={searchParameters.Job}" +
            $"&residingCountry={searchParameters.ResidingCountry}&heightFrom={searchParameters.HeightFrom}" +
            $"&heightTo={searchParameters.HeightTo}";

        var dbProfiles = await _serviceRepository.GetAsync<Paging<MiniProfile>>(sessiontoken.ToString(), url);

        /* end of search */

        foreach (var profile in dbProfiles.Data)
        {
            var profileToFormat = new MiniProfile();
            profileToFormat = profile;

            if (profile.ThumbNails.Any())
            {
                profileToFormat.ThumbNailUri = new Uri($"{ImagesRepo}/thumbs/{profile.ThumbNails[0].Name}");
            }

            if (!string.IsNullOrWhiteSpace(profile.BirthDate))
            {
                var year = Convert.ToInt16(profile.BirthDate.Substring(0, 4));
                int age = DateTime.Now.Year - year;
                profileToFormat.Age = $"{age}yrs";
            }
            userL.Add(profileToFormat);
        }
        modifiedPofiles.Data = userL;
        modifiedPofiles.MetaData = dbProfiles.MetaData;
        return modifiedPofiles;
    }

    public async Task<Model.App> GetAppDetails(string sessiontoken)
    {
        var url = Constants.API_URL_GetAppDetails; //"app";
        return await _serviceRepository.GetAsync<Model.App>(sessiontoken.ToString(), url);
    }

    public async Task<Profile> GetUserData(string sessiontoken)
    {
        var url = Constants.API_URL_GetUserProfile;// "profiles/mine";
        return await _serviceRepository.GetAsync<Profile>(sessiontoken.ToString(), url);
    }

    public async Task<bool> UpdateBasicDetails(string sessiontoken, Profile profile)
    {
        var url = Constants.API_URL_UpdateUserBasics;
        return await UpdateProfileDetails(sessiontoken, profile, url);
    }

    private async Task<bool> UpdateProfileDetails(string sessiontoken, Profile profile, string url)
    {
        return await _serviceRepository.PutAsync<Profile, bool>(sessiontoken.ToString(), url, profile);
    }

    public async Task<bool> UploadProfilePhoto(MultipartFormDataContent formData)
    {
        return await _serviceRepository.UploadProfilePhoto(formData, Constants.API_URL_AdminUploadOCRFile);
    }

    public async Task<bool> UpdateEducationDetails(string sessiontoken, Profile profile)
    {
        var url = Constants.API_URL_UpdateUserEducation;
        return await UpdateProfileDetails(sessiontoken, profile, url);
    }

    public async Task<bool> UpdateContactDetails(string sessiontoken, Profile profile)
    {
        var url = Constants.API_URL_UpdateUserContact;
        return await UpdateProfileDetails(sessiontoken, profile, url);
    }

    public async Task<bool> UpdateFamilyDetails(string sessiontoken, Profile profile)
    {
        var url = Constants.API_URL_UpdateUserFamily;
        return await UpdateProfileDetails(sessiontoken, profile, url);
    }

    public async Task<bool> UpdateLifeStyleDetails(string sessiontoken, Profile profile)
    {
        var url = Constants.API_URL_UpdateUserLifeStyle;
        return await UpdateProfileDetails(sessiontoken, profile, url);
    }

    public async Task<bool> UpdatePhysicalDetails(string sessiontoken, Profile profile)
    {
        var url = Constants.API_URL_UpdateUserPhysical;
        return await UpdateProfileDetails(sessiontoken, profile, url);
    }

    public async Task<bool> UpdateReligionDetails(string sessiontoken, Profile profile)
    {
        var url = Constants.API_URL_UpdateUserReligion;
        return await UpdateProfileDetails(sessiontoken, profile, url);
    }

    public async Task<bool> UpdateExpectations(string sessiontoken, Partner partner)
    {
        var url = Constants.API_URL_UpdateUserExpectation;
        return await _serviceRepository.PutAsync<Partner, bool>(sessiontoken.ToString(), url, partner);
    }

    public async Task<bool> CreateUpdateDeviceToken(string sessiontoken, FCMToken fcmToken)
    {
        var url = Constants.API_URL_FCM;
        return await _serviceRepository.PostAsync<FCMToken, bool>(sessiontoken.ToString(), url, fcmToken);
    }

    public async Task<List<FCMToken>> GetUserDeviceTokens(string sessiontoken, string notificationRecipient)
    {
        var url = $"{Constants.API_URL_FCM}?notificationRecipient={notificationRecipient}";
        return await _serviceRepository.GetAsync<List<FCMToken>>(sessiontoken.ToString(), url);
    }

    public async Task<Profile> GetProfileByIdWithoutAuth(Guid profileId)
    {
        var url = $"{Constants.API_URL_NotificationFrom}/{profileId}";
        return await _serviceRepository.GetAsync<Profile>(Guid.Empty.ToString(), url);
    }

    public async Task<List<RequestSent>> GetAllRequests(string sessiontoken)
    {
        return await _serviceRepository.GetAsync<List<RequestSent>>(sessiontoken.ToString(), Constants.API_URL_AllRequests);
    }

    public async Task<Authentication> AdminLoginAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username)) throw new ArgumentException($"Pin not provided");

        if (string.IsNullOrEmpty(password)) throw new ArgumentException($"Password not provided");

        var authenticationRequest = new Authentication { UserName = username, Password = password };
        var url = Constants.API_URL_AdminAuthentication; //"sessiontoken";
        var response = await _serviceRepository.PostAsync<Authentication, Authentication>(Guid.Empty.ToString(), url, authenticationRequest);
        return response;
    }

    public async Task<List<Lead>> GetLeads(string sessionToken, string groupName)
    {
        groupName = groupName.Replace(' ', '_');
        return await _serviceRepository.GetAsync<List<Lead>>(sessionToken, $"{Constants.API_URL_AdminLeads}?groupName={groupName}");
    }

    public async Task<bool> RetrieveNumbers4mImage(MultipartFormDataContent formData)
    {
        return await _serviceRepository.UploadProfilePhoto(formData, Constants.API_URL_AdminUploadOCRFile);
    }

    public async Task<List<Master>> GetWhatsappGroups(string sessiontoken)
    {
        return await _serviceRepository.GetAsync<List<Master>>("", Constants.API_URL_AdminWhatsappGroupNames);
    }

    public async Task<bool> UpdateLeadCall(Lead lead)
    {
        return await _serviceRepository.PutAsync<Lead, bool>("", Constants.API_URL_AdminLeads, lead);
    }

    //private void Completed(object sender, AsyncCompletedEventArgs e)
    //{
    //    if (e.Error != null)
    //    {
    //        if (OnFileDownloaded != null)
    //            OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
    //    }
    //    else
    //    {
    //        if (OnFileDownloaded != null)
    //            OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));
    //    }
    //}

    public async Task<bool> GetPDF(string sessionToken, SearchParameters searchParameters)
    {
        var requestStorageWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();

        if (requestStorageWrite == PermissionStatus.Granted)
        {
            var gender = "male";
            var religion = "christian";
            var caste = "mala";
            //var folder = "downloadsMatri";

            //string pathToNewFolder = "";
            //System.Environment.SpecialFolder.Personal
            //Android.OS.Environment.ExternalStorageDirectory.AbsolutePath
            var url = $"{Constants.API_URL_ImagesRepo}{Constants.API_URL_AdminPdf}?gender={gender}&religion={religion}&caste={caste}";
//#if ANDROID
//         pathToNewFolder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
//#endif
            //Directory.CreateDirectory(pathToNewFolder);

            //try
            //{
            //    WebClient webClient = new WebClient();
            //    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            //    string pathToNewFile = Path.Combine(pathToNewFolder, "test.pdf");//, Path.GetFileName(url));
            //    webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            //}
            //catch (Exception ex)
            //{
            //    if (OnFileDownloaded != null)
            //        OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            //}

            //string fileName = FileSystem.Current.AppDataDirectory + "Temp.pdf";

            //return await _serviceRepository.GetAsync<bool>(sessionToken, );

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    //If we get the success status code, we can download it to the app data directory.
                    var stream = await response.Content.ReadAsStreamAsync();

#if ANDROID
                var resolver = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.ContentResolver;
                var contentValues = new Android.Content.ContentValues();
                contentValues.Put(Android.Provider.MediaStore.IMediaColumns.DisplayName, "Temp.pdf");
                contentValues.Put(Android.Provider.MediaStore.IMediaColumns.MimeType, "application/pdf");
                contentValues.Put(Android.Provider.MediaStore.IMediaColumns.RelativePath, Android.OS.Environment.DirectoryDownloads);
                Android.Net.Uri uri = resolver.Insert(Android.Provider.MediaStore.Downloads.ExternalContentUri, contentValues);
                Stream outputStream = resolver.OpenOutputStream(uri);
                await stream.CopyToAsync(outputStream);
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "PDF file has been downloaded", "Ok");
#endif
                }
                else
                {
                    throw new Exception("File not found");
                }
            }
        }
        else
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please allow app to access Storage", "OK");
            AppInfo.ShowSettingsUI();
        }
        return true;
    }
}

//public class DownloadEventArgs : EventArgs
//{
//    public bool FileSaved = false;
//    public DownloadEventArgs(bool fileSaved)
//    {
//        FileSaved = fileSaved;
//    }
//}
