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
        private readonly LinkGenerator _linkGenerator;

        public DeliveryDriverController(
            IQueryProcessor queryProcessor,
            IAmACommandProcessor commandProcessor,
            LinkGenerator linkGenerator)
        {
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
            _linkGenerator = linkGenerator;
        }


        
        [HttpGet(Name = "GetDrivers")]
        public async Task<IActionResult> GetDrivers()
        {
            var result = await _queryProcessor.ExecuteAsync(new AllDeliveryDriversQuery());

            foreach (var driver in result.DeliveryDriversViewModel) driver.Url = _linkGenerator.GetUriByName(HttpContext, "GetDriverById", new { driver.Id });

            return Ok(result.DeliveryDriversViewModel);
        }

        
        [HttpGet("{id}", Name = "GetDriverById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (await _queryProcessor.ExecuteAsync(new DeliveryDriverByIdQuery(id)) is { } result)
            {
                result.DeliveryDriverViewModel.Url = _linkGenerator.GetUriByName(HttpContext, "GetDriverById", new { result.DeliveryDriverViewModel.Id });
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
            var addedDriver = result.DeliveryDriverViewModel;
            addedDriver.Url = _linkGenerator.GetUriByName(HttpContext, "GetDriverById", new { addedDriver.Id });

            return CreatedAtRoute("GetDriverById", new { id = addedDriver.Id }, addedDriver);
        }

        
        [HttpDelete("{id}", Name = "DeleteDriverById")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _commandProcessor.SendAsync(new DeleteDeliveryDriverByIdCommand(id));

            return Ok();
        }

        
        [HttpDelete(Name = "DeleteAllDrivers")]
        public async Task<IActionResult> DeleteAll()
        {
            await _commandProcessor.SendAsync(new DeleteAllDeliveryDriversCommand());

            return Ok();
        }

        [HttpPatch("{id}", Name = "UpdateDriver")]
        public async Task<IActionResult> UpdateById(int id, [FromBody]UpdateDeliveryDriverRequest request)
        {
            var command = new UpdateDeliveryDriverByIdCommand(id, request.Name, request.Orders, request.Latitude, request.Longitude);
            await _commandProcessor.SendAsync(command);

            var result = await _queryProcessor.ExecuteAsync(new DeliveryDriverByIdQuery(id));
            var updatedDriver = result.DeliveryDriverViewModel;
            updatedDriver.Url = _linkGenerator.GetUriByName(HttpContext, "GetDriverById", new { updatedDriver.Id });

            return Ok(updatedDriver);
        }
    }
}

