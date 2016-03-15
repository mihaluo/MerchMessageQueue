﻿// Copyright © 2013-2015 Sergey Odinokov, Marco Casamento 
// This software is based on https://github.com/HangfireIO/Hangfire.Redis 

// Hangfire.Redis.StackExchange is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as 
// published by the Free Software Foundation, either version 3 
// of the License, or any later version.
// 
// Hangfire.Redis.StackExchange is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with Hangfire.Redis.StackExchange. If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace MarchMessageQueue.Hangfire.Redis.Storage
{
    public static class RedisDatabaseExtensions
	{
		public static HashEntry[] ToHashEntries(this IEnumerable<KeyValuePair<string, string>> keyValuePairs)
		{
			var hashEntry = new HashEntry[keyValuePairs.Count()];
			int i = 0;
			foreach (var kvp in keyValuePairs)
			{
				hashEntry[i] = new HashEntry(kvp.Key, kvp.Value);
				i++;
			}
			return hashEntry;
		}
		
		//public static Dictionary<string, string> ToStringDictionary(this HashEntry[] entries)
		//{
		//	var dictionary = new Dictionary<string, string>(entries.Length);
		//	foreach (var entry in entries)
		//		dictionary[entry.Name] = entry.Value;
		//	return dictionary;
		//}
		
		public static bool ContainsKey(this HashEntry[] hashEntries, RedisValue key)
		{
			return hashEntries.Any(x=> x.Name == key);
		}

        public static Dictionary<string, string> GetValuesMap(this IDatabase redis, string[] keys)
		{
			var redisKeyArr = keys.Select(x => (RedisKey)x).ToArray();
			var valuesArr = redis.StringGet(redisKeyArr);
			Dictionary<string, string> result = new Dictionary<string, string>(valuesArr.Length);
			for (int i = 0; i < valuesArr.Length; i++)
			{
				result.Add(redisKeyArr[i], valuesArr[i]);
			}
			return result;
		}


    }

}
