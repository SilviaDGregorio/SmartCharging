using System;

namespace SmartCharging.Domain
{
    public class AmpsException : Exception
    {
        public AmpsException()
        {
        }

        public AmpsException(string message)
            : base(message)
        {
        }
    }
}