using Matri.Model;
using Matri.Model.Email;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Matri.Business
{
    public interface IServiceManager
    {
        Task<Authentication> LoginAsync(string username, string password);
        Task<bool> ForgetPasswordAsync(string username);
        Task<RegisterUser> RegisterUserAsync(string FirstName, string LastName, string UserName, string Password,
            string ConfirmPassword, string Gender, DateTime BirthDate, string website);

        Task<Paging<MiniProfile>> GetProfiles(string sessiontoken, int page = 1, int perpage = 5, string sortby = "Id",
            string order = "Desc", string filterby = "All", bool newProfiles = false);
        Task<bool> LogoutAsync(string sessiontoken);

        Task<Profile> GetProfileById(string sessiontoken, Guid profileId);

        Task<Guid> ConvertNumberToGuid(string guid, string profileId);
        Task<Mini> GetShortDetailsOfLoggedInUser(string sessiontoken);

        Task<AffiliateContactDetails> GetAffiliateContactDetails(string affiliatePortal);

        Task<bool> ChangePasswordAsync(string guid, Password password);

        Task<bool> CreateProfileVisitor(string sessiontoken, Guid targetProfile);

        Task<Paging<MiniProfile>> GetMarkedProfiles(string sessiontoken, string markedAs, int page = 1, int perpage = 5);

        Task<Paging<Visitor>> GetProfileVisitors(string sessiontoken, int startPage, int pageSize);

        Task<bool> MarkProfile(string sessiontoken, Request request);

        Task<Paging<RequestSent>> GetRequestSent(string sessiontoken, int page = 1, int perpage = 5);

        Task<Paging<RequestReceived>> GetRequestReceived(string sessiontoken, int page = 1, int perpage = 5);

        Task<bool> ContactUs(string sessiontoken, ContactUs contactUs);

        Task<MDD> GetMasterData(string sessiontoken);

        Task<Paging<MiniProfile>> Search(string sessiontoken, SearchParameters searchParameters);

        Task<Model.App> GetAppDetails(string sessiontoken);

        Task<Profile> GetUserData(string sessiontoken);

        Task<bool> UpdateBasicDetails(string sessiontoken, Profile profile);
        Task<bool> UpdateEducationDetails(string sessiontoken, Profile profile);
        Task<bool> UpdateContactDetails(string sessiontoken, Profile profile);
        Task<bool> UpdateFamilyDetails(string sessiontoken, Profile profile);
        Task<bool> UpdateLifeStyleDetails(string sessiontoken, Profile profile);
        Task<bool> UpdatePhysicalDetails(string sessiontoken, Profile profile);
        Task<bool> UpdateReligionDetails(string sessiontoken, Profile profile);

        Task<bool> UploadProfilePhoto(MultipartFormDataContent formData);

        Task<bool> UpdateExpectations(string sessiontoken, Partner partner);
        Task<bool> CreateUpdateDeviceToken(string sessiontoken, FCMToken fcmToken);

        Task<List<FCMToken>> GetUserDeviceTokens(string sessiontoken, string notificationRecipient);

        Task<Profile> GetProfileByIdWithoutAuth(Guid profileId);

        Task<List<RequestSent>> GetAllRequests(string sessiontoken);

        Task<Authentication> AdminLoginAsync(string username, string password);

        Task<List<string>> GetLeads(string sessionToken);

        Task<bool> RetrieveNumbers4mImage(MultipartFormDataContent formData);

        Task<List<Master>> GetWhatsappGroups(string sessiontoken);

        Task<bool> UpdateLeadCall(string id_number_comment);
    }
}
