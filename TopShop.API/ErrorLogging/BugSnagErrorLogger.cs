﻿using Bugsnag;
using TopShop.Application.Logging;

namespace TopShop.API.ErrorLogging
{
    public class BugSnagErrorLogger : IErrorLogger
    {
        private readonly Bugsnag.IClient _bugsnag;

        public BugSnagErrorLogger(IClient bugsnag)
        {
            _bugsnag = bugsnag;
        }

        public void Log(AppError error)
        {
            //_bugsnag.Notify(error.Exception);

            _bugsnag.Notify(error.Exception, (report) => {
                report.Event.Metadata.Add("Error", new Dictionary<string, string> {
                    { "user", error.Username },
                    { "erroCode", error.ErrorId.ToString() },
        });
            });
        }
    }

}
