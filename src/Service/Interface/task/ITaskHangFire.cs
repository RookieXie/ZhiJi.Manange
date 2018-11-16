using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.task
{
    public interface ITaskHangFire
    {
        Task<int> CheckNoAnswerChatFourtyEightHours();
        Task<int> CheckChatEnd();
    }
}
