using Matri.CustomExceptions;
using Matri.Data.Services;
using Matri.Model;
using Matri.Model.Email;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Data.Impl
{
    public class ServiceRepository : ServiceBase, IServiceRepository
    {
        HttpClient client = new HttpClient();

        public async Task<bool> LogOut(Guid sessiontoken)
        {
            var client = CreateHttpClient(sessiontoken);

            HttpResponseMessage httpResponse = await client.DeleteAsync("sessiontoken");

            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CreateProfileVisitor(Guid sessiontoken, Guid targetProfile)
        {
            var client = CreateHttpClient(sessiontoken);

            HttpResponseMessage httpResponse = await client.PostAsync($"visitors/{targetProfile}", null);

            if (httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.Created)
                    return true;
            }
            else
            {
                var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception($"{jsonResponse}");
            }
            return false;
        }

        public async Task<bool> UploadProfilePhoto(MultipartFormDataContent formData)
        {
            var client = CreateHttpClientForPhotoUpload();
            HttpResponseMessage httpResponse = await client.PostAsync("files/photo", formData);

            if (httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    return true;
            }
            else
            {
                var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception($"{jsonResponse}");
            }
            return false;
        }

        public async Task<TOut> PostAsync<TIn, TOut>(string sessionToken, string uri, TIn content)
        {
            try
            {
                var httpClient = CreateHttpClient(new Guid(sessionToken));

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
                            throw new Exception("Something went wrong, please try again");
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
                throw ex;
            }
        }

        public async Task<bool> UploadImage(string sessionToken, string uri, MultipartFormDataContent formData)
        {
            try
            {
                var httpClient = CreateHttpClientForPhotoUpload();
                httpClient.DefaultRequestHeaders.Clear();
                using (HttpResponseMessage response = await httpClient.PostAsync(uri, formData))
                {
                    response.EnsureSuccessStatusCode();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<TResult> GetAsync<TResult>(string sessionToken, string url)
        {
            TResult objectToReturn = default(TResult);
            HttpResponseMessage response = null;

            try
            {
                var httpClient = CreateHttpClient(new Guid(sessionToken));
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
                //HandleHttpRequestException(response, ex, url);
            }
            catch (Exception ex)
            {
                //logWriter.WriteError(error);
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
                return false;
            }
        }

        public async Task<TOut> PutAsync<TIn, TOut>(string sessionToken, string uri, TIn content)
        {
            try
            {
                var httpClient = CreateHttpClient(new Guid(sessionToken));

                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PutAsync(uri, serialized))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
