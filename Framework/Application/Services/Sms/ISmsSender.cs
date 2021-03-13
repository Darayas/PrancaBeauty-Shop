using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application.Services.Sms
{
    public interface ISmsSender
    {
        public bool Send(string PhoneNumber, string Message);
    }
}
