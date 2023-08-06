using Microsoft.AspNetCore.Mvc;
using Paramore.Darker;
using Paramore.Brighter;
using FoodDelivery.Core.Ports.Queries;
using FoodDelivery.Core.Ports.Commands;
using FoodDelivery.Core.ViewModels;


namespace FoodDelivery.API.Controllers
{
    [Route("drivers")]
    public class DeliveryDriverController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAmACommandProcessor _commandProcessor;

        public DeliveryDriverController(IQueryProcessor queryProcessor, IAmACommandProcessor commandProcessor)
        {
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
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
        public async Task<IActionResult> Post([FromBody]AddDeliveryDriverRequest request)
        {
            var command = new AddDeliveryDriverCommand(request.Name);
            await _commandProcessor.SendAsync(command);

            return Ok();
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

