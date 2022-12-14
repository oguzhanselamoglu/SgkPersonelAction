using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgkPersonelActionApp.Models
{
    public class SgkPersonel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string GirisTarihi { get; set; }
        public short SigortaliTuru { get; set; }
        public string GorevKodu { get; set; }
        public string MeslekKodu { get; set; }
        public string CsgbisKolu { get; set; }
        public string EskiHukumlu { get; set; }
        public string Ozurlu { get; set; }
        public string OgrenimKodu { get; set; }
        public string MezuniyetBolumu { get; set; }
        public short MezuniyetYili { get; set; }
        public string KismiSureliCalisiyormu { get; set; }
        public short KismiSureliCalismaGunSayisi { get; set; }
        public long TcKimlikNo { get; internal set; }
        public SgkPeriod BulunduguDonem { get; set; }
        public SgkPeriod OncekiDonem { get; set; }
        public string NakilGidecegiIsyeriSicil { get; set; }

    }
    public class SgkPeriod
    {
        public short Belgeturu { get; set; }
        public double HakedilenUcret { get; set; }
        public double PrimIkramiye { get; set; }
        public short EksikGunsayisi { get; set; }
        public short EksikgunNedeni { get; set; }

    }
}
