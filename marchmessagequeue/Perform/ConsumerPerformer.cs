using System;
using System.Reflection;

namespace MarchMessageQueue.Perform
{
    public class ConsumerPerformer
    {
        public PerformedContext Perform(PerformContext performContext)
        {
            PerformedContext performedContext = new PerformedContext(performContext);
            try
            {
                object instance = Activator.CreateInstance(performContext.ConsumeType);

                MethodInfo methodInfo = performContext.ConsumeType.GetMethod("Consume");

                bool result = (bool)methodInfo.Invoke(instance, new[] { performContext.MessageObj });

                performedContext.Result = result;

            }
            catch (Exception e)
            {
                performedContext.Result = false;
                performedContext.Exception = e;
            }

            return performedContext;
        }
    }
}