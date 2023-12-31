﻿
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace RisePhoneApp.Shared.Models.ResponseModels
{
    public class CustomResponse<T>
    {
        public T Data { get; private set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; private set; }

        public CustomResponse(T data, int statusCode, bool isSuccessful)
        {
            Data = data;
            StatusCode = statusCode;
            IsSuccessful = isSuccessful;
        }

        public CustomResponse(int statusCode, bool isSuccessful, List<string> errors)
          : this(default(T), statusCode, isSuccessful)
        {
            Errors = errors;
        }

        public CustomResponse(int statusCode, bool isSuccessful, string error)
          : this(default(T), statusCode, isSuccessful)
        {
            Errors = new List<string> { error };
        }

        internal IActionResult CreateResponse()
        {
            return new ObjectResult(this)
            {
                StatusCode = StatusCode
            };
        }

        public static IActionResult Success(T data, int statusCode)
        {
            return new CustomResponse<T>(data, statusCode, true).CreateResponse();
        }

        public static IActionResult Success(int statusCode)
        {
            return new CustomResponse<T>(default(T), statusCode, true).CreateResponse();
        }

        public static IActionResult Fail(List<string> errors, int statusCode)
        {
            return new CustomResponse<T>(statusCode, false, errors).CreateResponse();
        }

        public static IActionResult Fail(string error, int statusCode)
        {
            return new CustomResponse<T>(statusCode, false, error).CreateResponse();
        }


    }
}
