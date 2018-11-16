using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Service.Interface.task;
using WebMVC.Filter;

namespace WebMVC.Controllers
{
    [NoFilter]
    public class TaskInitController : Controller
    {
        private ITaskHangFire taskHangFire;
        public TaskInitController( ITaskHangFire taskHangFire)
        {
            this.taskHangFire = taskHangFire;
        }
        public string Index(int minuteInterval = 5)
        {
            RecurringJob.AddOrUpdate(() => taskHangFire.CheckNoAnswerChatFourtyEightHours(), Cron.MinuteInterval(minuteInterval));
            RecurringJob.AddOrUpdate(() => taskHangFire.CheckChatEnd(), Cron.MinuteInterval(minuteInterval));
            return "1";
        }
    }
}