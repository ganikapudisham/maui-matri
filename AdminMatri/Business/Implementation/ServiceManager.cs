using AdminMatri.Data;

namespace AdminMatri.Business.Impl
{
    public class ServiceManager : IServiceManager
    {
        IServiceRepository _serviceRepository;
        public ServiceManager(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException($"Pin not provided");

            if (string.IsNullOrEmpty(password)) throw new ArgumentException($"Password not provided");

            var authenticationRequest = new Authentication { UserName = username, Password = password };
            var url = Constants.API_URL_Authentication; //"sessiontoken";
            var response = await _serviceRepository.PostAsync<Authentication, Authentication>(Guid.Empty.ToString(), url, authenticationRequest);
            return response;
        }
    }
}
