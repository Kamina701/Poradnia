using System.ComponentModel.DataAnnotations;
using System;
using SRP.Models.Commons;
using System.Linq;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using AutoMapper;

namespace SRP.Configurations
{
    public static class Extensions
    {

        // Paginated list
        public static PaginatedList<TDestination> PaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => Models.Commons.PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);
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
            return Models.Commons.PaginatedList<TDestination>.Create(list, count, pageNumber, pageSize);
        }

    }
}
