using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.IDAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

namespace TravelMeaning.DAL
{
    public class RelationShipService : BaseService<RelationShip>, IRelationShipService
    {
        public RelationShipService(TMContext db) : base(db)
        {
        }
    }
}
