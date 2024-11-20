using Matri.CustomExceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Matri.Data.Services
{
    public class ServiceBase
    {
        const int MAX_RETRY = 3;
        //private readonly Uri _apiEndpoint = new Uri(""http://10.0.2.2:90/"); //android emulator
        //private readonly Uri _apiEndpoint = new Uri(""http://192.168.0.185:90/"); //real device connected via usb
        private readonly Uri _apiEndpoint = new Uri("https://api.christianjodi.com/");

        //private readonly Uri _apiEndpoint = DeviceInfo.Platform == DevicePlatform.Android ? new Uri("http://lapi.Matri.com/") : new Uri("http://lapi.Matri.com/");


        public string GetQueryString(object obj)
        {
            if (obj == null)
                return string.Empty;

            var result = new List<string>();
            var props = obj.GetType().GetRuntimeProperties().Where(p => p.GetValue(obj, null) != null);
            foreach (var p in props)
            {
                var value = p.GetValue(obj, null);
                var enumerable = value as IEnumerable;
                if (enumerable != null)
                {
                    result.AddRange(from object v in enumerable
                                    select
$"{p.Name}={(p.PropertyType == typeof(DateTime) ? WebUtility.UrlEncode(((DateTime)v).ToString("o")) : WebUtility.UrlEncode(v.ToString()))}");
                }
                else
                {
                    result.Add(
                        $"{p.Name}={(p.PropertyType == typeof(DateTime) ? WebUtility.UrlEncode(((DateTime)value).ToString("o")) : WebUtility.UrlEncode(value.ToString()))}");
                }
            }

            return string.Join("&", result.ToArray());
        }

        protected HttpClient CreateHttpClient(string token)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    var matriInternetException = new MatriInternetException();
            //    matriInternetException.Message = "Please Check Internet Connection";
            //    throw matriInternetException;
            //}

            //var accessType = Connectivity.Current.NetworkAccess;

            //if (accessType != NetworkAccess.Internet)
            //{
            //    var matriInternetException = new MatriInternetException();
            //    matriInternetException.Message = "Please Check Internet Connection";
            //    throw matriInternetException;
            //}

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

        protected HttpClient CreateHttpClientForPhotoUpload()
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    var matriInternetException = new ChristianJodiInternetException();
            //    matriInternetException.Message = "Please Check Internet Connection";
            //    throw matriInternetException;
            //}

            var client = new HttpClient() { BaseAddress = _apiEndpoint };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            return client;
        }

        /// <summary>
        /// If your request support paging and has 
        /// Page and PageSize property this ensures 
        /// valid values are provided and if missing
        /// default values are added.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="requestToValidate"></param>
        /// <returns></returns>
        protected TEntity ValidatePageAndPageSize<TEntity>(TEntity requestToValidate)
        {
            var entityType = typeof(TEntity);
            var pageProperty = entityType.GetRuntimeProperty("Page");
            var pageSizeProperty = entityType.GetRuntimeProperty("PageSize");

            var pageValue = (int?)pageProperty?.GetValue(requestToValidate);
            var pageSizeValue = (int?)pageSizeProperty?.GetValue(requestToValidate);

            if (pageValue == 0)
                entityType.GetRuntimeProperty("Page").SetValue(requestToValidate, 1);

            if (pageSizeValue == 0)
                entityType.GetRuntimeProperty("PageSize").SetValue(requestToValidate, 10);

            return requestToValidate;

        }

    }
}
