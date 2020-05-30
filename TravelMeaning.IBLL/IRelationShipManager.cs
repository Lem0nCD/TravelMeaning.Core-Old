using System;
using System.Threading.Tasks;
using TravelMeaning.Models.Model;

namespace TravelMeaning.IBLL
{
    public interface IRelationShipManager
    {
        public Task<Guid> ChangeType(Guid fromUserId, Guid toUserId,RelationshipType type);
        public Task<RelationshipType> GetRelationshipType(Guid fromUserId, Guid toUserId);
    }
}
