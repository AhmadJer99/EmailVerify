using System.Configuration;
using EmailVerify.Controllers.ApiInterfaces;

namespace EmailVerify.Controllers;

public class HttpRequestCreator : IValidateEmail
{
    public HttpClient CreateHttpClient()
    {
        return new HttpClient();
    }

    public string LoadApiKey()
    {
        return ConfigurationManager.AppSettings.Get("EmailVerifyApiKey") ?? "";
    }

    public string LoadHttpValidationConnectionString()
    {
        return ConfigurationManager.AppSettings.Get("ValidationHttpEndpoint") ?? "";
    }
}