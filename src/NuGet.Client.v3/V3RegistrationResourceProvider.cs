﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Data;

namespace NuGet.Client
{
    [NuGetResourceProviderMetadata(typeof(V3RegistrationResource), "V3RegistrationResource", NuGetResourceProviderPositions.Last)]
    public class V3RegistrationResourceProvider : INuGetResourceProvider
    {
        public V3RegistrationResourceProvider()
        {
        }

        public async Task<Tuple<bool, INuGetResource>> TryCreate(SourceRepository source, CancellationToken token)
        {
            V3RegistrationResource resource = null;
            var serviceIndex = await source.GetResourceAsync<V3ServiceIndexResource>(token);
            if (serviceIndex != null)
            {
                var messageHandlerResource = await source.GetResourceAsync<HttpHandlerResource>(token);

                DataClient client = new DataClient(messageHandlerResource.MessageHandler);

                IEnumerable<Uri> templateUrls = serviceIndex[ServiceTypes.Registrations];
                if (templateUrls != null && templateUrls.Any())
                {
                    resource = new V3RegistrationResource(client, templateUrls);
                }
            }

            return new Tuple<bool, INuGetResource>(resource != null, resource);
        }
    }
}