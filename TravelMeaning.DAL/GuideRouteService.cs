using TravelMeaning.IDAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

namespace TravelMeaning.DAL
{
    public class GuideRouteService : BaseService<GuideRoute>, IGuideRouteService
    {
        public GuideRouteService(TMContext db) : base(db)
        {
        }
    }
}
