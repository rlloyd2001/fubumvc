﻿using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.ServiceBus;
using FubuMVC.Tests.ServiceBus.ScenarioSupport;
using NUnit.Framework;
using Shouldly;

namespace FubuMVC.Tests.ServiceBus
{
    [TestFixture]
    public class ServiceBus_Consume_right_now_Tester
    {
        [Test]
        public void send_now_is_handled_right_now()
        {

            using (var runtime = FubuApplication.For<FubuRegistry>(x =>
            {
                x.ServiceBus.Enable(true);
                x.ServiceBus.EnableInMemoryTransport();
                x.Handlers.DisableDefaultHandlerSource();
                x.Handlers.Include<SimpleHandler<OneMessage>>();
            }).Bootstrap())
            {
                var serviceBus = runtime.Factory.Get<IServiceBus>();

                TestMessageRecorder.Clear();

                var message = new OneMessage();

                serviceBus.Consume(message);

                TestMessageRecorder.ProcessedFor<OneMessage>().Single().Message
                    .ShouldBeTheSameAs(message);
            }
        }
    }
}