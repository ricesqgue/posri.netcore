using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PosRi.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item) where TEnum : struct
        {
            var descriptionAttr = item.GetType()
                .GetField(item.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            return descriptionAttr?.Description ?? string.Empty;
        }


        public static void SeedEnumValues<T, TEnum>(this DbSet<T> dbSet, Func<TEnum, T> converter) where T : class
        {
            Enum.GetValues(typeof(TEnum))
                .Cast<object>()
                .Select(v => converter((TEnum)v))
                .ToList()
                .ForEach(instance => dbSet.Add(instance));
        }
    }
}
