namespace ColoredLive.MainService.Requests
{
    public class RegisterRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
    }
}