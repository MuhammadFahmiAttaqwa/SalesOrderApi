using SalesOrderApi.Common;
using SalesOrderApi.DTO.SaleOrder.Request;
using SalesOrderApi.DTO.SaleOrder.Response;
using SalesOrderApi.Entity;

namespace SalesOrderApi.Repository
{
    public interface ISalesOrderRepository
    {
        Task<PagingResponse<SearchResponse>> Search (SearchRequest request);

        Task<int> Count (SearchRequest request);

        Task<string> AddOrUpdate (SoOrder request, List<SoItem> requestItem, string or);

        Task<string> AddItem (List<SoItem> request, long soOrderId);

        Task<List<ComCustomer>> GetCustomer ();

        Task<SoOrder> GetSoOrderById(long OrderId);

        Task<List<SoItem>> GetItem(GetItemRequest OrderId);

        Task<int> TotalGetItem(long OrderId);

        Task<string> DeleteData(long OrderId);



    }
}
