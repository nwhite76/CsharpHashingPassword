using System.Security.Cryptography;

int menuChoice = 0;
string password = string.Empty;
string hash = string.Empty;
byte[] salt = Array.Empty<byte>();

while (menuChoice != 5)
{
    Console.WriteLine("Password Hashing Example");
    Console.WriteLine("1. Hash a password - No Salt");
    Console.WriteLine("2. Verify a password - Not Salt");
    Console.WriteLine("3. Hash a password");
    Console.WriteLine("4. Verify a password");
    Console.WriteLine("5. Exit");
    Console.Write("Enter your choice: ");
    
    if (!int.TryParse(Console.ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > 5)
    {
        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
        continue;
    }
    switch (menuChoice)
    {
        case 1:
            HashPasswordNoSalt();
            break;
        case 2:
            VerifyPasswordNoSalt();
            break;
        case 3:
            HashPassword();
            break;
        case 4:
            VerifyPassword();
            break;
        case 5:
            Console.WriteLine("Exiting the program.");
            break;
    }
}


void HashPassword()
{

    Console.WriteLine("Password Hashing Example Using PBKDF2 and a Salt");
    Console.WriteLine("Enter a password to hash:");
    bool validPassword = false;


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
    // Using a salt is important to protect against rainbow table attacks and
    // ensure that the same password does not always produce the same hash.
    salt = RandomNumberGenerator.GetBytes(16);
    Console.WriteLine($"Generated Salt: {Convert.ToBase64String(salt)}");

    // Hash the password with the salt
    hash = Convert.ToBase64String(
        Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32));

    Console.WriteLine($"Generated Hash: {hash}");
}


void VerifyPassword()
{
    if (string.IsNullOrEmpty(hash) || salt.Length == 0)
    {
        Console.WriteLine("No password has been hashed yet. Please hash a password first.");
        return;
    }
    Console.WriteLine("Enter the password again to verify:");
    string passwordToVerify = Console.ReadLine();

    string hashToVerify = Convert.ToBase64String(
        Rfc2898DeriveBytes.Pbkdf2(
            passwordToVerify,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32));

    if (hashToVerify == hash)
    {
        Console.WriteLine("Password is correct!");
    }
    else
    {
        Console.WriteLine("Password is incorrect.");
    }
}

void HashPasswordNoSalt()
{
    Console.WriteLine("Password Hashing Example Without Salt");
    Console.WriteLine("Enter a password to hash:");
    bool validPassword = false;
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
    // Hash the password without a salt
    hash = Convert.ToBase64String(
        Rfc2898DeriveBytes.Pbkdf2(
            password,
            Array.Empty<byte>(), // No salt
            100000,
            HashAlgorithmName.SHA256,
            32));
    Console.WriteLine($"Generated Hash: {hash}");
}

void VerifyPasswordNoSalt()
{
    if (string.IsNullOrEmpty(hash))
    {
        Console.WriteLine("No password has been hashed yet. Please hash a password first.");
        return;
    }
    Console.WriteLine("Enter the password again to verify:");
    string passwordToVerify = Console.ReadLine();
    string hashToVerify = Convert.ToBase64String(
        Rfc2898DeriveBytes.Pbkdf2(
            passwordToVerify,
            Array.Empty<byte>(), // No salt
            100000,
            HashAlgorithmName.SHA256,
            32));
    if (hashToVerify == hash)
    {
        Console.WriteLine("Password is correct!");
    }
    else
    {
        Console.WriteLine("Password is incorrect.");
    }
}