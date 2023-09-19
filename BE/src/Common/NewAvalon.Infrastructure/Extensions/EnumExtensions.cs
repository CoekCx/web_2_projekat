using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace NewAvalon.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum enumValue) =>
            enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetDescription();
    }
}
