using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATest.AOL.RESTAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new Nrtestapi())
            {
                var articles = client.Article.GetArticles();
            }
        }
    }
}
