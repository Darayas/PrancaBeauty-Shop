using Framework.Infrastructure;
using Kavenegar;
using Kavenegar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application.Services.Sms
{
    public class KaveNegarSmsSender : ISmsSender
    {
        private ILogger _Logger;
        private readonly string ApiKey;
        private readonly string Sender;
        public KaveNegarSmsSender(ILogger logger)
        {
            _Logger = logger;
            ApiKey = "";
            Sender = "";
        }

        public bool Send(string PhoneNumber, string Message)
        {
            throw new ArgumentNullException();
        }

        public bool SendOTP(string PhoneNumber, string TemplateName, string[] Tokens)
        {
            if (PhoneNumber == null)
                throw new ArgumentNullException("PhoneNumber cant be null");

            if (TemplateName == null)
                throw new ArgumentNullException("TemplateName cant be null");

            if (Tokens == null)
                throw new ArgumentNullException("Tokens cant be null");

            KavenegarApi Api = new KavenegarApi(ApiKey);
            SendResult Result = null;
        }
    }
}
