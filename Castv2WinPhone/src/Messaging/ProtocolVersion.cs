using ProtoBuf;

namespace Castv2WinPhone.Messaging
{
    [ProtoContract]
    public enum ProtocolVersion
    {
        [ProtoEnum(Name = "CASTV2_1_0", Value = 0)]
        CastV210
    }
}
