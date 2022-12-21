using SgkPersonelActionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgkPersonelActionApp.Services
{
    public interface ISgkPersonelService
    {
        Task<ActionResponse<List<SgkResult>>> SendAsync(SgkParameter parameter,List<SgkPersonel> personels);
        Task<ActionResponse<SgkResult>> CheckPersonel(SgkParameter parameter, long tcKimlikNo);
        Task<ActionResponse<Stream>> GetPersonelPdf(SgkParameter parameter, long referanceCode);
    }
}
