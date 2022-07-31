using Real.Time.Chat.Shared.Kernel.Entity;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Time.Chat.MessageBus.Contracts
{
    public interface IChatApi
    {
        [Post("/users/receive")]
        Task<ApiOkReturn> DeliveryMessage([Body] MessageDto transaction);
    }
}
