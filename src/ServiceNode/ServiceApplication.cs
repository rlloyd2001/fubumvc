﻿using FubuMVC.Core;
using FubuMVC.Core.ServiceBus.Configuration;
using FubuMVC.Core.ServiceBus.Polling;

namespace ServiceNode
{
    public class ServiceApplication : IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            return FubuApplication.For<ServiceRegistry>();
        }
    }

    public class ServiceRegistry : FubuTransportRegistry<TestBusSettings>
    {
        public ServiceRegistry()
        {
            Features.ServiceBus.Enable(true);
            Channel(x => x.Service).ReadIncoming();
            HealthMonitoring.ScheduledExecution(ScheduledExecution.Disabled);
        }
    }
}