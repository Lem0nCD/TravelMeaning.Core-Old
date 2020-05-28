using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.IDAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

namespace TravelMeaning.DAL
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        public CommentService(TMContext db) : base(db)
        {
        }
    }
}
