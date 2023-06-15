namespace Event_Manager.Data.Response
{
    public class ReponseStandards
    {
        public const string DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
    }


    public class BaseResponse
    {
        public BaseResponse()
        {
            ResultItem = null;
            ResultState = enumResultState.Unknown;
            ResultMessage = "";
        }

        public object ResultItem { get; set; }
        public enumResultState ResultState { get; set; }
        public string ResultMessage { get; set; }
    }

    public enum enumResultState
    {
        Unknown = 0,
        Success = 200,
        NoRowAffected = 201,

        Error = 400,
        IncorrectCredentials = 401,
        NotRegisteredOrActivated = 402
    }
}
