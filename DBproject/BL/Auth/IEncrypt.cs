namespace DBproject.BL.Auth
{
    public interface IEncrypt
    {
        string HashPassword(string password);
    }
}
