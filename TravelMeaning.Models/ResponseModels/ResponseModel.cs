namespace TravelMeaning.Models.ResponseModels
{
    public class ResponseModel<T> where T : class
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回内容
        /// </summary>
        public T Data { get; set; }
    }
}
