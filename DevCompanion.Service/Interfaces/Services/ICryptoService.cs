namespace DevCompanion.Service
{
	public interface ICryptoService
	{
		string Encrypt(string json, string password);
		string Decrypt(string encryptedData, string password);
	}
}
