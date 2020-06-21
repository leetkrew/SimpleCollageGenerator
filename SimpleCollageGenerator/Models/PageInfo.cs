using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCollageGenerator.Models
{
    class PageInfo
    {
        public string Pic0 { get; set; }
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string Pic3 { get; set; }

        public PageInfo()
        {
            this.Pic0 = string.Empty;
            this.Pic1 = string.Empty;
            this.Pic2 = string.Empty;
            this.Pic3 = string.Empty;
        }

    }
}
