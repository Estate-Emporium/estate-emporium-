using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace estate_emporium.Services
{
  public class CertificateService()
  {
    private static readonly string secretName = "Certificate_PFX"; //name of your secret 
    private readonly IAmazonSecretsManager secretsManagerClient;

    public async Task<X509Certificate2> GetCertAndKey()
    {
      GetSecretValueRequest request = new GetSecretValueRequest
      {
        SecretId = secretName,
        VersionStage = "AWSCURRENT",
      };

      // var secretsManagerClient = new AmazonSecretsManagerClient();

      GetSecretValueResponse response = await secretsManagerClient.GetSecretValueAsync(request);

      var secretObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString);


      if (secretObject.TryGetValue("pfx", out string pfxBase64) && secretObject.TryGetValue("password", out string password))
      {
        // Decode base64 string to byte array
        byte[] pfxBytes = Convert.FromBase64String(pfxBase64);

        X509Certificate2 cert = new X509Certificate2(pfxBytes, password);

        return cert;
      }
      else
      {
        throw new Exception("PFX certificate or password not found in secret.");
      }
    }
  }
}