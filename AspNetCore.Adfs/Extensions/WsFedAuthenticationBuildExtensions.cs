using System;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Authentication
{
    public static class WsFedAuthenticationBuildExtensions
    {
        public static AuthenticationBuilder AddWsFed(this AuthenticationBuilder builder)
            => builder.AddWsFed(_ => { });

        public static AuthenticationBuilder AddWsFed(this AuthenticationBuilder builder, Action<WsFedOptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<WsFederationOptions>, ConfigureWsFedOptions>();
            builder.AddWsFederation();
            return builder;
        }

        private class ConfigureWsFedOptions : IConfigureNamedOptions<WsFederationOptions>
        {
            private readonly WsFedOptions _azureOptions;

            public ConfigureWsFedOptions(IOptions<WsFedOptions> azureOptions)
            {
                _azureOptions = azureOptions.Value;
            }

            public void Configure(string name, WsFederationOptions options)
            {
                options.Wtrealm = _azureOptions.Realm;
                options.MetadataAddress = _azureOptions.Metadata;
                options.UseTokenLifetime = true;
                options.CallbackPath = _azureOptions.CallbackPath;
                options.RequireHttpsMetadata = false;
                options.BackchannelHttpHandler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
            }

            public void Configure(WsFederationOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}
