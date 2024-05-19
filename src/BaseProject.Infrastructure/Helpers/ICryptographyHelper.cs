namespace BaseProject.Infrastructure.Helpers
{
    public interface ICryptographyHelper
    {
        string GenerateSalt();

        string HashPassword(string password, string salt);

        string GenerateHash(string input);

        bool VerifyPassword(string password, string passwordHash, string passwordSalt);
    }
}