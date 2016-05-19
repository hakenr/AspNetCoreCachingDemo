using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCoreCachingDemo.Services
{
	public class XyRepository : IXyRepository
	{
		private readonly IMemoryCache memoryCache;

		public XyRepository(IMemoryCache memoryCache)
		{
			this.memoryCache = memoryCache;
		}

		private string FetchDataFromDb()
		{
			DateTime now = DateTime.Now;
			Thread.Sleep(300);
			return DateTime.Now.ToString(CultureInfo.InvariantCulture);
		}

		public string GetData()
		{
			return FetchDataFromDb();  // no caching

			const string cacheKey = "MyData";

			string result;
			if (!memoryCache.TryGetValue(cacheKey, out result))
			{
				lock (dataLock)
				{
					if (!memoryCache.TryGetValue(cacheKey, out result))
					{
						{
							result = FetchDataFromDb();

							memoryCache.Set(
								key: cacheKey,
								value: result,
								options: new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5), Priority = CacheItemPriority.Normal });
						}
					}
				}
			}
			return result;
		}
		private readonly object dataLock = new object();
	}
}