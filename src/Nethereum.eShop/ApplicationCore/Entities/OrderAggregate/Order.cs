using Nethereum.eShop.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public int? QuoteId { get; set; }

        public OrderStatus Status { get; set; }

        /// <summary>
        /// The transaction hash for Purchase Order creation
        /// </summary>
        public string TransactionHash { get; set; }

        public string BuyerId { get; set; }

        /// <summary>
        /// The Buyer Address
        /// </summary>
        public string BuyerAddress { get; set; }

        public string CurrencyAddress { get; set; }

        public string CurrencySymbol { get; set; }

        public string ApproverAddress { get; set; }

        /// <summary>
        /// The Purhase Order Number
        /// </summary>
        public long? PoNumber { get; set; }

        // TODO: Change to enum when it is ready
        /*
         * enum PoType
    {
        Initial,                // 0  expect never to see this
        Cash,                   // 1  PO is paid up front by the buyer
        Invoice                 // 2  PO is paid later after buyer receives an invoice
    }
         */
        public int PoType { get; set; }

        /// <summary>
        /// eShop constant - one wallet expected per shop
        /// </summary>
        public string BuyerWalletAddress { get; set; } // buyer wallet address

        /// <summary>
        /// eShop Id - allow orders in the db to reference multiple shops
        /// </summary>
        public string SellerId { get; set; } 

        public DateTimeOffset? PoDate { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public PostalAddress BillTo { get; private set; }
        public PostalAddress ShipTo { get; private set; }

        // DDD Patterns comment
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method Order.AddOrderItem() which includes behavior.
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        // Using List<>.AsReadOnly() 
        // This will create a read only wrapper around the private list so is protected against "external updates".
        // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
        //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private Order()
        {
            // required by EF
        }

        public Order(string buyerId, string buyerAddress, PostalAddress billTo, PostalAddress shipTo, List<OrderItem> items)
        {
            Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
            Guard.Against.NullOrEmpty(buyerAddress, nameof(buyerAddress));
            // TODO: Reinforce null address guards
            // Guard.Against.Null(billTo, nameof(billTo));
            // Guard.Against.Null(shipTo, nameof(shipTo));
            Guard.Against.Null(items, nameof(items));

            BuyerId = buyerId;
            BuyerAddress = buyerAddress;
            ShipTo = shipTo;
            _orderItems = items;
        }

        public decimal Total()
        {
            var total = 0m;
            foreach (var item in _orderItems)
            {
                total += item.UnitPrice * item.Quantity;
            }
            return total;
        }

        public int ItemCount()
        {
            return _orderItems == null ? 0 : _orderItems.Count;
        }
    }
}
