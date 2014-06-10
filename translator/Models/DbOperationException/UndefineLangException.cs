using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace translator.Models.DbOperationException
{
    public class UndefineLangException : Exception
    {
        public UndefineLangException()
        {
        }

        public UndefineLangException(string message)
            : base(message)
        {
        }

        public UndefineLangException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}