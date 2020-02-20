using System;
using System.Collections.Generic;
using System.Text;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IDataSource
    {
        public string GetMarkupID();

        public bool IsRemote();
    }
}
