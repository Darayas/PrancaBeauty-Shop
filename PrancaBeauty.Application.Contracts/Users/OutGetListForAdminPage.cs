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

        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "AccessLevelName")]
        public string AccessLevelName { get; set; }

        [Display(Name = "RegDate")]
        public DateTime Date { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
    }
}
