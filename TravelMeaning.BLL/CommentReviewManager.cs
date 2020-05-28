using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;

namespace TravelMeaning.BLL
{
    public class CommentReviewManager : ICommentReviewManager
    {
        protected readonly ICommentReviewService _commentReviewSvc;

        public CommentReviewManager(ICommentReviewService commentReviewSvc)
        {
            _commentReviewSvc = commentReviewSvc ?? throw new ArgumentNullException(nameof(commentReviewSvc));
        }
    }
}
