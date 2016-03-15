using System;

namespace MarchMessageQueue.Messages
{
    
    public class RetryMessage : MessageBase
    {
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryTimes { get; set; }

        /// <summary>
        /// 下次调用时间
        /// </summary>
        public TimeSpan ScheduleTime { get; set; }

        public string Message { get; set; }

        public Type MessageType { get; set; }

    }
}