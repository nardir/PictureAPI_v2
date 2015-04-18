using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axerrio.Data.Entity;
using AOL.Model;
using AOL.Repository;
using AOL;

namespace CADataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTest();
        }

        static void DataTest()
        {
            try
            {
                DataConfiguration.Configure(AOLDataConfig.Configure);

                using (IAOLRepository repo = new AOLRepository())
                {
                    var articles = repo.GetArticlesAsync().Result;

                    var article = repo.GetArticleAsync("A0060905").Result;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}