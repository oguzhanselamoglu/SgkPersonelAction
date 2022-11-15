using SgkIseGirisServiceReference;
using SgkPersonelActionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SgkPersonelActionApp.Services
{
    public class SgkPersonelService : ISgkPersonelService
    {
        private readonly SgkIseGirisServiceReference.WS_SgkIseGirisClient _client;

        public SgkPersonelService(string url)
        {


            _client = new SgkIseGirisServiceReference.WS_SgkIseGirisClient();
          //  _client.Endpoint.Address = new EndpointAddress(url);

        }

        public async Task<ActionResponse<SgkResult>> CheckPersonel(SgkParameter parameter, long tcKimlikNo)
        {
            var response = ActionResponse<SgkResult>.Success(200);


            tcKimliktenIseGirisSorParametre param = new tcKimliktenIseGirisSorParametre
            {
                kullaniciBilgileri = new kullaniciBilgileri
                {
                    isyeriKodu = parameter.IsyeriKodu,
                    isyeriSicil = parameter.IsyeriSicil,
                    isyeriSifre = parameter.IsyeriSifre,
                    kullaniciAdi = parameter.KullaniciAdi,
                    sistemSifre = parameter.SistemSifre
                }
            };
            param.tcKimlikNo = tcKimlikNo;

            var sorguSonuc = await _client.tckimlikNoileiseGirisSorgulaAsync(param);
            if (sorguSonuc.tckimlikNoileiseGirisSorgulaReturn.hatakodu == 0)
            {
                var first = sorguSonuc.tckimlikNoileiseGirisSorgulaReturn.iseGirisKayitlari.FirstOrDefault();

                response.Data = new SgkResult
                {
                    TckimlikNo = first.tckimlikNo,
                    SicilNo = first.sicilno,
                    GirisTarihi = first.girisTarihi,
                    SigortaTuru = first.sigortaTuru
                };
            }
            else
            {

                response.ResponseType = ResponseType.Error;
                response.Message = sorguSonuc.tckimlikNoileiseGirisSorgulaReturn.hataAciklama;
            }
            return response;
        }

        public async Task<ActionResponse<List<SgkResult>>> SendAsync(SgkParameter parameter, List<SgkPersonel> personels)
        {
            var response = ActionResponse<List<SgkResult>>.Success(200);

            sgk4AIseGirisParametre param = new sgk4AIseGirisParametre
            {
                kullaniciBilgileri = new kullaniciBilgileri
                {
                    isyeriKodu = parameter.IsyeriKodu,
                    isyeriSicil = parameter.IsyeriSicil,
                    isyeriSifre = parameter.IsyeriSifre,
                    kullaniciAdi = parameter.KullaniciAdi,
                    sistemSifre = parameter.SistemSifre
                }
            };

            param.sigortaliIseGirisListesi = personels.Select(x => new sigortaliIseGiris
            {
                ad = x.Ad,
                soyad = x.Soyad,
                giristarihi = x.GirisTarihi,
                tckimlikNo = x.TcKimlikNo,
                csgbiskolu = x.CsgbisKolu,
                eskihukumlu = x.EskiHukumlu,
                gorevkodu = x.GorevKodu,
                kismiSureliCalisiyormu = x.KismiSureliCalisiyormu,
                kismiSureliCalismaGunSayisi = x.KismiSureliCalismaGunSayisi,
                meslekkodu = x.MeslekKodu,
                mezuniyetbolumu = x.MezuniyetBolumu,
                mezuniyetyili = x.MezuniyetYili,
                ogrenimkodu = x.OgrenimKodu,
                ozurlu = x.Ozurlu,
                sigortaliTuru = x.SigortaliTuru,

            }).ToArray();
            response.Data = new();
            var iseGrisSonuc = await _client.iseGirisKaydetAsync(param);
            if (iseGrisSonuc.iseGirisKaydetReturn.hataKodu == 0)
            {
                iseGrisSonuc.iseGirisKaydetReturn.sigortaliIseGirisSonuc.ToList().ForEach(x =>
                {
                    response.Data.Add(new SgkResult
                    {
                        GirisTarihi = x.iseGirisTarihi,
                        SicilNo = x.sicilno,
                        TckimlikNo = x.tcKimlikNo,
                        IslemSonuc = x.islemSonucu
                    });

                    if (x.islemSonucu == -1)
                    {
                        response.ResponseType = ResponseType.Error;
                        response.Message = x.islemAciklamasi;
                    }
                });
            }
            else
            {
                response.ResponseType = ResponseType.Error;
                response.Message = iseGrisSonuc.iseGirisKaydetReturn.hataAciklamasi;
            }

            return response;
        }

    }
}
