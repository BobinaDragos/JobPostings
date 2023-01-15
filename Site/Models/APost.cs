using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models
{
    
    public class APost
    {
        
        public int Id { get; set; }
        
        public string PostTitle { get; set; }
        
        public string PostDescription { get; set; }
        
        public string PostTags { get; set; }

        public APost()
        {

        }
    }
}
