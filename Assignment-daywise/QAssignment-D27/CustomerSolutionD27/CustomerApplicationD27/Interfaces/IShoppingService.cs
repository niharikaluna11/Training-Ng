using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApplicationD27.Interfaces
{
    internal interface IShoppingService
    {
        //1) View Previous Order(all orders sorted by date in desc)
        //   2) View Order summary take order number from user
        //   - Order number, Customer name, Products ordered - 
        //   3) View shipper details for given order number
        void ViewPreviousOrders(string customerId);
        void ViewOrderSummary(string customerId);
        void ViewShipperDetails(string customerId);

    }
}
