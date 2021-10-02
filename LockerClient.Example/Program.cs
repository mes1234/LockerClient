using System;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using LockerClient;

namespace LockerExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterLockerExtension();
            builder.RegisterType<Worker>();

            var worker = builder.Build().Resolve<Worker>();

            await worker.Start();


        }
    }

    public class Worker
    {
        private readonly ILocker _locker;

        public Worker(ILocker locker)
        {
            _locker = locker;
        }

        public async Task Start()
        {
            await _locker.AuthorizeAsync("witek", "test123");
            var lockerId = await _locker.AddLockerAsync();

            _locker.UseLocker(lockerId);

            _locker["yello"] = Encoding.UTF8.GetBytes("yello");

            var data = _locker["yello"];
        }
    }
}
