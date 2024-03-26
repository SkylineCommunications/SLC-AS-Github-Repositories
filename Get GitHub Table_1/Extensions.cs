namespace Get_GitHub_Table_1
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;

	public static class Extensions
	{
		/// <summary>
		/// Fetches the friendly description defined with the <see cref="DescriptionAttribute"/>, or just the enum name if no <see cref="DescriptionAttribute"/> was found.
		/// </summary>
		/// <typeparam name="T">The type of the enum.</typeparam>
		/// <param name="requestType">The enum value.</param>
		/// <returns>A friendly description for the enum value.</returns>
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

		/// <summary>
		/// Returns the enum value from the friendly description defined with the <see cref="DescriptionAttribute"/>, or just the enum name if no <see cref="DescriptionAttribute"/> was found.
		/// </summary>
		/// <typeparam name="T">The type of the enum.</typeparam>
		/// <param name="description">The friendly description for the enum value.</param>
		/// <returns>The enum value.</returns>
		public static T ParseEnumDescription<T>(string description) where T : Enum
		{
			var enumType = typeof(T);
			var descriptions = enumType.GetFields().ToDictionary(field => field, field => field.GetCustomAttribute<DescriptionAttribute>());
			var @enum = descriptions.FirstOrDefault(desc => desc.Value != null && desc.Value.Description == description);
			if (@enum.Value != null)
			{
				return (T)Enum.Parse(enumType, @enum.Key.Name);
			}

			throw new KeyNotFoundException("There is no value for the given description");
		}
	}
}
