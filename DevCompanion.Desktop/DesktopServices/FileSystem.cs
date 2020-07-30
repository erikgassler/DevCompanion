using DevCompanion.Service;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DevCompanion.Desktop
{
	public class FileSystem : IFileSystem
	{
		public bool IsValidDirectory(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) { return false; }
			path = GetFullPath(path);
			string test = path.Substring(path.Length - 5);
			if (path.Substring(path.Length-5) == ".dcbp")
			{
				path = GetDirectoryName(path);
			}
			try
			{
				DirectoryInfo info = new DirectoryInfo(path);
				if (info.Exists) { return true; }
				info.Create();
				if (!info.Exists) { return false; }
				info.Delete(true);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool SaveFileToDirectory(string filePath, string json)
		{
			try
			{
				string fullPath = GetFullPath(filePath);
				if (!IsValidDirectory(fullPath)) { return false; }
				using FileStream stream = File.OpenWrite(fullPath);
				using StreamWriter writer = new StreamWriter(stream);
				writer.Write(json);
				writer.Close();
				stream.Close();
				return true;
			}
			catch (Exception) { return false; }
		}

		public string GetFullPath(string partialPath)
		{
			return Path.GetFullPath(partialPath)
				.Replace(@"\\", "/")
				.Replace(@"\", "/");
		}

		public string GetDirectoryName(string path)
		{
			return Path.GetDirectoryName(path)
				.Replace(@"\\", "/")
				.Replace(@"\", "/");
		}

		public string ReadAllText(string filePath)
		{
			return File.ReadAllText(filePath);
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
