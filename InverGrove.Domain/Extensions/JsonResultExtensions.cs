using System;
using System.Web.Mvc;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Helpers;
using JetBrains.Annotations;

namespace InverGrove.Domain.Extensions
{
    public static class JsonResultExtensions
    {
        /// <summary>
        /// Ases the camel case resolver result.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static JsonResult AsCamelCaseResolverResult(this JsonResult json)
        {
            if (json == null)
            {
                throw new ParameterNullException("json");
            }

            return new JsonCamelCaseResolverResult
            {
                Data = json.Data,
                ContentType = json.ContentType,
                ContentEncoding = json.ContentEncoding,
                JsonRequestBehavior = json.JsonRequestBehavior
            };
        }
    }
}