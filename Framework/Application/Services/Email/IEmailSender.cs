using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application.Services.Email
{
    public interface IEmailSender
    {
        public bool Send(string _To, string _Subject, string _Message);
        public Task SendAsync(string _To, string _Subject, string _Message);
    }
}
