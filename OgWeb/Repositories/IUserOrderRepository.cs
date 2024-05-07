﻿namespace OgWeb.Repositories;

public interface IUserOrderRepository
{
    Task<IEnumerable<Order>> UserOrders(bool getAll = false);
    Task ChangeOrderStatus(UpdateOrderStatusModel data);

    bool GetPaymentStatusByKey(string key);
    Task TogglePaymentStatus(int orderId);
    Task<Order?> GetOrderById(int id);
    Task<IEnumerable<OrderStatus>> GetOrderStatuses();
}
