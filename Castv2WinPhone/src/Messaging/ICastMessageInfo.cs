namespace Castv2WinPhone.Messaging
{
    public interface ICastMessageInfo
    {
        ProtocolVersion ProtocolVersion { get; }

        string SourceId { get; }

        string DestinationId { get; }

        string Namespace { get; }

        PayloadType PayloadType { get; }
    }
}
