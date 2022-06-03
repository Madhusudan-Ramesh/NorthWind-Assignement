using Assignment.Model;
using Assignment.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{

    [Authorize]
    [ApiController]
    public class SalesAmountController: ControllerBase
    {
        SalesAmountService service;

        private readonly IJWTManagementService JwtManager;


        public SalesAmountController(SalesAmountService salesService, IJWTManagementService JwtManager)
        {
            this.service = salesService;
            this.JwtManager = JwtManager;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/authenticate")]
        public IActionResult Authenticate(Users userData)
        {
            var token = JwtManager.Authenticate(userData);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }


        [Route("api/EmolyeesSalesData")]
        [HttpGet]
        public IActionResult GetSalesData()
        {
            try
            {
                var getSalesData =  service.GetSalesAmount();
                return Ok(getSalesData);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
