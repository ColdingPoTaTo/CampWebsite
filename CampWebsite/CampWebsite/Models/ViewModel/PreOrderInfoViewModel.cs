using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampWebsite.Models.ViewModel
{
    public class PreOrderInfoViewModel
    {
        public tMember tMember { get; set; }
        public tTent tTent { get; set; }
        public System.DateTime fCheckinDate { get; set; }
        public string fPhone { get; set; }
        public string fOrderComment { get; set; }
        public int fOrderPrice { get; set; }


    }
}