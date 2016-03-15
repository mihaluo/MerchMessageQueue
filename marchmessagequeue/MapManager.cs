using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MarchMessageQueue.Common;
using MarchMessageQueue.Consumer;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue
{
    public class MapManager
    {
        private static readonly Dictionary<Type, Type> ConsumeMessageTypeMap = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, Type> MessageConsumeTypeMap = new Dictionary<Type, Type>();
        static MapManager()
        {
            Type[] consumeTypes = TypeHelper.GetTypesByInterfaceType(typeof(IConsume<>));
            foreach (var consumeType in consumeTypes)
            {
                MethodInfo methodInfo = consumeType.GetMethod("Consume");
                var parameterInfos = methodInfo.GetParameters();
                ParameterInfo messageParameterInfo = parameterInfos.FirstOrDefault();
                if (messageParameterInfo != null)
                {
                    ConsumeMessageTypeMap.Add(consumeType, messageParameterInfo.ParameterType);
                    MessageConsumeTypeMap.Add(messageParameterInfo.ParameterType, consumeType);
                }
            }
        }

        public static Type GetMessageTypeByConsumeType(Type consumeType)
        {
            return GetTypeByKey(ConsumeMessageTypeMap, consumeType);
        }

        public static Type GetConsumeTypeByMessageType(Type messageType)
        {
            return GetTypeByKey(MessageConsumeTypeMap, messageType);
        }

        private static Type GetTypeByKey(Dictionary<Type, Type> dictionary, Type key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return null;
        }

    }
}