namespace AdminMatri.Business
{
    public interface IServiceManager
    {
        Task<bool> LoginAsync(string username, string password);
    }
}
