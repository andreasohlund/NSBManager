using System;

namespace NSBManager.ManagementService.FailedMessages
{
    public class FailedMessage
    {
        public string Id { get; set; }

        public string Origin { get; set; }

        public string AddressOfFailedMessageStore { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as FailedMessage;

            if(other == null)
                return false;
            
            return other.Id.Equals(Id);
        }


        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}