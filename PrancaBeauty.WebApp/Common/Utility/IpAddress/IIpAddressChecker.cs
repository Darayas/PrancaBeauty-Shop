﻿namespace PrancaBeauty.WebApp.Common.Utility.IpAddress
{
    public interface IIpAddressChecker
    {
        string CheckIp(string Ip);
        string GetLangAbbr(string Ip);
    }
}