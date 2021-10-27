using System;

namespace CommonHelpers.Tests.TestHelpers
{
    public class SingletonTestService
    {
        public SingletonTestService()
        {
            InstanceId = DateTime.Now.Ticks;
        }

        public long InstanceId { get; set; }
    }
}
