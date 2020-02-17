using System;
using System.Text;

using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities.RulesEngine
{ 
    public class LocalDataSource : BaseEntity, IDataSource
    {
        public LocalDataSource()
        { }

        public string GetMarkupID()
        {
            return "N";
        }

        public bool IsRemote()
        {
            return false;
        }
    }
}
