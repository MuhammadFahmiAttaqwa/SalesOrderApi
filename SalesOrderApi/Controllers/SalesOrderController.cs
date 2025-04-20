using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SalesOrderApi.Common;
using SalesOrderApi.DTO.SaleOrder.Request;
using SalesOrderApi.Service;
using System.Security.Cryptography.Xml;

namespace SalesOrderApi.Controllers
{
    [ApiController]
    [Route("SO/API")]
    [EnableCors("AllowFrontend")]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;
        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery]SearchRequest request)
        {
            try
            {
                var response = await _salesOrderService.Search(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erorr");
            }
        }
        [HttpGet]
        [Route("Export")]
        public async Task<IActionResult> ExportExcel([FromQuery]SearchRequest request)
        {
            try
            {
                var response = await _salesOrderService.ExportExcel(request);
                var fileBytes = Convert.FromBase64String(response);

                return Ok(response);
            }
            catch(Exception ex) 
            {
            
            }
            return Ok();
        }

        [HttpPost]
        [Route("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] AddOrUpdateRequest request, string or)
        {
            ApiResult<string> response = new ApiResult<string>();
            try
            {
                response.Msg = await _salesOrderService.AddOrUpdate(request, or);
                response.Code = 200;
                response.Data = null;
                return Ok(response);
            } catch (Exception ex)
            {
                response.Code = 500;
                return StatusCode(response.Code, "Fail Save Data");
            }
        }

        [HttpGet]
        [Route("GetCustomer")]
        public async Task<IActionResult> GetCustomer()
        {
            var response = await _salesOrderService.GetCustomer();
            return Ok(response);
        }
        [HttpGet]
        [Route("GetData")]
        public async Task<IActionResult> GetData([FromQuery] GetItemRequest request)
        {
            try
            {
                var result = await _salesOrderService.GetData(request);
                return Ok(result);
            } catch(Exception ex)
            {
                return StatusCode(500, "ERORR");
            }
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteData([FromBody]long orderId)
        {

            var result = await _salesOrderService.DeleteData(orderId);
            return Ok(result);
        }
    }
}
