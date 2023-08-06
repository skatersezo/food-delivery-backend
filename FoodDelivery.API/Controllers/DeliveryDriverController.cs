using Microsoft.AspNetCore.Mvc;
using Paramore.Darker;
using FoodDelivery.Core.Ports.Queries;


namespace FoodDelivery.API.Controllers
{
    [Route("drivers")]
    public class DeliveryDriverController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;

        public DeliveryDriverController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _queryProcessor.ExecuteAsync(new AllDeliveryDriversQuery());

            return Ok(drivers);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var driver = await _queryProcessor.ExecuteAsync(new DeliveryDriverByIdQuery(id));

            return Ok(driver);
        }

        
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

