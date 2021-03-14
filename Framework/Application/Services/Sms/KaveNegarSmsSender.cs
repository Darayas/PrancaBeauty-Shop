using Framework.Infrastructure;
using Kavenegar;
using Kavenegar.Models;
using Microsoft.VisualBasic.CompilerServices;
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
            ApiKey = "464E415965386E3934784A504F416E705766534D39535A4E4D7944415369717A6578576C4943784B664B6B3D";
            Sender = "";
        }

        public bool Send(string PhoneNumber, string Message)
        {
            throw new ArgumentNullException();
        }

        private bool SendOTP(string PhoneNumber, string TemplateName, string[] Tokens)
        {
            try
            {
                if (PhoneNumber == null)
                    throw new ArgumentNullException("PhoneNumber cant be null");

                if (TemplateName == null)
                    throw new ArgumentNullException("TemplateName cant be null");

                if (Tokens == null)
                    throw new ArgumentNullException("Tokens cant be null");

                KavenegarApi Api = new KavenegarApi(ApiKey);
                SendResult Result = null;

                if (Tokens.Length == 0)
                {
                    throw new ArgumentNullException("Tokens cant be null");
                }
                else if (Tokens.Length == 1)
                {
                    Result = Api.VerifyLookup(PhoneNumber, Tokens[0], TemplateName);
                }
                else if (Tokens.Length == 2)
                {
                    Tokens.Append("");
                    Result = Api.VerifyLookup(PhoneNumber, Tokens[0], Tokens[1], Tokens[2], TemplateName);
                }
                else if (Tokens.Length == 3)
                {
                    Result = Api.VerifyLookup(PhoneNumber, Tokens[0], Tokens[1], Tokens[2], TemplateName);
                }

                var IsSent = CheckStatus(Result);

                return IsSent;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        private bool CheckStatus(SendResult Result)
        {
            try
            {
                if (Result.Status == 5)
                {
                    return true;
                }
                else
                {
                    if (Result.Status == 401)
                    {
                        throw new Exception("حساب کاربری غیرفعال شده است");
                    }
                    else if (Result.Status == 402)
                    {
                        throw new Exception("عملیات ناموفق بود");
                    }
                    else if (Result.Status == 403)
                    {
                        throw new Exception("کد شناسائی API-Key معتبر نمی‌باشد");
                    }
                    else if (Result.Status == 406)
                    {
                        throw new Exception("پارامترهای اجباری خالی ارسال شده اند");
                    }
                    else if (Result.Status == 407)
                    {
                        throw new Exception("دسترسی به اطلاعات مورد نظر برای شما امکان پذیر نیست. برای استفاده از متدهای Select، SelectOutbox و LatestOutBox و یا ارسال با خط بین المللی نیاز به تنظیم IP در بخش تنظیمات امنیتی می باشد");
                    }
                    else if (Result.Status == 409)
                    {
                        throw new Exception("سرور قادر به پاسخگوئی نیست بعدا تلاش کنید");
                    }
                    else if (Result.Status == 411)
                    {
                        throw new Exception($"دریافت کننده نامعتبر است, [{Result.Receptor}]");
                    }
                    else if (Result.Status == 412)
                    {
                        throw new Exception($"ارسال کننده نامعتبر است, [{Sender}]");
                    }
                    else if (Result.Status == 414)
                    {
                        throw new Exception("حجم درخواست بیشتر از حد مجاز است ،ارسال پیامک :هر فراخوانی حداکثر 200 رکوردو کنترل وضعیت :هر فراخوانی 500 رکورد");
                    }
                    else if (Result.Status == 415)
                    {
                        throw new Exception("اندیس شروع بزرگ تر از کل تعداد شماره های مورد نظر است");
                    }
                    else if (Result.Status == 416)
                    {
                        throw new Exception("IP سرویس مبدا با تنظیمات مطابقت ندارد");
                    }
                    else if (Result.Status == 417)
                    {
                        throw new Exception("تاریخ ارسال اشتباه است و فرمت آن صحیح نمی باشد.");
                    }
                    else if (Result.Status == 418)
                    {
                        throw new Exception("اعتبار شما کافی نمی‌باشد");
                    }
                    else if (Result.Status == 420)
                    {
                        throw new Exception("استفاده از لینک در متن پیام برای شما محدود شده است");
                    }
                    else if (Result.Status == 422)
                    {
                        throw new Exception("داده ها به دلیل وجود کاراکتر نامناسب قابل پردازش نیست");
                    }
                    else if (Result.Status == 424)
                    {
                        throw new Exception("الگوی مورد نظر پیدا نشد");
                    }
                    else if (Result.Status == 426)
                    {
                        throw new Exception("استفاده از این متد نیازمند سرویس پیشرفته می‌باشد");
                    }
                    else if (Result.Status == 427)
                    {
                        throw new Exception("استفاده از این خط نیازمند ایجاد سطح دسترسی می باشد");
                    }
                    else if (Result.Status == 428)
                    {
                        throw new Exception("ارسال کد از طریق تماس تلفنی امکان پذیر نیست");
                    }
                    else if (Result.Status == 429)
                    {
                        throw new Exception("IP محدود شده است");
                    }
                    else if (Result.Status == 432)
                    {
                        throw new Exception("پارامتر کد در متن پیام پیدا نشد");
                    }
                    else if (Result.Status == 451)
                    {
                        throw new Exception("فراخوانی بیش از حد در بازه زمانی مشخص IP محدود شده");
                    }
                    else if (Result.Status == 501)
                    {
                        throw new Exception("فقط امکان ارسال پیام تست به شماره صاحب حساب کاربری وجود دارد");
                    }
                    else
                    {
                        throw new Exception("Unknown error");
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public bool SendLoginCode(string PhoneNumber, string Code)
        {
            return SendOTP(PhoneNumber, "pr-SendLoginCode", new string[] { Code });
        }
    }
}
