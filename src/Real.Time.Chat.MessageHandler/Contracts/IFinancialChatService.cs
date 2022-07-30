﻿using Real.Time.Chat.Shared.Kernel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Real.Time.Chat.MessageHandler.Contracts
{
    public interface IChatService
    {
        IChatApi CreateApi();
    }
}