using System;
using System.Collections.Generic;
using System.Text;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IExecutionReport
    {
        DateTime GetStartTime();

        DateTime GetEndTime();
    }
}
