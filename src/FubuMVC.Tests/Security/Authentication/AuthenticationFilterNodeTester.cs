﻿using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Security.Authentication;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Tests.Security.Authentication
{
    [TestFixture]
    public class AuthenticationFilterNodeTester
    {
        private AuthenticationFilterNode theNode;

        [SetUp]
        public void SetUp()
        {
            theNode = new AuthenticationFilterNode();
        }

        [Test]
        public void has_to_be_authentication()
        {
            theNode.Category.ShouldEqual(BehaviorCategory.Authentication);
        }

        public class SomeDependency { }
    }
}