using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class PageHelper
    {

        public static string GetPageUrl(HttpRequest request, out string absoluteUrl)
        {
            string url = null;
            absoluteUrl = GetAbsoluteUri(request);
            var queryString = request.QueryString.ToString();
            if (string.IsNullOrEmpty(queryString))
            {
                url = string.Concat(absoluteUrl + "?");
            }
            else
            {
                var paramIndex = absoluteUrl.IndexOf("pageSize", StringComparison.OrdinalIgnoreCase);
                if (paramIndex > 0)
                {
                    url = absoluteUrl.Substring(0, paramIndex);
                }
                else
                {
                    url = string.Concat(absoluteUrl + "&");
                }
            }
            return url;
        }

        public static int GetPageIndex(HttpRequest request)
        {
            int index = 1;
            var pageIndex = request.Query["PageIndex"];
            if (!string.IsNullOrWhiteSpace(pageIndex))
            {
                int.TryParse(pageIndex, out index);
            }
            if (index < 1)
            {
                index = 1;
            }
            return index;
        }

        public static int GetPageSize(HttpRequest request)
        {
            int size = 30;
            var pageSize = request.Query["pageSize"];
            if (!string.IsNullOrWhiteSpace(pageSize))
            {
                int.TryParse(pageSize, out size);
            }
            if (size < 0)
            {
                size = 30;
            }
            return size;
        }

        public static int GetPageSize(HttpRequest request, int defaultSize)
        {
            int size = defaultSize;
            var pageSize = request.Query["pageSize"];
            if (!string.IsNullOrWhiteSpace(pageSize))
            {
                int.TryParse(pageSize, out size);
            }
            if (size < 0)
            {
                size = defaultSize;
            }
            return size;
        }
        public static string GetAbsoluteUri(HttpRequest _request)
        {
            return string.Concat(
                        _request.Scheme,
                        "://",
                        _request.Host.ToUriComponent(),
                        _request.PathBase.ToUriComponent(),
                        _request.Path.ToUriComponent(),
                        _request.QueryString.ToUriComponent());
        }
    }
}
