using Framework.Common.ExMethods;
using Framework.Domain.Enums;
using Framework.Infrastructure;
using PrancaBeauty.Domin.BankAccount.BankAccountAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Linq;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_BankAccounts
    {
        BaseRepository<tblBankAccounts> _RepBankAccount;
        BaseRepository<tblUsers> _RepUsers;
        public AddData_BankAccounts()
        {
            _RepBankAccount= new BaseRepository<tblBankAccounts>(new MainContext());
            _RepUsers= new BaseRepository<tblUsers>(new MainContext());
        }

        public void Run()
        {
            var _UserId = _RepUsers.Get.Where(a => a.Email=="reza9025@gmail.com").Select(a => a.Id).Single();

            if (!_RepBankAccount.Get.Where(a => a.UserId==_UserId).Where(a => a.AccountNumber=="2510001111").Any())
            {
                _RepBankAccount.AddAsync(new tblBankAccounts
                {
                    Id= new Guid().SequentialGuid(),
                    UserId=_UserId,
                    FullName="محمدرضا احمدی",
                    AccountNumber="2510001111",
                    CardNumber="1111000011110000",
                    IBAN="IR456412454500000002510001111",
                    BankName=BankAccountNamesEnum.Mellat,
                    Date=DateTime.Now,
                    IsConfirmed=true
                }, default, true).Wait();
            }

            if (!_RepBankAccount.Get.Where(a => a.UserId==_UserId).Where(a => a.AccountNumber=="45236444152").Any())
            {
                _RepBankAccount.AddAsync(new tblBankAccounts
                {
                    Id= new Guid().SequentialGuid(),
                    UserId=_UserId,
                    FullName="محمدرضا احمدی",
                    AccountNumber="45236444152",
                    CardNumber="1245325621853652",
                    IBAN="IR41254455100000045236444152",
                    BankName=BankAccountNamesEnum.Melli,
                    Date=DateTime.Now,
                    IsConfirmed=true
                }, default, true).Wait();
            }
        }
    }
}
