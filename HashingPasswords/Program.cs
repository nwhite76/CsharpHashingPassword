using System.Security.Cryptography;

Console.WriteLine("Password Hashing Example");
Console.WriteLine("Enter a password to hash:");
bool validPassword = false;
string password = string.Empty;

while (!validPassword)
{
    password = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Password cannot be empty. Please enter a valid password:");
    }
    else
    {
        validPassword = true;
    }
}

// Generate a random salt
byte[] salt = RandomNumberGenerator.GetBytes(16);
Console.WriteLine($"Generated Salt: {Convert.ToBase64String(salt)}");

// Hash the password with the salt
string hash = Convert.ToBase64String(
    Rfc2898DeriveBytes.Pbkdf2(
        password,
        salt,
        100000,
        HashAlgorithmName.SHA256,
        32));

Console.WriteLine($"Generated Hash: {hash}");

Console.WriteLine("------------------------------------------");
Console.WriteLine("Enter the password again to verify:");
string passwordToVerify = Console.ReadLine();

string hashToVerify = Convert.ToBase64String(
    Rfc2898DeriveBytes.Pbkdf2(
        passwordToVerify,
        salt,
        100000,
        HashAlgorithmName.SHA256,
        32));

Console.WriteLine("Verifying password...");

if (hashToVerify == hash)
{
    Console.WriteLine("Password is correct!");
}
else
{
    Console.WriteLine("Password is incorrect.");
}