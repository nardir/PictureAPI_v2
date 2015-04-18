using System;
using System.Net.Http;
using Microsoft.Azure.AppService;

namespace CATestContactList
{
    public static class ContactListAppServiceExtensions
    {
        public static ContactList CreateContactList(this IAppServiceClient client)
        {
            return new ContactList(client.CreateHandler());
        }

        public static ContactList CreateContactList(this IAppServiceClient client, params DelegatingHandler[] handlers)
        {
            return new ContactList(client.CreateHandler(handlers));
        }

        public static ContactList CreateContactList(this IAppServiceClient client, Uri uri, params DelegatingHandler[] handlers)
        {
            return new ContactList(uri, client.CreateHandler(handlers));
        }

        public static ContactList CreateContactList(this IAppServiceClient client, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
        {
            return new ContactList(rootHandler, client.CreateHandler(handlers));
        }
    }
}
