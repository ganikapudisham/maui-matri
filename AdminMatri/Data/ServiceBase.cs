namespace AdminMatri.Data.Services
{
    public class ServiceBase
    {
    }

    protected HttpClient CreateHttpClient(string token)
    {
        //if (!CrossConnectivity.Current.IsConnected)
        //{
        //    var matriInternetException = new MatriInternetException();
        //    matriInternetException.Message = "Please Check Internet Connection";
        //    throw matriInternetException;
        //}

        var accessType = Connectivity.Current.NetworkAccess;

        if (accessType != NetworkAccess.Internet)
        {
            var matriInternetException = new MatriInternetException();
            matriInternetException.Message = "Please Check Internet Connection";
            throw matriInternetException;
        }

        var client = new HttpClient() { BaseAddress = _apiEndpoint };
        client.DefaultRequestHeaders.Clear();

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Token {token}");
        }

        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        return client;
    }
}
