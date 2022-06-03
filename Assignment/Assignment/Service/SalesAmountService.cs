using System.Linq;

namespace Assignment.Service
{
    public class SalesAmountService
    {
        private NorthwindContext context;
        public SalesAmountService(NorthwindContext northwindContext)
        {
            context = northwindContext;
        }

        public  IQueryable<SalesAmountDTO> GetSalesAmount()
        {
            try
            {
                var salesData = from OrderData in context.Orders
                             join OrderDetailsData in context.OrderDetails
                             on OrderData.OrderId equals OrderDetailsData.OrderId
                             
                             group new { OrderData, OrderDetailsData } by new {
                                 
                                 OrderData.ShipCountry,
                                
                             }into EmpData
                             orderby EmpData.Key.ShipCountry
                                select (new SalesAmountDTO()
                             {
                                 
                                 country = EmpData.Key.ShipCountry,
                                 SalesAmount = EmpData.Sum(SalesData=> SalesData.OrderDetailsData.UnitPrice * SalesData.OrderDetailsData.Quantity)
                             });
                             
                                  
                return salesData.AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
