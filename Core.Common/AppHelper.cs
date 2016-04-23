using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Core.Common
{
    public static class AppHelper
    {
        public static void CreateMap<TValueObject, TModel>()
        {
            Mapper.CreateMap<TValueObject, TModel>();
            Mapper.CreateMap<TModel, TValueObject>();
        }
        /// <summary>
        /// Convert to list of <see cref="SelectListItem"/>
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <param name="optionValue">The option value.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToEnumSelectListItemsWithDesc(Array items, String optionLabel = null, String optionValue = null, String selectedValue = null)
        {
            var list = new List<SelectListItem>();
            if (!String.IsNullOrEmpty(optionLabel))
            {
                list.Add(new SelectListItem { Text = optionLabel, Value = (String.IsNullOrEmpty(optionValue) ? "" : optionValue) });
            }
            foreach (var item in items)
            {
                var selectListItem = new SelectListItem
                {
                    Value = ((int)item).ToString(),
                    Text = (Attribute.GetCustomAttribute(item.GetType().GetField(item.ToString()), typeof(System.ComponentModel.DescriptionAttribute)) as System.ComponentModel.DescriptionAttribute).Description,
                };
                selectListItem.Selected = selectListItem.Value == selectedValue;
                list.Add(selectListItem);
            }
            return list;
        }

    }
}
