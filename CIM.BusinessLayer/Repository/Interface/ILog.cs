﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repository.Interface
{
    public interface ILog
    {
        void WriteLog(Exception ex,string controller,string method);
    }
}
