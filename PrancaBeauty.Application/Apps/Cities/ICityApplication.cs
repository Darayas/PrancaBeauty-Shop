using PrancaBeauty.Application.Contracts.City;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Cities
{
    public interface ICityApplication
    {
        ///// <summary>
        ///// لیست از شهر ها را بر اساس شناسه استان برمیگرداند
        ///// </summary>
        ///// <param name="LangId">شناسه زبان</param>
        ///// <param name="ProvinceId">شناسه استان</param>
        ///// <param name="Search">نام مورد نظر به جهت جستوجو</param>
        ///// <exception cref="ArgumentInvalidException"></exception>
        ///// <returns></returns>
        Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
    }
}