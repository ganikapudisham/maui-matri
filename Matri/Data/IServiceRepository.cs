﻿using Matri.Model;
using Matri.Model.Email;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Matri.Data
{
    public interface IServiceRepository
    {
        Task<bool> LogOut(Guid sessiontoken);

        Task<bool> CreateProfileVisitor(Guid sessiontoken, Guid targetProfile);

        Task<bool> UploadProfilePhoto(MultipartFormDataContent formData);

        Task<TResult> GetAsync<TResult>(string token, string url);
        Task<TOut> PostAsync<TIn, TOut>(string token, string url, TIn content);
        Task<TOut> PutAsync<TIn, TOut>(string token, string url, TIn content);
    }
}
