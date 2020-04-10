using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Services
{
    public class HangfireService : Hangfire.JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public HangfireService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
