﻿using FubuMVC.Core.ServiceBus.Configuration;

namespace FubuMVC.Tests.ServiceBus.Docs.Transports
{
    public class InMemorySamples
    {
        public void run_all_in_memory()
        {
            // SAMPLE: FubuTransportInMemory
            FubuTransport.AllQueuesInMemory = true;
            // ENDSAMPLE

            // SAMPLE: FubuTransportClearInMemory
            FubuTransport.Reset();
            // ENDSAMPLE
        }
    }
}