using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DevCompanion.Service
{
	public class CryptoService : ICryptoService
	{
		public string Decrypt(string encryptedData, string password)
		{
			if (string.IsNullOrWhiteSpace(encryptedData)) { return null; }
			if (string.IsNullOrWhiteSpace(password)) { return null; }
			try
			{
				encryptedData = encryptedData.Replace(" ", "+");
				byte[] byteData = Convert.FromBase64String(encryptedData);
				return Encoding.Unicode.GetString(ServiceProcessing(byteData, password, CryptoMode.Decrypt));
			}
			catch (Exception) { }
			return null;
		}

		public string Encrypt(string json, string password)
		{
			if (string.IsNullOrWhiteSpace(json)) { return null; }
			if (string.IsNullOrWhiteSpace(password)) { return null; }
			try
			{
				byte[] byteData = Encoding.Unicode.GetBytes(json);
				return Convert.ToBase64String(ServiceProcessing(byteData, password, CryptoMode.Encrypt));
			}
			catch (Exception) { }
			return null;
		}

		private enum CryptoMode
		{
			Encrypt,
			Decrypt
		}
		private byte[] ServiceProcessing(byte[] byteData, string password, CryptoMode mode)
		{
			using MemoryStream memoryStream = new MemoryStream();
			using CryptoStream crypteStream = new CryptoStream(memoryStream, GetCryptoTransform(mode, password), CryptoStreamMode.Write);
			crypteStream.Write(byteData, 0, byteData.Length);
			crypteStream.Close();
			return memoryStream.ToArray();
		}

		private ICryptoTransform GetCryptoTransform(CryptoMode mode, string password)
		{
			using Aes cryptoService = Aes.Create();
			Rfc2898DeriveBytes byteDeriver = new Rfc2898DeriveBytes(password, GetSalt);
			cryptoService.Key = byteDeriver.GetBytes(32);
			cryptoService.IV = byteDeriver.GetBytes(16);
			return mode switch
			{
				CryptoMode.Decrypt => cryptoService.CreateDecryptor(),
				_ => cryptoService.CreateEncryptor(),
			};
		}

		private byte[] GetSalt => new byte[] { 0x16, 0x81, 0x11, 0x2a, 0x5f, 0x1a, 0x91, 0x14, 0x87, 0x5e, 0x7c, 0x43, 0x3a };
	}
}
