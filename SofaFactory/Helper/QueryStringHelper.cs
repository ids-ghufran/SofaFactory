using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace SofaFactory.Helper
{
    public static class QueryStringHelper
    {
        public static string UpdateQueryStringParameters(string queryString, IDictionary<string, string> parametersToUpdate)
        {
            // Convert query string to a dictionary
          
            IDictionary<string, string> queryParams = QueryHelpers.ParseQuery(queryString)
                .ToDictionary(kv => kv.Key, kv => kv.Value.ToString());

            // Update or add the specified parameters
            foreach (var kvp in parametersToUpdate)
            {
                if (queryParams.ContainsKey(kvp.Key))
                {
                    queryParams[kvp.Key] = kvp.Value;
                }
                else
                {
                    queryParams.Add(kvp.Key, kvp.Value);
                }
            }

            // Convert dictionary back to query string
            string newQueryString = QueryHelpers.AddQueryString("", queryParams);

            return newQueryString;
        }
    }
}
