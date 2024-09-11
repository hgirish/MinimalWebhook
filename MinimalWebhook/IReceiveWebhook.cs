
// Add services to the container.




public interface IReceiveWebhook
{
    Task<string> ProcessRequest(string requestBody);
}
