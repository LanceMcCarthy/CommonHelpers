using System.Runtime.Serialization;

namespace CommonHelpers.Tests.TestHelpers
{
    [DataContract]
    public sealed class TestEmployee
    {
        [DataMember(Name ="name")]
        public string Name { get; set; }

        [DataMember(Name = "salary")]
        public double Salary { get; set; }

        [DataMember(Name = "married")]
        public bool Married { get; set; }
    }
}
