using SalesOrderApi.Common;
using SalesOrderApi.DTO.SaleOrder.Request;
using SalesOrderApi.DTO.SaleOrder.Response;
using SalesOrderApi.Entity;

namespace SalesOrderApi.Service
{
    public interface ISalesOrderService
    {
        Task<PagingResponse<SearchResponse>> Search(SearchRequest request);

        Task<string> ExportExcel(SearchRequest request);

        Task<string> AddOrUpdate (AddOrUpdateRequest request, string or);

        Task<List<GetCustomer>> GetCustomer();

        Task<PagingResponse<SoItemResponse>> GetItemPaging(GetItemRequest request);

        Task<SoOrderResponse> GetData (GetItemRequest request);

        Task<string> DeleteData(long orderId);
    }
}
