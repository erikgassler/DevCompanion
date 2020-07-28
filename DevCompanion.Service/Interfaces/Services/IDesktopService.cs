﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DevCompanion.Service.Interfaces
{
	public interface IDesktopService
	{
		void InitializeDesktop(IDesktopWindow window);
		void StopDesktopServices();
	}
}
