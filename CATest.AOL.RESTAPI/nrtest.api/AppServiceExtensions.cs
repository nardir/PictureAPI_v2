using System;
using System.Net.Http;
using Microsoft.Azure.AppService;

namespace 
{
    public static class AppServiceExtensions
    {
        public static  Create(this IAppServiceClient client)
        {
            return new (client.CreateHandler());
        }

        public static  Create(this IAppServiceClient client, params DelegatingHandler[] handlers)
        {
            return new (client.CreateHandler(handlers));
        }

        public static  Create(this IAppServiceClient client, Uri uri, params DelegatingHandler[] handlers)
        {
            return new (uri, client.CreateHandler(handlers));
        }

        public static  Create(this IAppServiceClient client, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
        {
            return new (rootHandler, client.CreateHandler(handlers));
        }
    }
}
