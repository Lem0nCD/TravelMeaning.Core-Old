using TravelMeaning.IDAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

namespace TravelMeaning.DAL
{
    public class CommentReviewService : BaseService<CommentReview>, ICommentReviewService
    {
        public CommentReviewService(TMContext db) : base(db)
        {
        }
    }
}
