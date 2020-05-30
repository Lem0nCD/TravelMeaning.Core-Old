using AutoMapper;
using System;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;

namespace TravelMeaning.BLL
{
    public class GuideRouteManager : IGuideRouteManager
    {
        protected readonly IGuideRouteService _guideRouteSvc;
        protected readonly IMapper mapper;

        public GuideRouteManager(IGuideRouteService guideRouteSvc, IMapper mapper)
        {
            _guideRouteSvc = guideRouteSvc ?? throw new ArgumentNullException(nameof(guideRouteSvc));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
