using DevCompanion.Service;
using Xunit;

namespace DevCompanion.BuildTests.Services
{
	public class UTCryptoService : MockHelpers
	{
		[Theory]
		[InlineData("{}", "good")]
		[InlineData("{}", "a")]
		[InlineData("{}", "1")]
		[InlineData("could be any data!", "good")]
		[InlineData("!`@80&*$%^()}{[]:\";'|\\	", "good")]
		[InlineData("!`@80&*$%^()}{[]:\";'|\\	", "this is fine")]
		public void VerifyEncryptionAndDecryption(string dataInput, string password)
		{
			ICryptoService service = new CryptoService();
			string encrypted = service.Encrypt(dataInput, password);
			Assert.NotNull(encrypted);
			string decrypted = service.Decrypt(encrypted, password);
			Assert.Equal(dataInput, decrypted);
		}

		[Theory]
		[InlineData("some data", "")] // password must have some value
		[InlineData("", "some password")] // data must have some value
		[InlineData("BadData", "good password")] // data is not encrypted correctly
		public void VerifyFailedDecryption(string data, string password)
		{
			ICryptoService service = new CryptoService();
			string result = service.Decrypt(data, password);
			Assert.Null(result);
		}

		[Theory]
		[InlineData("some data", "")] // password must have some value
		[InlineData("", "some password")] // data must have some value
		public void VerifyFailedEncryption(string data, string password)
		{
			ICryptoService service = new CryptoService();
			string result = service.Encrypt(data, password);
			Assert.Null(result);
		}
	}
}
