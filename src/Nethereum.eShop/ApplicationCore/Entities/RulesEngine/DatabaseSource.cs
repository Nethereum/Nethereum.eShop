using System;
using System.Text;

using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities.RulesEngine
{
    public class DatabaseSource : BaseEntity, IDataSource
    {
        public DatabaseSource()
        { }

        public string GetMarkupID()
        {
            return "N";
        }

        public bool IsRemote()
        {
            return true;
        }
    }
}
