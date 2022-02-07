using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectedADOWithStoredProcedures.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
