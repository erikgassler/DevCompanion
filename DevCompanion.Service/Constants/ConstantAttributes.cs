using System;
using System.Linq;
using System.Reflection;

namespace DevCompanion.Service
{
	public static class ConstantUnitTypeAttributes
	{
		/// <summary>
		/// Get enum attributes detailing descrition of what the enum value represents.
		/// Intended to be used in app doc pages and helper contexts.
		/// Returns empty string if attribute has not been set.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumValue"></param>
		/// <returns></returns>
		public static string Details<T>(this T enumValue)
			where T : Enum
		{
			Type type = typeof(T);
			MemberInfo[] infoList = type.GetMember(enumValue.ToString());
			MemberInfo info = infoList.FirstOrDefault(item => item.DeclaringType == type);
			if (info == null) { return enumValue.AsName(); }
			object[] valueAttributes = info.GetCustomAttributes(typeof(Constants.AboutAttribute), false);
			if (valueAttributes.Length == 0) { return ""; }
			string displayName = $"{((Constants.AboutAttribute)valueAttributes[0]).Details}";
			if (string.IsNullOrWhiteSpace(displayName)) { return ""; }
			return displayName;
		}

		/// <summary>
		/// Get enum attribute detailing a human readable name of the enum value.
		/// Intended to be used in option labels that change enum variable values.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumValue"></param>
		/// <returns></returns>
		public static string DisplayName<T>(this T enumValue)
			where T: Enum
		{
			Type type = typeof(T);
			MemberInfo[] infoList = type.GetMember(enumValue.ToString());
			MemberInfo info = infoList.FirstOrDefault(item => item.DeclaringType == type);
			if (info == null) { return enumValue.AsName(); }
			object[] valueAttributes = info.GetCustomAttributes(typeof(Constants.DisplayNameAttribute), false);
			if (valueAttributes.Length == 0) { return enumValue.AsName(); }
			string displayName = $"{((Constants.DisplayNameAttribute)valueAttributes[0]).DisplaName}";
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
		internal class AboutAttribute : Attribute
		{
			public string Details { get; set; }
			public AboutAttribute(string details)
			{
				this.Details = details;
			}
		}

		[AttributeUsage(AttributeTargets.Field)]
		internal class DisplayNameAttribute : Attribute
		{
			public string DisplaName { get; set; }
			public DisplayNameAttribute(string displayName)
			{
				this.DisplaName = displayName;
			}
		}
	}
}
