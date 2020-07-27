using System;
using System.Linq;
using System.Reflection;

namespace DevCompanion.Service
{
	public static class ConstantUnitTypeAttributes
	{
		public static string DisplayName<T>(this T enumValue)
			where T: Enum
		{
			Type type = typeof(T);
			MemberInfo[] infoList = type.GetMember(enumValue.ToString());
			MemberInfo info = infoList.FirstOrDefault(item => item.DeclaringType == type);
			if (info == null) { return enumValue.AsName(); }
			object[] valueAttributes = info.GetCustomAttributes(typeof(Constants.DisplayNameAttribute), false);
			if (valueAttributes.Length == 0) { return enumValue.AsName(); }
			string displayName = $"{((Constants.DisplayNameAttribute)valueAttributes[0]).Message}";
			if (string.IsNullOrWhiteSpace(displayName)) { return enumValue.AsName(); }
			return displayName;
		}

		public static string AsName(this Enum enumValue)
		{
			return Enum.GetName(enumValue.GetType(), enumValue);
		}
	}

	public partial class Constants
	{

		[AttributeUsage(AttributeTargets.Field)]
		internal class DisplayNameAttribute : Attribute
		{
			public string Message { get; set; }
			public DisplayNameAttribute(string message)
			{
				this.Message = message;
			}
		}
	}
}
