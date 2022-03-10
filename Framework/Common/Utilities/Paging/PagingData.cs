using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Utilities.Paging
{
    public static class PagingData
    {
        public static OutPagingData Calc(long CountAllItem, int Page, int Take)
        {
            try
            {
                int _Skip = 0;
                int CountPages = 5;

                Page = Page <= 0 ? 1 : Page;

                // زمانی که هیچ دیتایی به جهت نمایش وجود ندارد
                if (CountAllItem == 0)
                {
                    return new OutPagingData
                    {
                        CountAllItem = 0,
                        CountAllPage = 1,
                        Page = 1,
                        Take = Take,
                        Skip = 0,
                        StartPage = 1,
                        EndPage = 1
                    };
                }

                // محاسبه تعداد صفحات
                int _CountAllPage = (int)(Math.Ceiling((decimal)CountAllItem / Take));

                if (CountAllItem < Take)
                    Take = (int)CountAllItem;

                if (Page > _CountAllPage)
                    Page = _CountAllPage;

                // _Skip محاسبه 
                _Skip = (int)((Take * Page) - Take);
                if (_Skip < 0)
                    _Skip = 0;

                int StartPage = Page - CountPages <= 0 ? 1 : Page - CountPages;
                int EndPage = Page + CountPages > _CountAllPage ? _CountAllPage : Page + CountPages;

                return new OutPagingData()
                {
                    CountAllItem = CountAllItem,
                    CountAllPage = _CountAllPage,
                    Page = Page,
                    Skip = _Skip,
                    Take = Take,
                    StartPage = StartPage,
                    EndPage = EndPage
                };
            }
            catch
            {
                return new OutPagingData
                {
                    CountAllItem = 0,
                    CountAllPage = 1,
                    Page = 1,
                    Take = Take,
                    Skip = 0,
                    StartPage = 1,
                    EndPage = 1
                };
            }
        }
    }
}
