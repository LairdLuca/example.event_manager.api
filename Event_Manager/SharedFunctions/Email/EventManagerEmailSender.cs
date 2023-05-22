namespace Event_Manager.SharedFunctions.Email
{
    public class EventManagerEmailSender : EmailSender
    {
        //@ConfigurationSettings@
        private static string BaseURL = "http://localhost:17634";
        private static EventManagerEmailSender _instance { get; set; }

        public EventManagerEmailSender()
        {
            //@ConfigurationSettings@
            _emailAddressSender = "noreply@event_manager.com";
            _host = "smtp.mandrillapp.com";
            _port = 587;

            _credentialEmail = "email";
            _credentialPassword = "password";
        }

        public static EventManagerEmailSender Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventManagerEmailSender();
                return _instance;
            }
        }

        public static async void SendRequestLogin(string email)
        {
            string body = "";

            body += "<a href=\"" + BaseURL + "Home/ExecuteLogin?email=" + email + "\">";
            body += "Click to access";
            body += "</a>";

            await EventManagerEmailSender.Instance.SendEmail("Link to access", email, body);
        }
    }
}
