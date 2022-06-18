using System;

namespace HSM
{
    public class NoActiveStateException : Exception
    {
        public NoActiveStateException(string message) : base(message)
        {
        }
    }
}