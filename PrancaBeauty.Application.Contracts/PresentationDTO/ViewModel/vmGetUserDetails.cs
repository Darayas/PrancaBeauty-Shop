using System;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmGetUserDetails
    {
        public string UserId { get; set; }
        public string SellerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string AccessLevel { get; set; }
        public DateTime Date { get; set; }
    }
}
