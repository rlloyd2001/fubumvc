﻿using FubuMVC.Core.ServiceBus.Configuration;
using FubuMVC.Core.ServiceBus.Monitoring;
using NUnit.Framework;
using Shouldly;

namespace FubuMVC.Tests.ServiceBus.Monitoring
{
    [TestFixture]
    public class Monitoring_handlers_are_registered
    {
        [Test]
        public void handler_calls_are_registered_by_default()
        {
            var graph = FubuTransportRegistry.HandlerGraphFor(x => { });

            graph.ChainFor<TakeOwnershipRequest>().ShouldNotBeNull();
            graph.ChainFor<TaskHealthRequest>().ShouldNotBeNull();
            graph.ChainFor<TaskDeactivation>().ShouldNotBeNull();
        }
    }
}