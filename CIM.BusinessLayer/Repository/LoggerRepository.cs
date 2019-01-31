using BusinessLayer.Repository.Interface;
using System;

namespace CIM.BusinessLayer.Repository
{
    public class LoggerRepository : ILog
    {
        public void WriteLog(Exception ex, string controller, string method)
        {
            throw new NotImplementedException();
        }
    }
}
