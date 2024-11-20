namespace AdminMatri.Data
{
    public interface IServiceRepository
    {
        Task<TResult> GetAsync<TResult>(string token, string url);
        Task<TOut> PostAsync<TIn, TOut>(string token, string url, TIn content);
        Task<TOut> PutAsync<TIn, TOut>(string token, string url, TIn content);
    }
}
