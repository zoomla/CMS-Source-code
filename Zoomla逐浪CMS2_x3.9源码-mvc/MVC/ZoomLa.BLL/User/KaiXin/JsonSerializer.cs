using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace fastJSON
{
	internal class JSONSerializer
	{
		private readonly StringBuilder _output = new StringBuilder();
		
		public static string ToJSON(object obj)
		{
			return new JSONSerializer().ConvertToJSON(obj);
		}

		internal string ConvertToJSON(object obj)
		{
			WriteValue(obj);

			return _output.ToString();
		}

		private void WriteValue(object obj)
		{
			if (obj == null)
				_output.Append("null");
			
			else if (obj is sbyte ||
			         obj is byte ||
			         obj is short ||
			         obj is ushort ||
			         obj is int ||
			         obj is uint ||
			         obj is long ||
			         obj is ulong ||
			         obj is decimal ||
			         obj is double ||
			         obj is float)
				_output.Append(Convert.ToString(obj,NumberFormatInfo.InvariantInfo));
			
			else if (obj is bool)
				_output.Append(obj.ToString().ToLower()); // conform to standard
			
			else if (obj is char || obj is Enum || obj is Guid || obj is string)
				WriteString(obj.ToString());
			
			else if (obj is DateTime)
			{
				_output.Append("\"");
				_output.Append(((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss"));// conform to standard
				_output.Append("\"");
			}
			
			else if(obj is DataSet)
				WriteDataset((DataSet)obj);
			
			else if( obj is byte[])
				WriteByteArray((byte[]) obj);
			
			else if (obj is IDictionary)
				WriteDictionary((IDictionary)obj);
			
			else if (obj is Array || obj is IList || obj is ICollection)
				WriteArray((IEnumerable)obj);
			
			else
				WriteObject(obj);
			
		}
		
		private void WriteByteArray(byte[] bytes)
		{
			WriteString(Convert.ToBase64String(bytes));
		}
		
		//private void WriteHashTable(Hashtable hash)
		//{
		//    _output.Append("{");

		//    bool pendingSeparator = false;

		//    foreach (object entry in hash.Keys)
		//    {
		//        if (pendingSeparator)
		//            _output.Append(",");

		//        WriteValue(entry);
		//        _output.Append(":");
		//        WriteValue(hash[entry]);
		
		//        pendingSeparator = true;
		//    }

		//    _output.Append("}");
		//}
		
		private void WriteDataset(DataSet ds)
		{
			_output.Append("{");
			WritePair("$schema", ds.GetXmlSchema());
			_output.Append(",");

			foreach (DataTable table in ds.Tables)
			{
				_output.Append("\"");
				_output.Append(table.TableName);
				_output.Append("\":[");

				foreach (DataRow row in table.Rows)
				{
					_output.Append("{");
					foreach (DataColumn column in row.Table.Columns)
					{
						WritePair(column.ColumnName, row[column]);
					}
					_output.Append("}");
				}

				_output.Append("]");
			}
			// end dataset
			_output.Append("}");
		}

		private void WriteObject(object obj)
		{
			_output.Append("{");
			Type  t = obj.GetType();
			WritePair("$type", t.AssemblyQualifiedName);
			_output.Append(",");

			List<Getters> g = JSON.Instance.GetGetters(t);
			foreach (Getters p in g)
			{
				WritePair(p.Name,p.Getter(obj));
			}
			_output.Append("}");
		}
		
		private void WritePair(string name, string value)
		{
			WriteString(name);

			_output.Append(":");
			
			WriteString(value);
		}

		private void WritePair(string name, object value)
		{
			WriteString(name);

			_output.Append(":");

			WriteValue(value);
		}

		private void WriteArray(IEnumerable array)
		{
			_output.Append("[");

			bool pendingSeperator = false;

			foreach (object obj in array)
			{
				if (pendingSeperator)
					_output.Append(',');

				WriteValue(obj);

				pendingSeperator = true;
			}

			_output.Append("]");
		}

		private void WriteDictionary(IDictionary dic)
		{
			_output.Append("[");

			bool pendingSeparator = false;

			foreach (DictionaryEntry entry in dic)
			{
				if (pendingSeparator)
					_output.Append(",");

				_output.Append("{");
				WritePair("k",entry.Key);
				_output.Append(",");
				WritePair("v",entry.Value);
				_output.Append("}");


				pendingSeparator = true;
			}

			_output.Append("]");
		}
		
		private void WriteString(string s)
		{
			_output.Append('\"');

			foreach (char c in s)
			{
				switch (c)
				{
						case '\t': _output.Append("\\t"); break;
						case '\r': _output.Append("\\r"); break;
						case '\n': _output.Append("\\n"); break;
					case '"' :
						case '\\': _output.Append("\\");  _output.Append(c); break;
					default:
						{
							if (c >= ' ' && c < 128)
								_output.Append(c);
							else
							{
								_output.Append("\\u");
								_output.Append(((int)c).ToString("X4"));
							}
						}
						break;
				}
			}

			_output.Append('\"');
		}
	}
}
