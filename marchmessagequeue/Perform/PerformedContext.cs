using System;

namespace MarchMessageQueue.Perform
{
    public class PerformedContext : PerformContext
    {
        public PerformedContext(PerformContext performContext)
            :base(performContext.MessageType, performContext.ConsumeType, performContext.MessageObj)
        {
            
        }

        public bool Result { get; set; }

        public Exception Exception { get; set; }

    }
}