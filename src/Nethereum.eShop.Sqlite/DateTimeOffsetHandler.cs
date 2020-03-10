using Dapper;
using System;
using System.Data;

namespace Nethereum.eShop.Sqlite
{
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public static readonly DateTimeOffsetHandler Instance = new DateTimeOffsetHandler();

        public override DateTimeOffset Parse(object value)
        {
            //2020-03-10 12:16:08.4610707+00:00
            return DateTimeOffset.TryParse((string)value, out DateTimeOffset date) ? date : DateTimeOffset.MinValue;
        }

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        {
            //2020-03-10 12:16:08.4610707+00:00
            //somewhat naive and optimistic approach!
            //we're not yet implementing datetimeoffset parameters in queries
            parameter.Value = value.ToString();
        }
    }
}
