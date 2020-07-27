using DevCompanion.Service;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DevCompanion.Desktop
{
	public class FileSystem : IFileSystem
	{
		public bool Exists(string filePath)
		{
			return File.Exists(filePath);
		}

		public string GetFullPath(string partialPath)
		{
			return Path.GetFullPath(partialPath);
		}

		public async Task<string> ReadAllTextAsync(string filePath)
		{
			return await File.ReadAllTextAsync(filePath);
		}

		public void SetRegistryValue(string key, string value)
		{
			using RegistryKey regSoftware = Registry.CurrentUser.OpenSubKey(RegistryKeyForSoftware, true);
			using RegistryKey regApp = regSoftware.CreateSubKey(RegistryKeyForAppName, true);
			regApp.SetValue(key, value);
		}

		public bool TryGetRegistryValue(string key, out string value)
		{
			using RegistryKey regSoftware = Registry.CurrentUser.OpenSubKey(RegistryKeyForSoftware, true);
			using RegistryKey regApp = regSoftware.CreateSubKey(RegistryKeyForAppName, true);
			value = (string)regApp.GetValue(key);
			return !string.IsNullOrWhiteSpace(value);
		}

		private const string RegistryKeyForSoftware = "Software";
		private const string RegistryKeyForAppName = "DevCompanion";
	}
}
