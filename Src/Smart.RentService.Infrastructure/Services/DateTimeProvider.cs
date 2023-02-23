using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart.RentService.Infrastructure.Interfaces;

namespace Smart.RentService.Infrastructure.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
