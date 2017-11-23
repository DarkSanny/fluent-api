using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ObjectPrinting
{
	public class PrintingConfig<TOwner>
	{
		private HashSet<Type> excudedTypes = new HashSet<Type>();
		private Dictionary<Type, Delegate> serializedTypes 
			= new Dictionary<Type, Delegate>();
		private Dictionary<Type, CultureInfo> cultures 
			= new Dictionary<Type, CultureInfo>();
		private Dictionary<string, Delegate> serializedProperties 
			= new Dictionary<string, Delegate>();
		private HashSet<string> excudedProperties = new HashSet<string>();

		public PrintingConfig<TOwner> ExcludeType<TType>()
		{
			excudedTypes.Add(typeof(TType));
			return this;
		}

		public void AddSerializeType(Type type, Delegate func)
		{
			serializedTypes[type] = func;
		}

		public void AddSerializeProperty(string propertyName, Delegate func)
		{
			serializedProperties[propertyName] = func;
		}

		public void AddCulture(Type type, CultureInfo cultureInfo)
		{
			cultures[type] = cultureInfo;
		}

		public SerializeConfig<TOwner, TType> Printing<TType>()
		{
			return new SerializeConfig<TOwner, TType>(this, null);
		}

		public SerializeConfig<TOwner, TProperty> Printing<TProperty>(
			Expression<Func<TOwner, TProperty>> memberSelector)
		{
			var propName = GetPropertyName(memberSelector);
			return new SerializeConfig<TOwner, TProperty>(this, propName);
		}

		private string GetPropertyName<TProperty>(Expression<Func<TOwner, TProperty>> memberSelector)
		{
			var prop = (PropertyInfo)((MemberExpression) memberSelector.Body).Member;
			return prop.Name;
		}

		public PrintingConfig<TOwner> ExcludeProperty<TProperty>(
			Expression<Func<TOwner, TProperty>> memberSelector)
		{
			var propName = GetPropertyName(memberSelector);
			excudedProperties.Add(propName);
			return this;
		}

		public string PrintToString(TOwner obj)
		{
			return PrintToString(obj, 0);
		}

		private string PrintProperty(object obj, PropertyInfo info, int nestingLevel)
		{
			if (serializedTypes.ContainsKey(info.PropertyType))
				return serializedTypes[info.PropertyType].DynamicInvoke(info.GetValue(obj)) + Environment.NewLine;
			if (serializedProperties.ContainsKey(info.Name))
				return serializedProperties[info.Name].DynamicInvoke(info.GetValue(obj)) + Environment.NewLine;
			if (cultures.ContainsKey(info.PropertyType))
				return ((IFormattable) info.GetValue(obj)).ToString(null, cultures[info.PropertyType]) + Environment.NewLine;

			return PrintToString(info.GetValue(obj), nestingLevel + 1);
		}


		private string PrintToString(object obj, int nestingLevel)
		{
			if (obj == null)
				return "null" + Environment.NewLine;
			var type = obj.GetType();
			var finalTypes = new[]
			{
				typeof(int), typeof(double), typeof(float), typeof(string),
				typeof(DateTime), typeof(TimeSpan)
			};
			if (finalTypes.Contains(type))
				return obj + Environment.NewLine;
			var properties = type.GetProperties();
			var identation = new string('\t', nestingLevel + 1);
			var builder = new StringBuilder(type.Name + Environment.NewLine);
			foreach (var propertyInfo in properties)
			{
				if (excudedTypes.Contains(propertyInfo.PropertyType)) continue;
				if (excudedProperties.Contains(propertyInfo.Name)) continue;
				builder.Append(identation + propertyInfo.Name + " = " +
				               PrintProperty(obj, propertyInfo,
					               nestingLevel));
			}
			return builder.ToString();
		}
	}
}