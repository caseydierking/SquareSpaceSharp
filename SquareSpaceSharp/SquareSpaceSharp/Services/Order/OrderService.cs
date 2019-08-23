﻿using System.Net.Http;
using System.Threading.Tasks;
using SquareSpaceSharp.Entities;
using SquareSpaceSharp.Extensions;
using SquareSpaceSharp.Infrastructure;

namespace SquareSpaceSharp.Services.Order
{
    public class OrderService : SquareSpaceService
    {
        /// <summary>
        /// Creates a new instance of <see cref="OrderService" />.
        /// </summary>
        /// <param name="secretApiKey">App Secret Api Key</param>
        public OrderService(string secretApiKey) : base(secretApiKey)
        {
        }

        /// <summary>
        /// Returns collection of orders
        /// </summary>
        /// <param name="queryParameters">Order query parameters</param>
        public virtual async Task<OrderCollection> GetOrdersAsync(OrderQueryParameters queryParameters = null)
        {
            var req = PrepareRequest("orders");

            if (queryParameters != null)
            {
                req.QueryParams.AddRange(queryParameters.ToParameters());
            }

            return await ExecuteRequestAsync<OrderCollection>(req, HttpMethod.Get);
        }

        /// <summary>
        /// Returns a order with provided ID.
        /// </summary>
        /// <param name="orderId">Requested order ID</param>
        /// <returns>The <see cref="Order"/>.</returns>
        public virtual async Task<Entities.Order> GetOrderAsync(string orderId)
        {
            var req = PrepareRequest($"orders/{orderId}");

            return await ExecuteRequestAsync<Entities.Order>(req, HttpMethod.Get);
        }


        /// <summary>
        /// Post a fulfillment of an order
        /// </summary>
        /// <param name="orderId">Requested order ID, specifies the order to update.</param>
        ///  /// <param name="fulfillmentParameter">Requested order ID</param>
        /// <returns>The <see cref="Order"/>.</returns>
        public virtual async Task<Entities.Order> UpdateFulfillmentAsync(string orderId, OrderFulfillment fulfillmentParameter)
        {
            var req = PrepareRequest($"orders/{orderId}/fulfillments");
            HttpContent content = null;

            if (fulfillmentParameter != null)
            {
                var body = fulfillmentParameter.ToDictionary();
                content = new JsonContent(body);
            }

            return await ExecuteRequestAsync<Entities.Order>(req, HttpMethod.Post, content);
        }
    }
}