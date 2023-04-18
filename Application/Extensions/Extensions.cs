using AutoMapper.QueryableExtensions;
using AutoMapper;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Application.Extensions
{
    public static class Extensions
    {
        public static string GetName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            return enumType
                       .GetMember(enumValue.ToString())
                       .First(x => x.MemberType == MemberTypes.Field && ((FieldInfo)x).FieldType == enumType)
                       .GetCustomAttribute<DisplayAttribute>()?.Name ?? enumValue.ToString();
        }
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public static string DisplayDescription(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            return enumType
                       .GetMember(enumValue.ToString())
                       .First(x => x.MemberType == MemberTypes.Field && ((FieldInfo)x).FieldType == enumType)
                       .GetCustomAttribute<DescriptionAttribute>()?.Description ?? enumValue.ToString();
        }
        public static string UnifyUsername(this string username)
        {
            return new string(username.Where(c => char.IsDigit(c)).ToArray()).TrimStart('0');
        }
        public static string FromNow(this DateTime dateTimeOffset)
        {
            if (dateTimeOffset == DateTime.MinValue)
                return "Brak danych";
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTimeOffset.ToUniversalTime().Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "jedna sekunda temu" : ts.Seconds + " sekundy temu";

            if (delta < 2 * MINUTE)
                return "minuta temu";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minut temu";

            if (delta < 90 * MINUTE)
                return "godzina temu";

            if (delta < 24 * HOUR)
                return ts.Hours + " godzin temu";

            if (delta < 48 * HOUR)
                return "wczoraj";

            if (delta < 30 * DAY)
                return ts.Days + " dni temu";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "miesiąc temu" : months + " miesiące temu";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "rok temu" : years + " lat temu";
            }
        }
        // Paginated list
        public static PaginatedList<TDestination> PaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => Domain.Common.PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);


        public static List<TDestination> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToList();
        /// <summary>
        /// Creates PaginatedList with all item.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PaginatedList<TDestination> ToPaginatedList<TDestination>(this List<TDestination> list, int count, int pageNumber, int pageSize)
        {
            return Domain.Common.PaginatedList<TDestination>.Create(list, count, pageNumber, pageSize);
        }
    }
}
