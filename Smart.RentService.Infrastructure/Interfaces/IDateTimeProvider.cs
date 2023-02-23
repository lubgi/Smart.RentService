using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.RentService.Infrastructure.Interfaces
{
    public interface IDateTimeProvider
    {
        public DateTimeOffset Now { get; }
    }
}
