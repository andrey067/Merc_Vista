using Domain.Enums;
using System.ComponentModel;

namespace Infrastructure.Extensions
{
    public static class B3CompaniesEnumExtensions
    {
        public static string GetDescription(this B3Companies stockCode)
        {
            var fieldInfo = stockCode.GetType().GetField(stockCode.ToString());
            var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute != null ? descriptionAttribute.Description : stockCode.ToString();
        }
    }
}
