using System.Net;
using System.Text;
using AdminMatri.Data;
using AdminMatri.Data.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdminMatri.Data.Impl
{
    public class ServiceRepository : ServiceBase, IServiceRepository
    {
        public ServiceRepository()
        {
        }

        public async Task<TOut> PostAsync<TIn, TOut>(string sessionToken, string uri, TIn content)
        {

            try
            {
                var httpClient = CreateHttpClient(sessionToken);

                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PostAsync(uri, serialized))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var t = JsonConvert.DeserializeObject<MatriException>(responseBody);
                            throw new Exception(t?.Message);
                        }
                        else if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            //throw new Exception("Something went wrong, please try again");
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var t = JsonConvert.DeserializeObject<MatriException>(responseBody);
                            throw new Exception(t?.Message);
                        }
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<TOut>(responseBody);
                    }
                }
                return default(TOut);
            }
            catch (Exception ex)
            {
                _firebaseCrashlyticsService.Log(ex);
                throw ex;
            }
        }

        public async Task<TResult> GetAsync<TResult>(string sessionToken, string url)
        {
            TResult objectToReturn = default(TResult);
            HttpResponseMessage response = null;

            try
            {
                var httpClient = CreateHttpClient(sessionToken);
                response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var t = JsonConvert.DeserializeObject<MatriException>(responseBody);
                        throw new Exception(t?.Message);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Not found, please try again with different data");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        throw new Exception("Something went wrong, please try again");
                    }
                }
                else
                {
                    var responseJson = await response.Content.ReadAsStringAsync();

                    if (IsValidateJSON(responseJson))
                    {
                        objectToReturn = JsonConvert.DeserializeObject<TResult>(responseJson);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _firebaseCrashlyticsService.Log(ex);
            }
            catch (Exception ex)
            {
                _firebaseCrashlyticsService.Log(ex);
                throw ex;
            }
            return objectToReturn;
        }

        public bool IsValidateJSON(string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (JsonReaderException ex)
            {
                _firebaseCrashlyticsService.Log(ex);
                return false;
            }
        }

        public async Task<TOut> PutAsync<TIn, TOut>(string sessionToken, string uri, TIn content)
        {
            TOut objectToReturn = default(TOut);
            try
            {
                var httpClient = CreateHttpClient(sessionToken);

                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PutAsync(uri, serialized))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var t = JsonConvert.DeserializeObject<MatriException>(responseBody);
                            throw new Exception(t?.Message);
                        }
                        else if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            throw new Exception("Not found, please try again with different data");
                        }
                        else if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception("Something went wrong, please try again");
                        }
                    }
                    else
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();

                        if (IsValidateJSON(responseJson))
                        {
                            objectToReturn = JsonConvert.DeserializeObject<TOut>(responseJson);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _firebaseCrashlyticsService.Log(ex);
                throw ex;
            }
            return objectToReturn;
        }
    }
}
}
