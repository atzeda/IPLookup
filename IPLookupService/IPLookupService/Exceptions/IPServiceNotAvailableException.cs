using System;

namespace IPLookupService.Exceptions
{
    public class IPServiceNotAvailableException : Exception
    {
        public IPServiceNotAvailableException() : base("The external IP service is not available.") { }
    }
}
