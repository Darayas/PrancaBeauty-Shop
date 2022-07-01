using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Results
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

        public OperationResult Succeeded()
        {
            IsSucceeded = true;
            Message = "Operation was successded";

            return this;
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

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }

        public OperationResult<T> Succeeded(T _Data)
        {
            IsSucceeded = true;
            Message = "Operation was successded";
            Data=_Data;

            return this;
        }
        public OperationResult<T> Succeeded(string _Message, T _Data)
        {
            IsSucceeded = true;
            Message = _Message;
            Data=_Data;

            return this;
        }

        public OperationResult<T> Succeeded(int _Code, string _Message, T _Data)
        {
            IsSucceeded = true;
            Message = _Message;
            Code = _Code;
            Data=_Data;

            return this;
        }

        public OperationResult<T> Failed(string _Message)
        {
            IsSucceeded = false;
            Message = _Message;

            return this;
        }

        public OperationResult<T> Failed(int _Code, string _Message)
        {
            IsSucceeded = false;
            Message = _Message;
            Code = _Code;

            return this;
        }
    }
}
