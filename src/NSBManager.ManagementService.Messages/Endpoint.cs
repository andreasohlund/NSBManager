using System;

namespace NSBManager.ManagementService.Messages
{
    [Serializable]
    public class Endpoint
    {
        public string Id { get; set; }
        public string ServerName { get; set; }
    }
}