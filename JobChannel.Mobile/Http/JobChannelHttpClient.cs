using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JobChannel.Mobile.Domain.Contracts;
using JobChannel.Mobile.Http.Requests;

namespace JobChannel.Mobile.Http
{
    public class JobChannelHttpClient : IJobChannelHttpClient
    {
        private static volatile JobChannelHttpClient _instance;
        private static readonly object _syncRoot = new object();

        private readonly HttpClient _client = new HttpClient();
        public DateTime ExpireDate { get; private set; }
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public enum TypeREST { Get, Post, Put, Delete };

        private JobChannelHttpClient()
        {
            _client.BaseAddress = new Uri("http://user47.2isa.org");
            //_client.BaseAddress = new Uri("https://localhost:5001");

            ExpireDate = DateTime.MinValue;
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public static JobChannelHttpClient Instance // Propriété static pour créer l'instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot) // Verrou pour les accès multi threads
                    {
                        if (_instance == null)
                        {
                            _instance = new JobChannelHttpClient();
                        }
                    }
                }

                return _instance;
            }
        }

        public async Task<T> GetRequest<T>(string url)
        {
            var response = await CallHttpClient(TypeREST.Get, url);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> PostRequest<T, TRequest>(string url, TRequest data) where TRequest : IRequest
        {
            var response = await CallHttpClient(TypeREST.Post, url, data);

            var content = await response.Content.ReadFromJsonAsync<T>();

            return content;
        }

        public async Task<bool> PutRequest<TRequest>(string url, TRequest data) where TRequest : IRequest
        {
            var response = await CallHttpClient(TypeREST.Put, url, data);

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<bool> DeleteRequest(string url)
        {
            var response = await CallHttpClient(TypeREST.Delete, url);

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        private async Task<HttpResponseMessage> CallHttpClient(TypeREST typeREST, string uri, object data = null)
        {
            try
            {
                if (IsTokenExpired())
                    await GetAccessToken();

                StringContent? jsonBodyParameter = null;
                if (data != null)
                    jsonBodyParameter = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                return typeREST switch
                {
                    TypeREST.Get => await _client.GetAsync(_client.BaseAddress + uri),
                    TypeREST.Post => await _client.PostAsync(uri, jsonBodyParameter),
                    TypeREST.Put => await _client.PutAsync(uri, jsonBodyParameter),
                    TypeREST.Delete => await _client.DeleteAsync(uri),
                    _ => throw new NotImplementedException("Type Rest non Valide")
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task GetAccessToken()
        {
            try
            {
                var authRequest = new AuthenticationRequest("User", "PASSWORD_JOBCHANNEL_MOBILE");

                var httpResponse = await _client.PostAsJsonAsync(_client.BaseAddress + "authenticate", authRequest);
                var responseToken = await httpResponse.Content.ReadAsStringAsync();

                var token = jwtSecurityTokenHandler.ReadJwtToken(responseToken);

                ExpireDate = token.ValidTo;

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseToken);
            }
            catch (Exception)
            {
                throw new Exception("Authentication Problem");
            }
        }

        public bool IsTokenExpired()
        {
            return DateTime.UtcNow > ExpireDate;

        }
    }
}