﻿using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.ServiceBus.Configuration;
using FubuMVC.Core.ServiceBus.Polling;
using ServiceNode;

namespace WebsiteNode
{
    public class WebsiteApplication : IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            return FubuApplication.For<FubuRegistry>(x =>
            {
                x.Services(_ => _.For<MessageRecorder>().Singleton());
                x.Features.ServiceBus.Enable(true);
            });
        }
    }

    public class WebsiteRegistry : FubuTransportRegistry<TestBusSettings>
    {
        public WebsiteRegistry()
        {
            Channel(x => x.Website).ReadIncoming();
            Channel(x => x.Service).AcceptsMessagesInAssemblyContainingType<ServiceApplication>();

            HealthMonitoring.ScheduledExecution(ScheduledExecution.Disabled);
        }
    }

    public class MessageRecorder
    {
        public IList<string> Messages = new List<string>();
    }
}