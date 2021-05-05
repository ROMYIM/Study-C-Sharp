using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetty.Codecs.Protobuf;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Libuv;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NettyDemo.ChannelHandlers;
using NettyDemo.Models.Dtos;
using NettyDemo.Infrastructure.Caches;
using NettyDemo.Infrastructure.Caches.Abbractions;
using NettyDemo.Infrastructure.Extensions;

namespace NettyDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IKeyValueCache<string, IChannel>, ChannelCache>();
            // services.AddTransient<LoginHandler>();
            services.AddTransient<PushMessageHandler>();
            services.AddNettyService((services, bootstrap) => 
            {
                /// <summary>
                /// 
                /// </summary>
                var dispatcher = new DispatcherEventLoopGroup();
                var bossGroup = dispatcher;
                var workGroup = new WorkerEventLoopGroup(dispatcher);
                var businessGroup = new EventLoopGroup();
                bootstrap.Group(bossGroup, workGroup);

                /// 具体个选项的作用：https://blog.csdn.net/zuixiaoyao_001/article/details/90198968
                bootstrap.Channel<TcpServerChannel>();
                bootstrap.Option(ChannelOption.SoBacklog, 128);  //  处理客户端连接的队列大小
                bootstrap.Option(ChannelOption.ConnectTimeout, TimeSpan.FromMilliseconds(500)); // 连接超时时间
                bootstrap.Option(ChannelOption.SoReuseaddr, true); // 允许重复使用本地地址和端口
                bootstrap.Option(ChannelOption.SoKeepalive, true); // 如果在两小时内没有数据的通信时，TCP会自动发送一个活动探测数据报文

                bootstrap.Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    var pipeline = channel.Pipeline;
                    pipeline.AddLast("ACCEPT-LOG", new LoggingHandler());
                    // pipeline.AddLast("ACCEPT-CONN", services.GetRequiredService<LoginHandler>());

                }));

                bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    var pipeline = channel.Pipeline;
                    pipeline.AddLast(new ProtobufVarint32FrameDecoder());
                    pipeline.AddLast(new ProtobufDecoder(Options.Parser));
                    pipeline.AddLast(new ProtobufVarint32LengthFieldPrepender());
                    pipeline.AddLast(new ProtobufEncoder());

                    pipeline.AddLast(businessGroup, services.GetRequiredService<PushMessageHandler>());
                }));
            });


            services.AddMemoryCache();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NettyDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NettyDemo v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
