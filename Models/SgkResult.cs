using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgkPersonelActionApp.Models
{
    public class SgkResult
    {
        public long TckimlikNo { get;  set; }
        public long SicilNo { get;  set; }
        public string GirisTarihi { get;  set; }
        public string SigortaTuru { get;  set; }
        public long IslemSonuc { get;  set; }
    }
}
