namespace PasswordEncrypter.Algorithms
{
    public interface IAlgorithm
    {
        string Crypt(string value);
        string Crypt(string value, string salt, SaltType saltType);
        string Crypt(string value, string beforeSalt, string afterSalt);
    }
}