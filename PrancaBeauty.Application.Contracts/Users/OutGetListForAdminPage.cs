using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class OutGetListForAdminPage
    {
        public string Id { get; set; }

        public string AccessLevelId { get; set; }

        public string ImgUrl { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string AccessLevelName { get; set; }

        public DateTime Date { get; set; }

        public bool IsActive { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }
    }
}
