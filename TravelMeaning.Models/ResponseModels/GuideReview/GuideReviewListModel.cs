using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.Models.DTO;

namespace TravelMeaning.Models.ResponseModels.GuideReview
{
    public class GuideReviewListModel
    {
        public int Count { get; set; }
        public List<GuideReviewDTO> Guides { get; set; }
    }
}
