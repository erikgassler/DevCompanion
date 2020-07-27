﻿using System;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	/// <summary>
	/// Wrapper for OS file system interactions needed by services.
	/// </summary>
	public interface IFileSystem
	{
		string GetFullPath(string partialPath);
		bool TryGetRegistryValue(string key, out string value);
		void SetRegistryValue(string key, string value);
		bool Exists(string filePath);
		Task<string> ReadAllTextAsync(string filePath);
	}
}
