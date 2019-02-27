using System;
using System.Collections.Generic;

namespace fastJSON
{
	public class SafeDictionary<TKey, TValue>
	{
		private readonly object _Padlock = new object();
		private readonly Dictionary<TKey, TValue> _Dictionary = new Dictionary<TKey, TValue>();
		
		public bool ContainsKey(TKey key)
		{
			return _Dictionary.ContainsKey(key);
		}

		public TValue this[TKey key]
		{
			get
			{
				return _Dictionary[key];
			}
		}
		
		public void Add(TKey key, TValue value)
		{
			lock(_Padlock)
			{
				_Dictionary.Add(key,value);
			}
		}
	}
}
