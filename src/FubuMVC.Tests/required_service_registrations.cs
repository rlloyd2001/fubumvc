﻿using System;
using FubuCore;
using FubuCore.Binding;
using FubuCore.Binding.InMemory;
using FubuCore.Conversion;
using FubuCore.Formatting;
using FubuCore.Logging;
using FubuMVC.Core;
using FubuMVC.Core.Assets;
using FubuMVC.Core.Assets.Templates;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Diagnostics.Assets;
using FubuMVC.Core.Http;
using FubuMVC.Core.Http.Cookies;
using FubuMVC.Core.Registration.Querying;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Resources.PathBased;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Runtime.Aggregation;
using FubuMVC.Core.Runtime.Conditionals;
using FubuMVC.Core.Runtime.Files;
using FubuMVC.Core.SessionState;
using FubuMVC.Core.Urls;
using FubuMVC.Tests.Registration;
using NUnit.Framework;

namespace FubuMVC.Tests
{
    [TestFixture]
    public class required_service_registrations
    {
        [Test]
        public void all_default_service_registrations()
        {
            FubuMode.Reset();

            using (var runtime = FubuApplication.DefaultPolicies().Bootstrap())
            {
                var _ = runtime.Container;

                // Model Binding registrations
                _.DefaultRegistrationIs<IBindingContext, BindingContext>();
                _.DefaultRegistrationIs<IObjectResolver, ObjectResolver>();
                _.DefaultRegistrationIs<IBindingLogger, NulloBindingLogger>();
                _.DefaultRegistrationIs<ISmartRequest, FubuSmartRequest>();
                _.ShouldHaveRegistration<IModelBinder, ResourcePathBinder>();


                // Core services
                _.DefaultRegistrationIs<IAggregator, Aggregator>();
                _.DefaultRegistrationIs<IFubuRequestContext, FubuRequestContext>();
                _.DefaultRegistrationIs<IRequestData, FubuMvcRequestData>();
                _.DefaultRegistrationIs<IAsyncCoordinator, AsyncCoordinator>();
                _.DefaultRegistrationIs<IExceptionHandlingObserver, ExceptionHandlingObserver>();
                _.DefaultRegistrationIs<ICookies, Cookies>();
                _.DefaultRegistrationIs<IFlash, FlashProvider>();
                _.DefaultRegistrationIs<IOutputWriter, OutputWriter>();
                _.DefaultRegistrationIs<IPartialFactory, PartialFactory>();
                _.DefaultRegistrationIs<IRequestDataProvider, RequestDataProvider>();
                _.DefaultRegistrationIs<Stringifier, Stringifier>(); // it's goofy, but assert that it exists

                _.DefaultSingletonIs<IChainResolver, ChainResolutionCache>();
                _.DefaultSingletonIs<TemplateGraph, TemplateGraph>();
                _.DefaultSingletonIs<IClientMessageCache, ClientMessageCache>();

                _.DefaultRegistrationIs<IDisplayFormatter, DisplayFormatter>();
                _.DefaultRegistrationIs<IEndpointService, EndpointService>();
                _.DefaultRegistrationIs<IFileSystem, FileSystem>();

                _.ShouldHaveRegistration<ILogModifier, LogRecordModifier>();

                _.DefaultRegistrationIs<IObjectConverter, ObjectConverter>();
                _.DefaultRegistrationIs<ISetterBinder, SetterBinder>();

                _.DefaultRegistrationIs<IFubuApplicationFiles, FubuApplicationFiles>();

                _.DefaultRegistrationIs<IUrlRegistry, UrlRegistry>();
                _.DefaultRegistrationIs<IChainUrlResolver, ChainUrlResolver>();
                _.DefaultRegistrationIs<AppReloaded, AppReloaded>();

                _.DefaultRegistrationIs<ILogger, Logger>();

                // Conneg
                _.DefaultRegistrationIs<IResourceNotFoundHandler, DefaultResourceNotFoundHandler>();



                // Conditionals
                _.DefaultRegistrationIs<IContinuationProcessor, ContinuationProcessor>();
                _.DefaultRegistrationIs<IConditionalService, ConditionalService>();

                // Assets
                _.DefaultRegistrationIs<IAssetTagBuilder, AssetTagBuilder>();
                _.DefaultSingletonIs<IAssetFinder, AssetFinderCache>();

                // Diagnostics
                _.DefaultSingletonIs<IDiagnosticAssets, DiagnosticAssetsCache>();

            }
        }

        [Test]
        public void IAssetTagBuilder_is_registered_in_development_mode()
        {
            FubuMode.SetUpForDevelopmentMode();

            using (var runtime = FubuApplication.DefaultPolicies().Bootstrap())
            {
                runtime.Container.DefaultRegistrationIs<IAssetTagBuilder, DevelopmentModeAssetTagBuilder>();
            }
        }
    }
}