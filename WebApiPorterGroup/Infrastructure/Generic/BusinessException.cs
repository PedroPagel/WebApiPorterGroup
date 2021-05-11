using System;

namespace Infrastructure.Generic
{
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException(string erro) : base(erro) { }
    }
}
