using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests.Entities
{
    [DataContract]
    public sealed class ApplicationInfo
    {
        [DataMember(Name = "appId")]
        public string Id { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "namespaces")]
        public Namespace[] SupportedNamespaces { get; set; }

        [DataMember(Name = "sessionId")]
        public string SessionId { get; set; }

        [DataMember(Name = "statusText")]
        public string StatusText { get; set; }

        [DataMember(Name = "transportId")]
        public string TransportId { get; set; }
    }
}
