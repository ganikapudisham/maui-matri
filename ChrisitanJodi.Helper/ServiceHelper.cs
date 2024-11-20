using Matri.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Helper
{
    public static class ServiceHelper
    {
        public static IServiceProvider Services { get; private set; }

        public static void Initialize(IServiceProvider serviceProvider) =>
            Services = serviceProvider;

        public static object GetService(Type t) 
        {
            return Services.GetService(t);
        }
        


        public static ProfileDetailsInput InitialiseRequestsSent(List<RequestSent> requestsSentToSelectedUser)
        {
            var profileDetailsInput = new ProfileDetailsInput();
            if (requestsSentToSelectedUser != null && requestsSentToSelectedUser.Count > 0)
            {
                var blocked = requestsSentToSelectedUser.Where(rsts => rsts.Type == RequestAction.Block.ToString()).ToList().Count > 0;
                profileDetailsInput.Blocked = blocked;

                var favourite = requestsSentToSelectedUser.Where(rsts => rsts.Type == RequestAction.Favourite.ToString()).ToList().Count > 0;
                profileDetailsInput.Favourite = favourite;

                var photoRequest = requestsSentToSelectedUser.Where(rsts => rsts.Type == RequestAction.RequestPhoto.ToString()).ToList().Count > 0;
                profileDetailsInput.ReceivedPhotoRequest = photoRequest;

                var interestSent = requestsSentToSelectedUser.Where(rsts => rsts.Type == RequestAction.SendInterest.ToString()).ToList().Count > 0;
                profileDetailsInput.ReceivedInterest = interestSent;
            }
            return profileDetailsInput;
        }
    }
}
