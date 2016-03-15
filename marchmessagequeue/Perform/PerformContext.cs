using System;

namespace MarchMessageQueue.Perform
{
    public class PerformContext
    {
        public PerformContext(PerformContext performContext)
            : this(performContext.MessageType, performContext.ConsumeType, performContext.MessageObj)
        {

        }

        public PerformContext(Type messageType, Type consumeType, object messageObj)
        {
            this.MessageType = messageType;
            this.MessageObj = messageObj;
            this.ConsumeType = consumeType;

        }

        public Type MessageType { get; private set; }

        public Type ConsumeType { get; private set; }

        public object MessageObj { get; private set; }
    }
}