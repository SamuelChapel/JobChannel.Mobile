using JobChannel.Mobile.Domain.Contracts;

namespace JobChannel.Mobile.Http.Requests
{
    public class AuthenticationRequest : IRequest
    {
        public string login { get; set; }
        public string password { get; set; }

        public AuthenticationRequest(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }
}