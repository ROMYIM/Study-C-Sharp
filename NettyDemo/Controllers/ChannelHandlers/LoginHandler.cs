using DotNetty.Transport.Channels;
using NettyDemo.Infrastructure.Db;
using NettyDemo.Infrastructure.Db.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NettyDemo.Controllers.ChannelHandlers
{
    public class LoginHandler : ChannelHandlerAdapter
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task ConnectAsync(IChannelHandlerContext context, EndPoint remoteAddress, EndPoint localAddress)
        {
            var endPoint = remoteAddress as IPEndPoint;
            var host = endPoint.ToString();
            var client = new Client
            {
                Host = host,
                LatestSigninTime = DateTimeOffset.Now
            };
            _dbContext.Add(client);
            await _dbContext.SaveChangesAsync();
            await base.ConnectAsync(context, remoteAddress, localAddress);
        }

        public override Task DisconnectAsync(IChannelHandlerContext context)
        {

            return base.DisconnectAsync(context);
        }
    }
}