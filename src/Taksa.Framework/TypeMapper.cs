using System;
using System.Collections.Generic;

namespace Taksa.Framework
{
	using static String;

	public class TypeMapper
	{
		private readonly Dictionary<Type, string> namesByType = new Dictionary<Type, string>();
		private readonly Dictionary<string, Type> typesByName = new Dictionary<string, Type>();

		public TypeMapper Map(Type type, string name = null)
		{
			if (IsNullOrWhiteSpace(name))
				name = type.FullName;

			if (typesByName.ContainsKey(name))
				throw new InvalidOperationException(
					$"'{type}' is already mapped to the following name: {typesByName[name]}");

			typesByName[name] = type;
			namesByType[type] = name;

			return this;
		}

		public bool TryGetType(string name, out Type type)
		{
			return typesByName.TryGetValue(name, out type);
		}

		public bool TryGetTypeName(Type type, out string name)
		{
			return namesByType.TryGetValue(type, out name);
		}
	}

	public static class TypeMapperExtensions
	{
		public static TypeMapper Map<T>(this TypeMapper mapper, string name)
		{
			return mapper.Map(typeof(T), name);
		}

		public static string GetTypeName(this TypeMapper mapper, Type type)
		{
			if (!mapper.TryGetTypeName(type, out var name))
				throw new Exception($"Failed to find name mapped with '{type}'");

			return name;
		}

		public static Type GetType(this TypeMapper mapper, string name)
		{
			if (!mapper.TryGetType(name, out var type))
				throw new Exception($"Failed to find type mapped with '{name}'");

			return type;
		}
	}
}