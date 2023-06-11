namespace SysMaTra.Models.ApiServices
{
    public class VoyageApi
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5070/api");
            return client;
        }
    }
}
