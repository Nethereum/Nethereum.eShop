﻿using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Nethereum.eShop.ApplicationCore.Entities.BasketAggregate
{
    public class Basket : BaseEntity, IAggregateRoot
    {
        public static Basket CreateForDeletion(int basketId)
        {
            return new Basket { Id = basketId };
        }

        public string BuyerId { get; set; }

        /// <summary>
        /// The Ethereum Address
        /// </summary>
        public string BuyerAddress { get; set; }

        public PostalAddress BillTo { get; set; }

        public PostalAddress ShipTo { get; set; }

        public string TransactionHash { get; set; }

        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        public void AddItem(int catalogItemId, decimal unitPrice, int quantity = 1)
        {
            if (!Items.Any(i => i.CatalogItemId == catalogItemId))
            {
                _items.Add(new BasketItem()
                {
                    CatalogItemId = catalogItemId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
            existingItem.Quantity += quantity;
        }

        public void RemoveEmptyItems()
        {
            _items.RemoveAll(i => i.Quantity == 0);
        }
    }
}
