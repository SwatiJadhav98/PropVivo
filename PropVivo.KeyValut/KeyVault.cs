using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace PropVivo.KeyValut
{
    public class KeyVault : IKeyVault
    {
        private Dictionary<string, KeyVaultObject> _mappings = new Dictionary<string, KeyVaultObject>();

        public KeyVault(KeyVaultConfiguration keyVaultConfiguration)
        {
            KeyVaultConfiguration = keyVaultConfiguration;
            LoadKeys();
        }

        public KeyVaultConfiguration KeyVaultConfiguration { get; }

        public async Task<byte[]> ExtractCertificateAsync(string certKey, bool exportKey = false)
        {
            try
            {
                var vaultUri = new Uri(KeyVaultConfiguration.KeyVaultUrl);
                var client = new SecretClient(vaultUri, new DefaultAzureCredential());

                KeyVaultSecret secret;
                if (!exportKey)
                {
                    // Append vault address
                    certKey = $"{vaultUri}/certificates/{certKey}";
                    secret = await client.GetSecretAsync(certKey);
                    return Convert.FromBase64String(secret.Value);
                }
                else
                {
                    certKey = $"{vaultUri}/secrets/{certKey}";
                    secret = await client.GetSecretAsync(certKey);
                    return Convert.FromBase64String(secret.Value);
                }
            }
            catch (Exception)
            {
                return Array.Empty<byte>();
            }
        }

        public string GetValue(string key)
        {
            var response = "";

            KeyVaultObject? obj = null;
            if (_mappings.TryGetValue(key, out obj))
            {
                response = obj.KeyValue;
            }

            return response;
        }

        private async Task LoadAdditionalKeysAsync(SecretClient client)
        {
            var prefix = KeyVaultConfiguration.Convertor.EnvKey;

            foreach (string foo in KeyVaultConfiguration.KeysToExtract)
            {
                if (KeyVaultConfiguration.Convertor.Action.Invoke(foo))
                {
                    var secret = await client.GetSecretAsync(KeyVaultConfiguration.Convertor.KeyRename.Invoke(foo.ToString()));

                    _mappings.Add(foo, new KeyVaultObject { DestinationSource = DestinationSource.KeyVault, KeyValue = secret.Value.Value });
                }
            }
        }

        private void LoadKeys()
        {
            var vaultUri = new Uri(KeyVaultConfiguration.KeyVaultUrl);
            var credential = new ClientSecretCredential(KeyVaultConfiguration.TenantId, KeyVaultConfiguration.ClientId, KeyVaultConfiguration.ClientSecret);
            var secretClient = new SecretClient(vaultUri, credential);

            // var secret = await client.GetSecretAsync(KeyVaultConfiguration.KeyToExtract);
            //_mappings = JsonConvert.DeserializeObject<Dictionary<string, KeyVaultObject>>(secret.Value.Value);

            var allSecrets = secretClient.GetPropertiesOfSecrets();
            foreach (var secret in allSecrets)
            {
                var secretValue = secretClient.GetSecret(secret.Name);
                _mappings.Add(secret.Name, new KeyVaultObject { DestinationSource = DestinationSource.KeyVault, KeyValue = secretValue.Value.Value });
            }

            //await LoadAdditionalKeysAsync(secretClient);
        }
    }
}