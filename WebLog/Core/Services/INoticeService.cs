﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.Services
{
    public interface INoticeService
    {
        void SendNotice(Advertisement advertisement);
    }
}