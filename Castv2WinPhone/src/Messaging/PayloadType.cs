using ProtoBuf;

namespace Castv2WinPhone.Messaging
{
    [ProtoContract]
    public enum PayloadType
    {
        [ProtoEnum(Name = "STRING", Value = 0)]
        String,
        [ProtoEnum(Name = "BINARY", Value = 1)]
        Binary
    }
}
