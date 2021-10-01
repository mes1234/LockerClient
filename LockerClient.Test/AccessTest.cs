using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace LockerClient.Test
{
    public class AccessTest
    {
        [Fact]
        public async Task Authorization_Test()
        {
            ILocker locker = new Locker();

            await locker.AuthorizeAsync("witek", "test123");

            locker.Authorized.Should().BeTrue();
        }
        [Fact]
        public async Task AddLocker_Test()
        {
            ILocker locker = new Locker();

            await locker.AuthorizeAsync("witek", "test123");

            locker.Authorized.Should().BeTrue();

            var lockerId = await locker.AddLockerAsync();

            lockerId.Should().NotBeEmpty();
        }
    }
}
