﻿namespace Store.C42.APIs.Errors
{
    public class ApiExceptionResponse : ApiErrorResponse
    {
        public string? Details { get; set; }

        public ApiExceptionResponse(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
