
namespace UpSchool.HelpDesk.BusinessLayer.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public Result()
        {

        }
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        /// <summary>
        ///  success with data
        /// </summary>
        public Result(T data)
        {
            Data = data;
            IsSuccess= true;
        }

        /// <summary>
        ///  success with mesage and data
        /// </summary>
        public Result(T data, string message)
        {
            IsSuccess= true;
            Data = data;
            Message = message;
        }

        /// <summary>
        /// Fail with message
        /// </summary>
        public Result(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }
}
