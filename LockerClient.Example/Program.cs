﻿using System;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using LockerClient;
using Serilog;

namespace LockerExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterLockerExtension();
            builder.RegisterType<Worker>();
            builder.RegisterInstance<ILogger>(new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger());

            var worker = builder.Build().Resolve<Worker>();

            await worker.Start();


        }
    }

    public class Worker
    {
        private readonly ILogger _logger;
        private readonly ILocker _locker;

        public Worker(
            ILogger logger,
            ILocker locker)
        {
            _logger = logger;
            _locker = locker;
        }

        public async Task Start()
        {
            await _locker.AuthorizeAsync("witek", "test123");
            var lockerId = await _locker.AddLockerAsync();

            //var lockerId = Guid.Parse("7ff9bf42-cb8c-4100-a900-4a8bad90638d");

            _logger.Information("Attached to {lockerId}", lockerId);

            _locker.UseLocker(lockerId);

            _locker["yello"] = Encoding.UTF8.GetBytes("witek jest mistrz nad mistrze nikt tego nie moze podwazyc");

            _logger.Information("Got {data}, for {key}", Encoding.UTF8.GetString(_locker["yello"]), "yello");
        }
    }
}
