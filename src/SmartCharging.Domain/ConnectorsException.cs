using System;

namespace SmartCharging.Domain
{
    public class ConnectorsException : Exception
    {
        public ConnectorsException()
        {
        }

        public ConnectorsException(string message)
            : base(message)
        {
        }
    }
}