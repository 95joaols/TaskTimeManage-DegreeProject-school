using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Security;

public static class Cryptography
{
  public static string Encrypt(string encryptString, string salt)
  {
    string EncryptionKey = salt;
    byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
    using (Aes encryptor = Aes.Create())
    {
      Rfc2898DeriveBytes pdb = new(EncryptionKey, new byte[]
      {
                  0x65,
                  0x20,
                  0x61,
                  0x76,
                  0x49,
                  0x65,
                  0x76,
                  0x65,
                  0x6e,
                  0x76,
                  0x4d,
                  0x64,
                  0x64,
                  0x49,
                  0x65,
                  0x76,
                  0x65,
      });
      encryptor.Key = pdb.GetBytes(32);
      encryptor.IV = pdb.GetBytes(16);
      using MemoryStream ms = new();
      using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
      {
        cs.Write(clearBytes, 0, clearBytes.Length);
        cs.Close();
      }
      encryptString = Convert.ToBase64String(ms.ToArray());
    }
    return encryptString;
  }

  public static string Hash(string text, string salt) => BCrypt.Net.BCrypt.HashPassword(text, salt);

  public static string CreatSalt() => BCrypt.Net.BCrypt.GenerateSalt();
}
