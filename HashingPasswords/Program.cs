using System.Security.Cryptography;

int menuChoice = 0;
string password = string.Empty;
string hash = string.Empty;
byte[] salt = Array.Empty<byte>();
string divider = "-------------------------------------------------------------";

while (menuChoice != 5)
{
    Console.WriteLine("######################################");
    Console.WriteLine("Password Hashing Example");
    Console.WriteLine("1. Hash a password - No Salt");
    Console.WriteLine("2. Verify a password - No Salt");
    Console.WriteLine("3. Hash a password - With a Salt");
    Console.WriteLine("4. Verify a password - With a Salt");
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
    Console.WriteLine(divider);
    Console.WriteLine("Password Hashing Example Using PBKDF2 and a Salt");
    Console.Write("Enter a password to hash: ");
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
    Console.WriteLine("A salt is used to ensure that passwords that are the same do not produced the same hash");
    
    Console.WriteLine(divider);
    Console.WriteLine($"Generated Salt: {Convert.ToBase64String(salt)}");
    Console.WriteLine(divider);

    // Hash the password with the salt
    hash = hashPassword(salt, password);

    Console.WriteLine(divider);
    Console.WriteLine($"Generated Hash: {hash}");
    Console.WriteLine(divider);
}

string hashPassword(byte[] hashSalt, string hashPassword)
{
    string createdHash = Convert.ToBase64String(
       Rfc2898DeriveBytes.Pbkdf2(
           hashPassword,
           hashSalt,    
           100000, // Simply rehashes the has 100,000 times.
           HashAlgorithmName.SHA256,
           32));
    return createdHash;
}
void VerifyPassword()
{
    Console.WriteLine(divider);
    if (string.IsNullOrEmpty(hash) || salt.Length == 0)
    {
        Console.WriteLine("No password has been hashed yet. Please hash a password first.");
        return;
    }

    Console.Write("Enter the password again to verify: ");
    string passwordToVerify = Console.ReadLine();

    string hashToVerify = hashPassword(salt, passwordToVerify);

    if (hashToVerify == hash)
    {
        Console.WriteLine(divider);
        Console.WriteLine("Password is correct!");
        Console.WriteLine(divider);
    }
    else
    {
        Console.WriteLine(divider);
        Console.WriteLine("Password is incorrect.");
        Console.WriteLine(divider);
    }
}

void HashPasswordNoSalt()
{
    Console.WriteLine(divider);
    Console.WriteLine("Password Hashing Example Without Salt");
    Console.Write("Enter a password to hash: ");
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
    hash = hashPassword(Array.Empty<byte>(), password);
    
    Console.WriteLine(divider);
    Console.WriteLine($"Generated Hash: {hash}");
    Console.WriteLine(divider);
}

void VerifyPasswordNoSalt()
{
    Console.WriteLine(divider);
    if (string.IsNullOrEmpty(hash))
    {
        Console.WriteLine("No password has been hashed yet. Please hash a password first.");
        return;
    }
    Console.Write("Enter the password again to verify: ");
    string passwordToVerify = Console.ReadLine();

    string hashToVerify = hashPassword(Array.Empty<byte>(), passwordToVerify);

    if (hashToVerify == hash)
    {
        Console.WriteLine(divider);
        Console.WriteLine("Password is correct!");
        Console.WriteLine(divider);
    }
    else
    {
        Console.WriteLine(divider);
        Console.WriteLine("Password is incorrect.");
        Console.WriteLine(divider);
    }
}