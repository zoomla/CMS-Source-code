using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.IO;

namespace fastJSON
{
	public class JSON
	{
		public readonly static JSON Instance = new JSON();
		private JSON()
		{

		}
		
		public string ToJSON(object obj)
		{
			return new JSONSerializer().ConvertToJSON(obj);
		}

		public object ToObject(string json)
		{
			Dictionary<string, object> ht = (Dictionary<string, object>)JsonParser.JsonDecode(json);
			if (ht == null)
				return null;

            //add by kaixin001
            return ht;
			//return ParseDictionary(ht);
		}

		#region [   PROPERTY GET SET CACHE   ]
		SafeDictionary<string, Type> _typecache = new SafeDictionary<string, Type>();
		private Type GetTypeFromCache(string typename)
		{
			if (_typecache.ContainsKey(typename))
				return _typecache[typename];
			else
			{
				Type t = Type.GetType(typename);
				_typecache.Add(typename, t);
				return t;
			}
		}

		SafeDictionary<string, PropertyInfo> _propertycache = new SafeDictionary<string, PropertyInfo>();
		private PropertyInfo getproperty(Type type, string propertyname)
		{
			if (propertyname == "$type")
				return null;
			StringBuilder sb = new StringBuilder();
			sb.Append(type.Name);
			sb.Append("|");
			sb.Append(propertyname);
			string n = sb.ToString();

			if (_propertycache.ContainsKey(n))
			{
				return _propertycache[n];
			}
			else
			{
				PropertyInfo[] pr = type.GetProperties();
				foreach (PropertyInfo p in pr)
				{
					StringBuilder sbb = new StringBuilder();
					sbb.Append(type.Name);
					sbb.Append("|");
					sbb.Append(p.Name);
					string nn = sbb.ToString();
					if (_propertycache.ContainsKey(nn) == false)
						_propertycache.Add(nn, p);
				}
				return _propertycache[n];
			}
		}

		private delegate void GenericSetter(object target, object value);

		private static GenericSetter CreateSetMethod(PropertyInfo propertyInfo)
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod();
			if (setMethod == null)
				return null;

			Type[] arguments = new Type[2];
			arguments[0] = arguments[1] = typeof(object);

			DynamicMethod setter = new DynamicMethod(
				String.Concat("_Set", propertyInfo.Name, "_"),
				typeof(void), arguments, propertyInfo.DeclaringType);
			ILGenerator il = setter.GetILGenerator();
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
			il.Emit(OpCodes.Ldarg_1);

			if (propertyInfo.PropertyType.IsClass)
				il.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
			else
				il.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

			il.EmitCall(OpCodes.Callvirt, setMethod, null);
			il.Emit(OpCodes.Ret);

			return (GenericSetter)setter.CreateDelegate(typeof(GenericSetter));
		}
		
		public delegate object GenericGetter(object target);
		
		private static GenericGetter CreateGetMethod(PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			if (getMethod == null)
				return null;

			Type[] arguments = new Type[1];
			arguments[0] = typeof(object);

			DynamicMethod getter = new DynamicMethod(
				String.Concat("_Get", propertyInfo.Name, "_"),
				typeof(object), arguments, propertyInfo.DeclaringType);
			ILGenerator il = getter.GetILGenerator();
			il.DeclareLocal(typeof(object));
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
			il.EmitCall(OpCodes.Callvirt, getMethod, null);

			if (!propertyInfo.PropertyType.IsClass)
				il.Emit(OpCodes.Box, propertyInfo.PropertyType);

			il.Emit(OpCodes.Ret);

			return (GenericGetter)getter.CreateDelegate(typeof(GenericGetter));
		}
		
		SafeDictionary<Type, List<Getters>> _getterscache = new SafeDictionary<Type, List<Getters>>();
		public List<Getters> GetGetters(Type type)
		{
			if(_getterscache.ContainsKey(type))
				return _getterscache[type];
			else
			{
				PropertyInfo[] props = type.GetProperties(BindingFlags.Public| BindingFlags.Instance);
				List<Getters> getters = new List<Getters>();
				foreach(PropertyInfo p in props)
				{
					GenericGetter g = CreateGetMethod(p);
					if(g!=null)
					{
						Getters gg = new Getters();
						gg.Name = p.Name;
						gg.Getter = g;
						getters.Add(gg);
					}
					
				}
				_getterscache.Add(type,getters);
				return getters;
			}
		}

		SafeDictionary<PropertyInfo, GenericSetter> _settercache = new SafeDictionary<PropertyInfo, GenericSetter>();
		private GenericSetter GetSetter(PropertyInfo prop)
		{
			if (_settercache.ContainsKey(prop))
				return _settercache[prop];
			else
			{
				GenericSetter s = CreateSetMethod(prop);
				_settercache.Add(prop, s);
				return s;
			}
		}
		#endregion

		private object ParseDictionary(Dictionary<string, object> d)
		{
			string tn = "" + d["$type"];
			Type type = GetTypeFromCache(tn);
			object o = Activator.CreateInstance(type);
			foreach (string name in d.Keys)
			{
				PropertyInfo pi = getproperty(type, name);
				if (pi != null)
				{
					object v = d[name];

					if (v != null)
					{
						object oset = null;
						GenericSetter setter;
						Type pt = pi.PropertyType;
						object dic = pt.GetInterface("IDictionary");
						if (pt.IsGenericType && pt.IsValueType == false && dic==null)
						{
							IList col = (IList)Activator.CreateInstance(pt);
							// create an array of objects
							foreach (object ob in (ArrayList)v)
								col.Add(ParseDictionary((Dictionary<string, object>)ob));

							oset = col;
						}
						else if (pt == typeof(byte[]))
						{
							oset = Convert.FromBase64String((string)v);
						}
						else if (pt.IsArray && pt.IsValueType == false)
						{
							ArrayList col = new ArrayList();
							// create an array of objects
							foreach (object ob in (ArrayList)v)
								col.Add(ParseDictionary((Dictionary<string, object>)ob));

							oset = col.ToArray(pi.PropertyType.GetElementType());
						}
						else if (pt == typeof(Guid) || pt == typeof(Guid?))
							oset = new Guid("" + v);
						
						else if (pt == typeof(DataSet))
							oset = CreateDataset((Dictionary<string, object>)v);
						
						else if (pt == typeof(Hashtable))
							oset = CreateDictionary((ArrayList)v,pt);
						
						else if (dic!=null)
							oset = CreateDictionary((ArrayList)v,pt);
						
						else
							oset = ChangeType(v, pt);
						
						setter = GetSetter(pi);
						setter(o, oset);
					}
				}
			}
			return o;
		}
		
		private object CreateDictionary(ArrayList reader, Type pt)
		{
			IDictionary col = (IDictionary)Activator.CreateInstance(pt);
			Type[] types = col.GetType().GetGenericArguments();
			
			foreach (object o in reader)
			{
				Dictionary<string, object> values = (Dictionary<string, object>)o;

				object key;
				object val;
				
				
				if(values["k"] is Dictionary<string,object>)
					key = ParseDictionary((Dictionary<string,object>)values["k"]);
				else
					key = ChangeType(values["k"] , types[0]);

				if (values["v"] is Dictionary<string, object>)
					val = ParseDictionary((Dictionary<string, object>)values["v"]);
				else
					val = ChangeType(values["v"] , types[1]);

				col.Add(key , val);
				
			}
			
			return col;
		}
		
		public object ChangeType(object value, Type conversionType)
		{
			if ( conversionType.IsGenericType &&
			    conversionType.GetGenericTypeDefinition( ).Equals( typeof( Nullable<> ) ) ) {
				
				System.ComponentModel.NullableConverter nullableConverter
					= new System.ComponentModel.NullableConverter(conversionType);
				
				conversionType = nullableConverter.UnderlyingType;
			}
			
			return Convert.ChangeType(value, conversionType);
		}
		
		private Hashtable CreateHashtable(ArrayList reader)
		{
			Hashtable ht = new Hashtable();
			
			foreach (object o in reader)
			{
				Dictionary<string, object> values = (Dictionary<string, object>)o;

				ht.Add(
					ParseDictionary((Dictionary<string,object>)values["k"]),
					ParseDictionary((Dictionary<string,object>)values["v"])
				);
			}
			return ht;
		}

		private DataSet CreateDataset(Dictionary<string, object> reader)
		{
			DataSet ds = new DataSet();

			// read dataset schema here
			string s = "" + reader["$schema"];
			TextReader tr = new StringReader(s);
			ds.ReadXmlSchema(tr);

			foreach (string key in reader.Keys)
			{
				if (key == "$type" || key == "$schema")
					continue;
				object tb = reader[key];
				if (tb != null && tb.GetType() == typeof(ArrayList))
				{
					ArrayList rows = (ArrayList)tb;
					foreach (Dictionary<string,object> row in rows)
					{
						DataRow dr = ds.Tables[key].NewRow();
						foreach (string col in row.Keys)
						{
							dr[col] = row[col];
						}

						ds.Tables[key].Rows.Add(dr);
					}
				}
			}

			return ds;
		}
	}
}
