// See https://aka.ms/new-console-template for more information
using SgkIseGirisServiceReference;
using SgkPersonelActionApp.Models;
using SgkPersonelActionApp.Services;

Console.WriteLine("Hello, World!");

ISgkPersonelService service = new SgkPersonelService("https://sgkt.sgk.gov.tr/WS_SgkTescil4a/WS_SgkIseGirisService?wsdl");
var param = new SgkParameter
{
    KullaniciAdi = "",
    IsyeriKodu = "",
    SistemSifre = "",
    IsyeriSifre = "",
    IsyeriSicil = "",

};

//var response = await service.CheckPersonel(param, 11111111111);
//if (response.ResponseType ==ResponseType.Ok)
//{
//    Console.WriteLine(response.Data.SicilNo);
//}


List<SgkPersonel> personels = new List<SgkPersonel>
{
     new SgkPersonel
    {
        TcKimlikNo=11111111111,
         Ad="Oğuzhan",
         Soyad="Selamoğlu",
         GirisTarihi="15.11.2022",
         SigortaliTuru=0,
         GorevKodu="02",
         MeslekKodu="0000.00",
         CsgbisKolu="18",
         EskiHukumlu="H",
         Ozurlu="H",
         OgrenimKodu="5",
         MezuniyetBolumu="TEST",
         MezuniyetYili=2006,
         KismiSureliCalisiyormu="H",
         KismiSureliCalismaGunSayisi=0
    }
};
var response = await service.SendAsync(param, personels);
if (response.ResponseType == ResponseType.Ok)
{
    response.Data.ForEach(x =>
    {
        Console.WriteLine(x.TckimlikNo);
    });
}