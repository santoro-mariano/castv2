using ProtoBuf;

namespace Castv2WinPhone.Messaging
{
    [ProtoContract]
    public sealed class CastMessage : ICastMessageInfo
    {
        public CastMessage()
        { }

        public CastMessage(ICastMessageInfo message)
        {
            this.ProtocolVersion = message.ProtocolVersion;
            this.SourceId = message.SourceId;
            this.DestinationId = message.DestinationId;
            this.Namespace = message.Namespace;
            this.PayloadType = message.PayloadType;
        }

        [ProtoMember(1,IsRequired = true, Name = "protocol_version")]
        public ProtocolVersion ProtocolVersion { get; set; }

        [ProtoMember(2, IsRequired = true, Name = "source_id")]
        public string SourceId { get; set; }

        [ProtoMember(3, IsRequired = true, Name = "destination_id")]
        public string DestinationId { get; set; }

        [ProtoMember(4, IsRequired = true, Name = "namespace")]
        public string Namespace { get; set; }

        [ProtoMember(5, IsRequired = true, Name = "payload_type")]
        public PayloadType PayloadType { get; set; }

        [ProtoMember(6, IsRequired = false, Name = "payload_utf8")]
        public string PayloadUtf8 { get; set; }

        [ProtoMember(7, IsRequired = false, Name = "payload_binary")]
        public byte[] PayloadBinary { get; set; }
    }
}
