using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.ResponseModels
{
    public class GenericModel
    {
        public string Message { get; set; }
        public bool IsSucess { get; set; } = false;
    }
}
