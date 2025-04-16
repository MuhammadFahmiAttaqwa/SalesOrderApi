using Microsoft.IdentityModel.Tokens;
using SalesOrderApi.Common;
using SalesOrderApi.DTO.SaleOrder.Request;
using SalesOrderApi.DTO.SaleOrder.Response;
using SalesOrderApi.Entity;
using SalesOrderApi.Helpers;
using SalesOrderApi.Repository;

namespace SalesOrderApi.Service.Impl
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        public SalesOrderService(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task<string> AddOrUpdate(AddOrUpdateRequest request, string or)
        {
            try
            {
                var soOrder = new SoOrder
                {
                    ORDER_NO = request.OrderNo,
                    ORDER_DATE = request.OrderDate,
                    ADDRESS = request.Address,
                    SO_ORDER_ID = (long)request.OrderId,
                    COM_CUSTOMER_ID = request.CustomerId,

                };

                var Item = request.Items.Select(x => new SoItem
                {
                    ITEM_NAME = x.ItemName,
                    QUANTITY = (int)x.Quantity,
                    PRICE = (double)x.Price,
                    SO_ORDER_ID = (int)x.Price
                }).ToList();

                await _salesOrderRepository.AddOrUpdate(soOrder, Item, or);
                return "SUCCESS";
            } 
            catch(Exception ex)
            {
                return "Fail Save Data";
            }
        }

        public Task<string> DeleteData(long orderId)
        {
            try
            {
                 return _salesOrderRepository.DeleteData(orderId);
            }
            catch(Exception ex)
            {
                throw new Exception("ERORR", ex);
            }
        }

        public async Task<string> ExportExcel(SearchRequest request)
        {
            try
            {
                var result = await _salesOrderRepository.Search(request);
                var response = ExcelHelper.ExportExcel(result.Data);
                return response;
            }
            catch (Exception ex) 
            {
                return "Erorr";
            }
        }

        public async Task<List<GetCustomer>> GetCustomer()
        {
            var result = await _salesOrderRepository.GetCustomer();
            var response = result.Select(x => new GetCustomer
            {
                CustomerId = x.COM_CUSTOMER_ID,
                CustomerName = x.CUSTOMER_NAME
            }).ToList();
            return response;
            
        }

        public async Task<SoOrderResponse> GetData(GetItemRequest request)
        {
            try
            {
                var item = await GetItemPaging(request);

                SoOrderResponse response = new SoOrderResponse();

                var soOrder = await _salesOrderRepository.GetSoOrderById(request.OrderId);
                response = new SoOrderResponse
                {
                    SoOrderId = soOrder.SO_ORDER_ID,
                    OrderNo = soOrder.ORDER_NO,
                    OrderDate = soOrder.ORDER_DATE,
                    CustomerId = soOrder.COM_CUSTOMER_ID,
                    Address = soOrder.ADDRESS,
                    Items = new SoItemResponsePaging
                    {
                        PagingResponse = item
                    }

                };
                return response;
            }catch (Exception e)
            {
                throw new Exception("error");
            }
        }

        public async Task<PagingResponse<SoItemResponse>> GetItemPaging(GetItemRequest request)
        {
            var data = await  _salesOrderRepository.GetItem(request);
            var soItem = data.Select(c => new SoItemResponse
            {
                ItemName = c.ITEM_NAME,
                Price = c.PRICE,
                Quantity = c.QUANTITY,
                Total = c.PRICE*c.QUANTITY,

            }).ToList();

            var total = await _salesOrderRepository.TotalGetItem(request.OrderId);

            var result = new PagingResponse<SoItemResponse>(request.CurrentPage, request.PageSize, total, soItem);

            return result;
        }

        public Task<PagingResponse<SearchResponse>> Search(SearchRequest request)
        {
            try
            {
                var response = _salesOrderRepository.Search(request);
                return response;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
