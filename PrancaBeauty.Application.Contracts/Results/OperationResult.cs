using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Results
{
    public class OperationResult
    {
        public bool IsSucceeded { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        public OperationResult()
        {
            IsSucceeded = false;
        }

        public OperationResult Succeeded(string _Message)
        {
            IsSucceeded = true;
            Message = _Message;

            return this;
        }

        public OperationResult Succeeded(int _Code, string _Message)
        {
            IsSucceeded = true;
            Message = _Message;
            Code = _Code;

            return this;
        }

        public OperationResult Failed(string _Message)
        {
            IsSucceeded = false;
            Message = _Message;

            return this;
        }

        public OperationResult Failed(int _Code, string _Message)
        {
            IsSucceeded = false;
            Message = _Message;
            Code = _Code;

            return this;
        }
    }
}
