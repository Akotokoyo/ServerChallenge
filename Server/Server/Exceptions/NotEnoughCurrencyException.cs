using System;

namespace ServerArchitecture.Exceptions
{
    [Serializable]
    public class NotEnoughCurrencyException : Exception
    {
        public NotEnoughCurrencyException()
        {
        }
    }
}
