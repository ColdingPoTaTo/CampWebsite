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
        public tOrder tOrder { get; set; }
    }
}