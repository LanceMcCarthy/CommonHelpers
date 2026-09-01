using System;

namespace CommonHelpers.Tests.TestHelpers;

public class SingletonTestService
{
    public long InstanceId { get; set; } = DateTime.Now.Ticks;
}