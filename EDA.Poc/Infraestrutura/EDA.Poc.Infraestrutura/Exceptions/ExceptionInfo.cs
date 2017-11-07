using System;
using System.Collections;
using System.Collections.Generic;

namespace EDA.Poc.Infraestrutura.Exceptions
{
    [Serializable]
    public class ExceptionInfo
    {
        public Dictionary<string, string> ExceptionMetadata { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string HelpLink { get; set; }
        public string Source { get; set; }
        public int HResult { get; set; }
        public DateTime Date { get; set; }
        public IDictionary Data { get; set; }
        public List<ExceptionInfo> InnerExceptionsInfos { get; set; }        

        public ExceptionInfo(Exception exception)
        {
            InnerExceptionsInfos = new List<ExceptionInfo>();

            ExceptionMetadata = new Dictionary<string, string>
                                    {
                                        {"TypeName", exception.GetType().Name},
                                        {"TypeFullName", exception.GetType().FullName},
                                        {"Namespace", exception.GetType().Namespace},
                                        {"AssemblyFullName", exception.GetType().Assembly.FullName},
                                        {"TargetSiteName", exception.TargetSite.Name}
                                    };

            Message = exception.Message;
            StackTrace = exception.StackTrace;
            HelpLink = exception.HelpLink;
            Source = exception.Source;
            HResult = exception.HResult;
            Date = DateTime.Now;
            Data = exception.Data;

            if (exception.InnerException != null)
                InnerExceptionsInfos.Add(new ExceptionInfo(exception.InnerException));

            var aggregateException = exception as AggregateException;

            if(aggregateException != null)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    InnerExceptionsInfos.Add(new ExceptionInfo(innerException));
                }
            }
        }
    }
}
