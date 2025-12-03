using eMoody.Client;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using System.Net;

namespace eMoody.Server.Biz
{
    public static class ProxyChecks
    {

        public static void AddLocalNetworks(this IList<IPNetwork> KnownNetworks) {
            // sanity checks
            if (KnownNetworks is null) { return; }

            // IPv4 private addresses
            KnownNetworks.Add(new IPNetwork(IPAddress.Parse("10.0.0.0"), 8));       // Private networks
            KnownNetworks.Add(new IPNetwork(IPAddress.Parse("172.16.0.0"), 12));    // Docker networks
            KnownNetworks.Add(new IPNetwork(IPAddress.Parse("192.168.0.0"), 16));   // Local networks
            KnownNetworks.Add(new IPNetwork(IPAddress.Parse("127.0.0.0"), 8));      // Loopback

            // IPv6 private addresses
            KnownNetworks.Add(new IPNetwork(IPAddress.IPv6Loopback, 128));          // ::1/128
            KnownNetworks.Add(new IPNetwork(IPAddress.Parse("fc00::"), 7));         // fc00::/7 (unique local)
            KnownNetworks.Add(new IPNetwork(IPAddress.Parse("fe80::"), 10));        // fe80::/10 (link-local)
        }




        public static bool IsInternalIP(this IPAddress TestIP) {
            // sanity check
            if (TestIP == null) { return false; }

            byte[] ipBytes = TestIP.GetAddressBytes();

            return (ipBytes.Length == 4 // IPv4 private ranges (RFC 1918)
                        && (ipBytes[0] == 10                                              // 10.0.0.0/8     (10.0.0.0 to 10.255.255.255)
                             || ipBytes[0] == 172 && ipBytes[1] >= 16 && ipBytes[1] <= 31 // 172.16.0.0/12  (172.16.0.0 to 172.31.255.255)
                             || ipBytes[0] == 192 && ipBytes[1] == 168                    // 192.168.0.0/16 (192.168.0.0 to 192.168.255.255)
                             || ipBytes[0] == 127                                         // Loopback       (127.0.0.0/8)
                           )
                   )
                || (ipBytes.Length == 16 // IPv6 private ranges
                        && (TestIP.Equals(IPAddress.IPv6Loopback)                   // ::1 (loopback)
                             || (ipBytes[0] & 0xfe) == 0xfc                         // fc00::/7 (unique local addresses)
                             || (ipBytes[0] == 0xfe && (ipBytes[1] & 0xc0) == 0x80) // fe80::/10 (link-local)
                           )
                   );
        }


        public static IApplicationBuilder UseOnlyLocalForwardedHeaders(this IApplicationBuilder builder) {
            // sanity checks
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Use(async (context, next) => {
                // Only allow forwarded headers from internal networks
                IPAddress clientIP = context?.Connection?.RemoteIpAddress;

                if (context != null && !clientIP.IsInternalIP()) {
                    // Remove untrusted forwarded headers
                    context.Request.Headers.Remove("X-Forwarded-For");
                    context.Request.Headers.Remove("X-Forwarded-Proto");
                    context.Request.Headers.Remove("X-Forwarded-Host");
                    context.Request.Headers.Remove("X-Forwarded-Prefix");  // note: not actually used at this time
                    context.Request.Headers.Remove("X-Forwarded-Port");    // note: not actually used at this time

                }
                await next();
            });

            return builder;
        }

    }
}
