using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Sellers
    {
        BaseRepository<tblSellers> _SellerRepository;
        BaseRepository<tblUsers> _UserRepository;

        MainContext _Context;
        public AddData_Sellers()
        {
            _Context = new MainContext();
            _SellerRepository = new BaseRepository<tblSellers>(_Context);
            _UserRepository = new BaseRepository<tblUsers>(_Context);
        }

        public void Run()
        {
            if (!_SellerRepository.Get.Where(a => a.tblUsers.UserName == "").Where(a => a.Name == "").Any())
            {
                _SellerRepository.AddAsync(new tblSellers
                {

                }, default, false).Wait();
            }

            _SellerRepository.SaveChangeAsync().Wait();
        }
    }
}
