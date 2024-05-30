// Ignore Spelling: Github Json

namespace Skyline.DataMiner.Github.Repositories
{
	using System;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;

	using Newtonsoft.Json.Linq;

	using Skyline.DataMiner.Github.Repositories.Attributes;

	public static class Extensions
	{
		public static string FriendlyDescription<T>(this T requestType) where T : Enum
		{
			var name = requestType.ToString();
			FieldInfo field = typeof(T).GetField(name);
			object[] attribs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (attribs.Length > 0)
			{
				return ((DescriptionAttribute)attribs[0]).Description;
			}

			return name;
		}

		public static T ParseEnumDescription<T>(string description) where T : Enum
		{
			var enumType = typeof(T);
			var descriptions = enumType.GetFields().ToDictionary(field => field, field => field.GetCustomAttribute<DescriptionAttribute>());
			var @enum = descriptions.FirstOrDefault(desc => desc.Value != null && desc.Value.Description == description);
			if (@enum.Value != null)
			{
				return (T)Enum.Parse(enumType, @enum.Key.Name);
			}

			return (T)Enum.Parse(enumType, description);
		}

		public static string ShortDescription<T>(this T requestType) where T : Enum
		{
			var name = requestType.ToString();
			FieldInfo field = typeof(T).GetField(name);
			object[] attribs = field.GetCustomAttributes(typeof(ShortDescriptionAttribute), false);
			if (attribs.Length > 0)
			{
				return ((ShortDescriptionAttribute)attribs[0]).Description;
			}

			return name;
		}

		public static bool IsJsonArray(this string json)
		{
			try
			{
				JArray.Parse(json);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}

	public static class LocalStorage
	{
		private static readonly string SonarTokenPath = @"C:\Skyline DataMiner\Documents\Github Repositories\SonarToken.key";

		public static string ReadSonarToken()
		{
			return System.IO.File.ReadAllText(SonarTokenPath);
		}
	}
}
