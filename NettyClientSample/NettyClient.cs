using System;
using DotNetty.Transport.Bootstrapping;

namespace NettyClientSample
{
    public class NettyClient
    {
        private Bootstrap _bootstrap;

        public Action<Bootstrap> ConfigBootstrap { get; set; }
    }
}