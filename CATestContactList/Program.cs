using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATestContactList
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new ContactList())
            {
                var contacts = client.Contacts.GetContacts();
            }
        }
    }
}
