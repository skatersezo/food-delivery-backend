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
        private readonly HttpContext _httpContext;
        private readonly LinkGenerator _linkGenerator;

        public DeliveryDriverController(
            IQueryProcessor queryProcessor,
            IAmACommandProcessor commandProcessor,
            HttpContext httpContext,
            LinkGenerator linkGenerator)
        {
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
            _httpContext = httpContext;
            _linkGenerator = linkGenerator;
        }

        
        [HttpGet(Name = "GetDrivers")]
        public async Task<IActionResult> Get()
        {
            var result = await _queryProcessor.ExecuteAsync(new AllDeliveryDriversQuery());

            foreach (var driver in result.DeliveryDriversViewModel) driver.Url = _linkGenerator.GetUriByName(_httpContext, "GetDriver", new { driver.Id });

            return Ok(result.DeliveryDriversViewModel);
        }

        
        [HttpGet("{id}", Name = "GetDriverByID")]
        public async Task<IActionResult> GetById(int id)
        {
            if (await _queryProcessor.ExecuteAsync(new DeliveryDriverByIdQuery(id)) is { } result)
            {
                result.DeliveryDriverViewModel.Url = _linkGenerator.GetUriByName(_httpContext, "GetDriver", new { result.DeliveryDriverViewModel.Id });
                return Ok(result.DeliveryDriverViewModel);
            }

            return NotFound();
        }

        
        [HttpPost(Name = "AddDriver")]
        public async Task<IActionResult> Post([FromBody]AddDeliveryDriverRequest request)
        {
            var command = new AddDeliveryDriverCommand(request.Name);
            await _commandProcessor.SendAsync(command);

            var result = await _queryProcessor.ExecuteAsync(new DeliveryDriverByIdQuery(command.DeliveryDriverId));
            result.DeliveryDriverViewModel.Url = _linkGenerator.GetUriByName(_httpContext, "GetDriver", new { command.DeliveryDriverId });

            return CreatedAtRoute("GetDriver", new { id = command.DeliveryDriverId }, result.DeliveryDriverViewModel);
        }

        
        [HttpDelete("{id}", Name = "DeleteDriver")]
        public void Put(int id, [FromBody]string value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

