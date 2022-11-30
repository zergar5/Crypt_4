using System.Security.Cryptography;
using System.Text;

namespace Crypt_4;

public class Aes
{
    private readonly AesCryptoServiceProvider _aes;

    public Aes()
    {
        _aes = new AesCryptoServiceProvider
        {
            BlockSize = 128,
            KeySize = 256,
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7
        };
    }
    public string Encode(string plainText)
    {
        var cryptoTransform = _aes.CreateEncryptor();

        var encodedBytes =
            cryptoTransform.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

        return Convert.ToBase64String(encodedBytes);
    }

    public string Decode(string cypherText)
    {
        var cryptoTransform = _aes.CreateDecryptor();

        var encodedBytes = Convert.FromBase64String(cypherText);

        var decodedBytes = cryptoTransform.TransformFinalBlock(encodedBytes, 0, encodedBytes.Length);

        return Encoding.UTF8.GetString(decodedBytes);
    }

    public void CreateKey(string key)
    {
        var keyHash = SHA256.HashData(Encoding.UTF8.GetBytes(key));
        _aes.Key = keyHash;
    }

    public string CreateIV()
    {
        Random rand = new(100);
        var iv = new byte[_aes.IV.Length];
        rand.NextBytes(iv);
        _aes.IV = iv;
        return Convert.ToBase64String(iv);
    }

    public void SetIV(string iv)
    {
        _aes.IV = Convert.FromBase64String(iv);
    }
}