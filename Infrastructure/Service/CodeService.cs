using Application.Contracts.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Service
{
    public class CodeService : ICodeService
    {
        private IMemoryCache _memoryCache;
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly int Length = 4;
        private static Random random = new Random();
        public CodeService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public string Generate(string requestUsername, string requestTelephone, out bool isNew)
        {
            var code = new string(Enumerable.Repeat(Chars, Length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            if (_memoryCache.Get("code") == null)
            {
                _memoryCache.Set("code", $"{requestUsername}_{requestTelephone}_{code}", TimeSpan.FromMinutes(2));
                isNew = true;
                return code;
            }
            if (_memoryCache.Get("code").ToString().Split('_').Take(2).Aggregate((i, j) => i + '_' + j) ==
                $"{requestUsername}_{requestTelephone}")
            {
                isNew = false;
                return _memoryCache.Get("code").ToString().Split('_').Last();
            }
            isNew = true;
            return code;
        }

        public bool VerifyCode(string username, string inputPhoneNumber, string inputConfirmationCode)
        {
            return _memoryCache.Get("code") != null && _memoryCache.Get("code").ToString() ==
                   $"{username}_{inputPhoneNumber}_{inputConfirmationCode}";
        }

        public void ClearCache()
        {
            _memoryCache.Remove("code");

        }
    }
}
