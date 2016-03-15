using System;
using System.Collections.Generic;
using Hangfire;
using MarchMessageQueue.Common;
using MarchMessageQueue.Messages;
using MarchMessageQueue.Perform;
using MarchMessageQueue.Publisher;

namespace MarchMessageQueue.Scheduling
{
    public class RetrySchedule
    {
        public static void Schedule(PerformedContext performedContext)
        {
            try
            {
                if (performedContext.Result)
                {
                    return;
                }
                var retryMessage = new RetryMessage
                {
                    MessageType = performedContext.MessageType,
                    Message = performedContext.MessageObj.ToJson()
                };
                var scheduleDto = GetScheduleDto(performedContext.MessageType);
                TimeSpan scheduleTimeSpan = scheduleDto.GetScheduleTimeSpan(retryMessage.RetryTimes);

                BackgroundJob.Schedule(() => RePublish(retryMessage), scheduleTimeSpan);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Schedule(PerformedContext performedContext, RetryMessage retryMessage)
        {
            try
            {
                if (performedContext.Result)
                {
                    return;
                }

                var scheduleDto = GetScheduleDto(performedContext.MessageType);

                if (retryMessage.RetryTimes >= scheduleDto.MaxRetryTimes)
                {
                    //超过最大重试次数
                    return;
                }

                TimeSpan scheduleTimeSpan = scheduleDto.GetScheduleTimeSpan(retryMessage.RetryTimes);
                retryMessage.RetryTimes++;

                BackgroundJob.Schedule(() => RePublish(retryMessage), scheduleTimeSpan);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static ScheduleDto GetScheduleDto(Type messageType)
        {
            var scheduleDto = new ScheduleDto
            {
                MaxRetryTimes = 5,
                MessageType = messageType,
                ScheduleSceondsDictionary = new Dictionary<int, int>
                                            {
                                                {0,5 },
                                                {1,5 },
                                                {2,5 },
                                                {3,5 },
                                                {4,5 },
                                            }
            };

            return scheduleDto;
        }


        public static void RePublish(RetryMessage retryMessage)
        {
            IPublisher retryPublisher = new RetryPublisher();
            retryPublisher.Publish(retryMessage);
        }
    }

}