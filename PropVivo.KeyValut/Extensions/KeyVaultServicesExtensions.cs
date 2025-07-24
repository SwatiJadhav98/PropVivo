using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PropVivo.KeyValut.Extensions
{
    public static class AzureKeyVaultExtensions
    {
        public static IServiceCollection AddAzureKeyValutService(this IServiceCollection services,
            IConfiguration _configuration)
        {
            services.AddSingleton<IKeyVault, KeyVault>(provider =>
            {
                var configuration = new KeyVaultConfiguration
                {
                    ClientId = _configuration["ClientId"].ToString(),
                    ClientSecret = _configuration["ClientSecret"].ToString(),
                    KeysToExtract = Enum.GetValues(typeof(Key)).Cast<Key>().Select(p => p.ToString()).ToList(),
                    KeyToExtract = _configuration["KeyName"].ToString(),
                    KeyVaultUrl = _configuration["KeyVaultURL"].ToString(),
                    TenantId = _configuration["TenantId"].ToString()
                }; // Assuming ProvideConfigurationForVault() method is defined somewhere
                return new KeyVault(configuration);
            });
            return services;
        }

        public static IConfigurationBuilder AddAzureKeyVaultConfiguration(this IConfigurationBuilder builder, IConfiguration _configuration)
        {
            KeyVaultSettings keyVaultConfig = new KeyVaultSettings
            {
                KeyVaultURL = _configuration["KeyVaultURL"].ToString(),
                ClientId = _configuration["ClientId"].ToString(),
                ClientSecret = _configuration["ClientSecret"].ToString(),
                TenantId = _configuration["TenantId"].ToString()
            };

            var clientSecretCredential = new ClientSecretCredential(keyVaultConfig.TenantId, keyVaultConfig.ClientId, keyVaultConfig.ClientSecret);
            builder.AddAzureKeyVault(new Uri(keyVaultConfig.KeyVaultURL), clientSecretCredential);
            return builder;
        }
    }
}