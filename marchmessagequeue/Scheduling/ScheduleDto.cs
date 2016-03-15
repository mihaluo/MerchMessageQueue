using System;
using System.Collections.Generic;

namespace MarchMessageQueue.Scheduling
{
    public class ScheduleDto
    {
        public Type MessageType { get; set; }

        public int MaxRetryTimes { get; set; }

        public Dictionary<int,int> ScheduleSceondsDictionary { get; set; }

        public TimeSpan GetScheduleTimeSpan(int retryTimes)
        {
            if (retryTimes > MaxRetryTimes)
            {
                throw new Exception("已超过最大重试次数");
            }

            return TimeSpan.FromSeconds(ScheduleSceondsDictionary[retryTimes]);
        }
    }
}