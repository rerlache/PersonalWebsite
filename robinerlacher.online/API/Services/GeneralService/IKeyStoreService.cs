using Microsoft.IdentityModel.Tokens;

namespace API.Services.GeneralService
{
    public interface IKeyStoreService
    {
        void StoreKey(string kid, SecurityKey key);
        SecurityKey GetKey(string kid);
    }
}
