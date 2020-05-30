using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;
using TravelMeaning.Models.Model;

namespace TravelMeaning.BLL
{
    public class RelationShipManager : IRelationShipManager
    {
        protected IRelationShipService _relationShipSvc;
        protected IUserService _userService;
        protected IMapper mapper;

        public RelationShipManager(IRelationShipService relationShipSvc, IUserService userService, IMapper mapper)
        {
            _relationShipSvc = relationShipSvc ?? throw new ArgumentNullException(nameof(relationShipSvc));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private async Task<RelationShip> FindOrBuildRelationships(Guid fromUserId, Guid toUserId)
        {
            var relationship = await _relationShipSvc.GetAll().Where(x => x.FromUserId == fromUserId && x.ToUserId == toUserId).FirstOrDefaultAsync();
            if (relationship == null)
            {
                relationship = new RelationShip
                {
                    FromUserId = fromUserId,
                    ToUserId = toUserId,
                };
                if (!await _relationShipSvc.CreateAsync(relationship))
                {
                    throw new Exception("BuildRelationships Fail!");
                }
            }
            return relationship;
        }

        public async Task<Guid> ChangeType(Guid fromUserId, Guid toUserId, RelationshipType type)
        {
            var relationship = await FindOrBuildRelationships(fromUserId, toUserId);
            relationship.Type = type;
            if (!await _relationShipSvc.EditAsync(relationship))
            {
                throw new Exception("FollowerUser Fail!");
            }
            return relationship.Id;
        }

        public async Task<RelationshipType> GetRelationshipType(Guid fromUserId, Guid toUserId)
        {
            RelationshipType type = RelationshipType.Detachment;
            var relationship = await _relationShipSvc.GetAll().Where(x => x.FromUserId == fromUserId && x.ToUserId == toUserId).FirstOrDefaultAsync();
            if (relationship != null)
            {
                type = relationship.Type;
            }
            return type;
        }
    }
}
