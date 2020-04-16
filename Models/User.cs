using System.Runtime.Serialization;

namespace ProtobufVsMsgPack.Models
{
    [DataContract]
    public class User
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public long NationalCode { get; set; }

        [DataMember(Order = 4)]
        public int Age { get; set; }

        [DataMember(Order = 5)]
        public Address Address { get; set; }

        [DataMember(Order = 6)]
        public Photo Photo { get; set; }
    }
}