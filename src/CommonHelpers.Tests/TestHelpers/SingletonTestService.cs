using System;

namespace CommonHelpers.Tests.TestHelpers;

public class SingletonTestService
{
    public long InstanceId { get; } = DateTime.Now.Ticks;
}