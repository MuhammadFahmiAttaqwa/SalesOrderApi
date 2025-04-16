using Dapper;
using Microsoft.EntityFrameworkCore;
using SalesOrderApi.Common;
using SalesOrderApi.DTO.SaleOrder.Request;
using SalesOrderApi.DTO.SaleOrder.Response;
using SalesOrderApi.Entity;

namespace SalesOrderApi.Repository.Impl
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly AppDbContext _context;
        public SalesOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddItem(List<SoItem> request, long soOrderId)
        {
            try
            {                
                foreach(var item in request)
                {
                    item.SO_ORDER_ID = soOrderId;
                }
                await _context.SoItem.AddRangeAsync(request);
                await _context.SaveChangesAsync();

                return "SUCCESS ADD ITEM";
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<string> AddOrUpdate(SoOrder request, List<SoItem> requestItem, string or)
        {
            try
            {
                var existOrder = await _context.SoOrder.FirstOrDefaultAsync(x => x.SO_ORDER_ID == request.SO_ORDER_ID);
                var checkSoOrderNo = await _context.SoOrder.FirstOrDefaultAsync(x=> x.ORDER_NO == request.ORDER_NO);

                if (or.Equals(CommonConstant.Add))
                {
                    if (checkSoOrderNo != null) return "Order No Duplicate";

                    await _context.SoOrder.AddAsync(request);
                    await _context.SaveChangesAsync();

                    await AddItem(requestItem, request.SO_ORDER_ID);
                    return "ADD DATA SUCCESS";

                } 
                else
                {
                    await _context.SoItem.Where(x=> x.SO_ORDER_ID == request.SO_ORDER_ID).ExecuteDeleteAsync();
                    existOrder.ORDER_NO = request.ORDER_NO;
                    existOrder.ORDER_DATE = request.ORDER_DATE;
                    existOrder.ADDRESS = request.ADDRESS;
                    existOrder.COM_CUSTOMER_ID = request.COM_CUSTOMER_ID;
                    await _context.SaveChangesAsync();
                    await AddItem(requestItem, request.SO_ORDER_ID);

                    return "UPDATE SUCCESS";
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Count(SearchRequest request)
        {
            try
            {
                using (var context = _context.Database.GetDbConnection())
                {
                    string sql = @"SELECT COUNT(so.ORDER_NO)
                            FROM SO_ORDER so
                            INNER JOIN COM_CUSTOMER c ON so.COM_CUSTOMER_ID = c.COM_CUSTOMER_ID
                            WHERE 
                                ((@orderNo IS NULL OR @orderNo = '')
                                  OR (UPPER(so.ORDER_NO) LIKE '%' + UPPER(@orderNo) + '%')
                                  OR (UPPER(c.CUSTOMER_NAME) LIKE '%' + UPPER(@orderNo) + '%'))
                                AND (@orderDate IS NULL OR @orderDate = '' 
                                  OR CONVERT(VARCHAR, so.ORDER_DATE, 3) = @orderDate)";
                    var parameter = new
                    {
                        orderNo = request.OrderNo,
                        orderDate = request.OrderDate,
                    };
                    var totalCount = await context.ExecuteScalarAsync<int>(sql, parameter);

                    return totalCount;
                }
            }
            catch (Exception ex)
            { 
                return 0;
            }
        }

        public async Task<List<ComCustomer>> GetCustomer()
        {
            return await _context.ComCustomer.ToListAsync();
        }

        public async Task<List<SoItem>> GetItem(GetItemRequest request)
        {
            try
            {
                var result = await _context.SoItem.AsNoTracking().
                    Where(x => x.SO_ORDER_ID == request.OrderId).
                    Skip((request.CurrentPage - 1) * request.PageSize).
                    Take(request.PageSize).
                    ToListAsync();
                return result;

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> TotalGetItem(long orderId)
        {
            var result = _context.SoItem.AsNoTracking().
                Where(x => x.SO_ORDER_ID == orderId).Count();
            return result;
        }

        public async Task<SoOrder> GetSoOrderById(long OrderId)
        {
            try
            {
                var soOrder = await _context.SoOrder.FindAsync(OrderId);
                return soOrder;
            }
            catch(Exception ex) 
            {
                throw new Exception("Data no Found");
            }
            
        }

        public async Task<PagingResponse<SearchResponse>> Search(SearchRequest request)
        {
            try
            {
                using (var context = _context.Database.GetDbConnection())
                {
                    string sql = @"SELECT 
                            so.ORDER_NO as OrderNo, so.SO_ORDER_ID as OrderId,
                            CONVERT(VARCHAR, so.ORDER_DATE, 3) AS OrderDate,
                            c.CUSTOMER_NAME as CustomerName
                            FROM SO_ORDER so
                            INNER JOIN COM_CUSTOMER c ON so.COM_CUSTOMER_ID = c.COM_CUSTOMER_ID
                            WHERE (@orderNo IS NULL OR @orderNo = ''
                                    OR UPPER(so.ORDER_NO) LIKE '%' + UPPER(@orderNo) + '%'
                                    OR UPPER(c.CUSTOMER_NAME) LIKE '%' + UPPER(@orderNo) + '%')
                                AND (@orderDate IS NULL OR @orderDate = '' 
                                    OR CONVERT(VARCHAR, so.ORDER_DATE, 3) = @orderDate)
                            ORDER BY so.ORDER_NO ASC
                            OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
                    var parameter = new
                    {
                        orderNo = request.OrderNo,
                        orderDate = request.OrderDate,
                        offset = (request.CurrentPage - 1) * request.PageSize,
                        pageSize = request.PageSize,

                    };
                    var data = await context.QueryAsync<SearchResponse>(sql, parameter);
                    var count = await Count(request);
                    var result = new PagingResponse<SearchResponse>(request.CurrentPage, request.PageSize, count, data.ToList());

                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> DeleteData(long OrderId)
        {
            try
            {
                var result = _context.SoOrder.FirstOrDefault(x=> x.SO_ORDER_ID == OrderId);
                if (result != null)
                {
                    await _context.SoItem.Where(x => x.SO_ORDER_ID == OrderId).ExecuteDeleteAsync();
                    await _context.SoOrder.Where(x => x.SO_ORDER_ID == OrderId).ExecuteDeleteAsync();

                    return "Success";
                }
                else
                {
                    return "Data Tidak Ditemukan";
                }
            } catch (Exception e)
            {
                return "Delete Gagal";
            }
        }
    }
}
