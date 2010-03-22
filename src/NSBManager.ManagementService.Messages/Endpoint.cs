using System;

namespace NSBManager.ManagementService.Messages
{
    [Serializable]
    public class Endpoint
    {
        public string Id { get; set; }
        public string ServerName { get; set; }
        public string AdressOfFailedMessageStore { get; set; }

        public EndpointStatus Status { get; set; }

        public string Adress { get; set; }
    }

    public enum EndpointStatus
    {
        Unknown,
        Running
    }
}