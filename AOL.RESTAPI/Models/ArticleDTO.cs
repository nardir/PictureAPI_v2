using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axerrio.Data.Entity;

namespace AOL.RESTAPI.Models
{
    public class ArticleDTO: StatefulEntity
    {
        public int ArticleKey { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string Code { get; set; }
    }
}