﻿using System;
using System.Text;
#if ASP_NET_CORE2
using Microsoft.AspNetCore.Hosting;
using IHostEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
#endif
#if ASP_NET_CORE3
using Microsoft.Extensions.Hosting;
#endif
using Microsoft.Extensions.DependencyInjection;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Web.DependencyInjection;

namespace NLog.Web.LayoutRenderers
{
    /// <summary>
    /// Rendering development environment. <see cref="IHostingEnvironment" />
    /// </summary>
    [LayoutRenderer("aspnet-environment")]
    [ThreadAgnostic]
    [ThreadSafe]
    public class AspNetEnvironmentLayoutRenderer : LayoutRenderer
    {
        private static IHostEnvironment _hostEnvironment;

        private static IHostEnvironment HostEnvironment => _hostEnvironment ?? (_hostEnvironment = ServiceLocator.ServiceProvider?.GetService<IHostEnvironment>());

        /// <summary>
        /// Append to target
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder" /> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(HostEnvironment?.EnvironmentName);
        }

        /// <inheritdoc />
        protected override void CloseLayoutRenderer()
        {
            _hostEnvironment = null;
            base.CloseLayoutRenderer();
        }
    }
}
