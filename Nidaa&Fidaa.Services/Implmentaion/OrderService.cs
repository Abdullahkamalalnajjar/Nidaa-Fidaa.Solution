using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Services.Abstract;

public class OrderService : IOrderService
{
    private readonly IGenericRepository<Basket> _basketRepository;
    private readonly IGenericRepository<Order> _orderRepository;
    private readonly IGenericRepository<BasketItem> _basketItemRepository;
    private readonly IGenericRepository<ProductAddition> _productAdditionRepository;
    private readonly IGenericRepository<Customer> _customerRepository;
    private readonly IGenericRepository<OrderItem> _orderItemRepository;

    public OrderService(IGenericRepository<Basket> basketRepository,
                        IGenericRepository<Order> orderRepository,
                        IGenericRepository<BasketItem> basketItemRepository,
                        IGenericRepository<ProductAddition> productAdditionRepository,
                        IGenericRepository<Customer> customerRepository,
                        IGenericRepository<OrderItem> orderItemRepository)

    {
        _basketRepository=basketRepository;
        _orderRepository=orderRepository;
       _basketItemRepository=basketItemRepository;
        _productAdditionRepository=productAdditionRepository;
        _customerRepository=customerRepository;
        _orderItemRepository=orderItemRepository;
    }

    public async Task<Order> CreateOrderAsync(int customerId, string? location)
    {
        var checkCustomer = await _customerRepository.GetByIdAsync(customerId);
        if (checkCustomer == null) {
            return null;
        }
        // Get basket with items
        var basket = await _basketRepository.GetTableNoTracking()
                                            .Include(b => b.Items)
                                            .ThenInclude(bi => bi.Additions) // Include related ProductAdditions
                                            .FirstOrDefaultAsync(b => b.CustomerId==customerId);

        if ( basket==null||!basket.Items.Any() )
            throw new InvalidOperationException("Basket is empty.");

        var basketItems = basket.Items.ToList();

        // Create the order
        var order = new Order
        {
            Location=location,
            CustomerId=customerId,
            OrderItems=new List<OrderItem>(),
            TotalAmount=0,
            Status="Pending"
        };

        // Add order items from the basket
        foreach ( var basketItem in basketItems )
        {
            var orderItem = new OrderItem
            {
                ProductId=basketItem.ProductId,
                Quantity=basketItem.Quantity,
              //  UnitPrice=basketItem.UnitPrice,
                TotalPrice=basketItem.TotalPrice
            };

            order.OrderItems.Add(orderItem);
            order.TotalAmount+=orderItem.TotalPrice;
        }

        // Save the order
        await _orderRepository.AddAsync(order);

        // Delete related ProductAddition entries for each basket item before deleting the basket items
        foreach ( var basketItem in basketItems )
        {
      

            // Now delete the basket item
            await _basketItemRepository.DeleteAsync(basketItem);
        }

        return order;
    }


    public async Task<List<Order>> GetOrderByCostomerIdAsync(int customerId)
    {
        return await _orderRepository.GetTableNoTracking()
                                     .Where(o => o.CustomerId==customerId) 
                                     .Include(c=>c.Customer)
                                    .Include(o => o.OrderItems)           
                                    .ThenInclude(oi => oi.Product)          
                                     .ToListAsync();
    }


    public async Task<Order?> GetOrderByIdAsync(int? id)
    {
        return await _orderRepository.GetTableNoTracking()
                             .Include(o => o.Customer) 
                             .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Product) 
                             .FirstOrDefaultAsync(o => o.Id==id);
    }
}

