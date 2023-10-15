namespace API.Helpers
{
    public interface ITokenGenerator
    {
        public string GetToken(string username);
    }
}
