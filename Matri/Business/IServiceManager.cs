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

        Task<Paging<MiniProfile>> GetProfiles(Guid sessiontoken, int page = 1, int perpage = 5, string sortby = "Id",
            string order = "Desc", string filterby = "All", bool newProfiles = false);
        Task<bool> LogoutAsync(Guid sessiontoken);

        Task<Profile> GetProfileById(Guid sessiontoken, Guid profileId);

        Task<Guid> ConvertNumberToGuid(Guid guid, string profileId);
        Task<Mini> GetShortDetailsOfLoggedInUser(Guid sessiontoken);

        Task<AffiliateContactDetails> GetAffiliateContactDetails(string affiliatePortal);

        Task<bool> ChangePasswordAsync(Guid guid, Password password);

        Task<bool> CreateProfileVisitor(Guid sessiontoken, Guid targetProfile);

        Task<Paging<MiniProfile>> GetMarkedProfiles(Guid sessiontoken, string markedAs, int page = 1, int perpage = 5);

        Task<Paging<Visitor>> GetProfileVisitors(Guid sessiontoken, int startPage, int pageSize);

        Task<bool> MarkProfile(Guid sessiontoken, Request request);

        Task<Paging<RequestSent>> GetRequestSent(Guid sessiontoken, int page = 1, int perpage = 5);

        Task<Paging<RequestReceived>> GetRequestReceived(Guid sessiontoken, int page = 1, int perpage = 5);

        Task<bool> ContactUs(Guid sessiontoken, ContactUs contactUs);

        Task<MDD> GetMasterData(Guid sessiontoken);

        Task<Paging<MiniProfile>> Search(Guid sessiontoken, SearchParameters searchParameters);

        Task<Model.App> GetAppDetails(Guid sessiontoken);

        Task<Profile> GetUserData(Guid sessiontoken);

        Task<bool> UpdateBasicDetails(Guid sessiontoken, Profile profile);
        Task<bool> UpdateEducationDetails(Guid sessiontoken, Profile profile);
        Task<bool> UpdateContactDetails(Guid sessiontoken, Profile profile);
        Task<bool> UpdateFamilyDetails(Guid sessiontoken, Profile profile);
        Task<bool> UpdateLifeStyleDetails(Guid sessiontoken, Profile profile);
        Task<bool> UpdatePhysicalDetails(Guid sessiontoken, Profile profile);
        Task<bool> UpdateReligionDetails(Guid sessiontoken, Profile profile);

        Task<bool> UploadProfilePhoto(MultipartFormDataContent formData);

        Task<bool> UpdateExpectations(Guid sessiontoken, Partner partner);
        Task<bool> CreateUpdateDeviceToken(Guid sessiontoken, FCMToken fcmToken);
    }
} 
