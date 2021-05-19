namespace ColoredLive.Core.Responses
{
    public class BaseResponse
    {
        public bool IsError { get; }
        public int Status { get; }
        public string ErrorMessage { get; }
        public object Data { get; }

        private BaseResponse(bool isError, int status, string errorMessage, object data)
        {
            IsError = isError;
            Status = status;
            ErrorMessage = errorMessage;
            Data = data;

        }


        public static BaseResponse Ok() => new BaseResponse(false, 200, "", null);
        public static BaseResponse Ok(object data) => new BaseResponse(false, 200, "", data);
        public static BaseResponse Error(int status, string message = "") => new BaseResponse(true, status, message, null);
    }
}