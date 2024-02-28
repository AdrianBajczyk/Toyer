using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration.AzureKeyVault;


namespace Toyer.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddKeyVaultSecretClient(this WebApplicationBuilder builder)
    {
        var keyVaultUrl = builder.Configuration["KeyVault:KeyVaultURL"];
        var keyVaultClientId = builder.Configuration["KeyVault:ClientId"];
        var keyVaultClientSecret = builder.Configuration["KeyVault:ClientSecret"];
        var keyVaultDirectoryId = builder.Configuration["KeyVault:DirectoryId"];

        var credential = new ClientSecretCredential(keyVaultDirectoryId, keyVaultClientId, keyVaultClientSecret);

        builder.Configuration.AddAzureKeyVault(keyVaultUrl, keyVaultClientId, keyVaultClientSecret, new DefaultKeyVaultSecretManager());

        var client = new SecretClient(new Uri(keyVaultUrl!), credential);

        builder.Services.AddSingleton(client);

        return builder;
    }
}