using Crypt_4;

try
{
    Console.WriteLine("Choose action: ");
    Console.WriteLine("1 - encode text");
    Console.WriteLine("2 - decode text");
    var choose = Console.ReadLine();

    var aes = new Aes();
    Console.Write("Input key: ");
    var key = Console.ReadLine();
    aes.CreateKey(key);

    switch (choose)
    {
        case "1":
            var iv = aes.CreateIV();
            Console.WriteLine($"IV: {iv}");
            Console.Write("Input text: ");
            var plainText = Console.ReadLine();
            var encodedText = aes.Encode(plainText);
            Console.WriteLine($"Encoded text: {encodedText}");
            break;
        case "2":
            Console.Write("Input IV: ");
            iv = Console.ReadLine();
            aes.SetIV(iv);
            Console.Write("Input text: ");
            encodedText = Console.ReadLine();
            var decodedText = aes.Decode(encodedText);
            Console.WriteLine($"Decoded text: {decodedText}");
            break;
    }
}
catch (Exception)
{
    Console.WriteLine("Incorrect input");
    throw;
}