using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public static class Enkripsi
{
    // Start is called before the first frame update
    static string DESKey = "SYAMSULN"; //set any string of 8 chars

    //DES - Encription 
    public static string DESEncryption(string inputData)
    {
        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
        byte[] keyByteData = ASCIIEncoding.ASCII.GetBytes(DESKey);

        DESCryptoServiceProvider DEScryptoProvider = new DESCryptoServiceProvider();
        ICryptoTransform trnsfrm = DEScryptoProvider.CreateEncryptor(keyByteData, keyByteData);
        CryptoStreamMode mode = CryptoStreamMode.Write;

        //Set up Stream & Write Encript data
        MemoryStream mStream = new MemoryStream();
        CryptoStream cStream = new CryptoStream(mStream, trnsfrm, mode);
        cStream.Write(txtByteData, 0, txtByteData.Length);
        cStream.FlushFinalBlock();

        //Read Ecncrypted Data From Memory Stream
        byte[] result = new byte[mStream.Length];
        mStream.Position = 0;
        mStream.Read(result, 0, result.Length);

        return Convert.ToBase64String(result);
    }

    //DES -  Decryption
    public static string DESDecryption(string inputData)
    {
        byte[] txtByteData = Convert.FromBase64String(inputData);
        byte[] keyByteData = ASCIIEncoding.ASCII.GetBytes(DESKey);

        DESCryptoServiceProvider DEScryptoProvider = new DESCryptoServiceProvider();
        ICryptoTransform trnsfrm = DEScryptoProvider.CreateDecryptor(keyByteData, keyByteData);
        CryptoStreamMode mode = CryptoStreamMode.Write;

        //Set up Stream & Write Encript data
        MemoryStream mStream = new MemoryStream();
        CryptoStream cStream = new CryptoStream(mStream, trnsfrm, mode);
        cStream.Write(txtByteData, 0, txtByteData.Length);
        cStream.FlushFinalBlock();

        //Read Ecncrypted Data From Memory Stream
        byte[] result = new byte[mStream.Length];
        mStream.Position = 0;
        mStream.Read(result, 0, result.Length);

        return ASCIIEncoding.ASCII.GetString(result);
    }
}
