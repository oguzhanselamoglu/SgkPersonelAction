using SgkIstenCikisServiceReference;
using SgkPersonelActionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgkPersonelActionApp.Services
{
    internal class SgkPersonelLeaveService : ISgkPersonelService
    {
        private readonly SgkIstenCikisServiceReference.WS_SgkIstenCikisClient _client;
        public SgkPersonelLeaveService()
        {
            _client = new WS_SgkIstenCikisClient();
        }
        public Task<ActionResponse<SgkResult>> CheckPersonel(SgkParameter parameter, long tcKimlikNo)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResponse<List<SgkResult>>> SendAsync(SgkParameter parameter, List<SgkPersonel> personels)
        {
            var response = ActionResponse<List<SgkResult>>.Success(200);
            sgk4AIstenCikisParametre param = new sgk4AIstenCikisParametre
            {
                kullaniciBilgileri = new SgkIstenCikisServiceReference.kullaniciBilgileri
                {
                    isyeriKodu = parameter.IsyeriKodu,
                    isyeriSicil = parameter.IsyeriSicil,
                    isyeriSifre = parameter.IsyeriSifre,
                    kullaniciAdi = parameter.KullaniciAdi,
                    sistemSifre = parameter.SistemSifre
                }
            };
            param.sigortaliIstenCikisListesi = personels.Select(x => new sigortaliIstenCikis
            {
                ad = x.Ad,
                soyad = x.Soyad,
                tckimlikNo = x.TcKimlikNo,
                csgbiskolu = x.CsgbisKolu,           
                meslekkodu = x.MeslekKodu,
                bulundugumuzDonem =new bulundugumuzDonem
                {
                    belgeturu=x.BulunduguDonem.Belgeturu,
                    eksikgunnedeni=x.BulunduguDonem.EksikgunNedeni,
                    eksikgunsayisi=x.BulunduguDonem.EksikGunsayisi,
                    hakedilenucret = x.BulunduguDonem.HakedilenUcret,
                    primikramiye=x.BulunduguDonem.PrimIkramiye,

                },
                oncekiDonem = new oncekiDonem
                {
                    belgeturu = x.OncekiDonem.Belgeturu,
                    eksikgunnedeni = x.OncekiDonem.EksikgunNedeni,
                    eksikgunsayisi = x.OncekiDonem.EksikGunsayisi,
                    hakedilenucret = x.OncekiDonem.HakedilenUcret,
                    primikramiye = x.OncekiDonem.PrimIkramiye,
                }

            }).ToArray();
            response.Data = new();
            var iseGrisSonuc = await _client.istenCikisKaydetAsync(param);
            if (iseGrisSonuc.istenCikisKaydetReturn.hataKodu == 0)
            {
                iseGrisSonuc.istenCikisKaydetReturn.sigortaliIstenCikisSonuc.ToList().ForEach(x =>
                {
                    response.Data.Add(new SgkResult
                    {
                        GirisTarihi = x.istenCikisTarihi,
                       // SicilNo = x.sicilno,
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
                response.Message = iseGrisSonuc.istenCikisKaydetReturn.hataAciklamasi;
            }
            return response;
        }
    }
}
