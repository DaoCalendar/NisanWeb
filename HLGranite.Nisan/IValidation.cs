using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public interface IValidation
    {
        bool IsExist { get; }
        bool IsValid { get; }
        string Message { get; }
        void Validate();
    }
}