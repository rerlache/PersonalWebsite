using Microsoft.IdentityModel.Tokens;

namespace API.Services.GeneralService
{
    public class KeyStoreService : IKeyStoreService
    {
        private readonly Dictionary<string, SecurityKey> _keyStore = new();
        public SecurityKey GetKey(string kid)
        {
            if(_keyStore.TryGetValue(kid, out SecurityKey key))
            {
                return key;
            }
            throw new SecurityTokenException("Key not found");
        }

        public void StoreKey(string kid, SecurityKey key)
        {
            _keyStore[kid] = key;
        }
    }
}
