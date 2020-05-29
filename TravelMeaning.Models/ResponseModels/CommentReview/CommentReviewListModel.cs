using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.Models.DTO;

namespace TravelMeaning.Models.ResponseModels.CommentReview
{
    public class CommentReviewListModel
    {
        public int Count { get; set; }
        public List<CommentReviewDTO> Comments { get; set; }
    }
}
