using Real.Time.Chat.Shared.Kernel.Entity;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Time.Chat.MessageHandler.Contracts
{
    public interface IChatApi
    {
        [Post("/user/receive")]
        Task<ApiOkReturn> DeliveryMessage([Body] MessageDto transaction);
    }
}
