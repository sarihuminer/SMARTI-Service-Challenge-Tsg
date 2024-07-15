using Demo.Application.Interfaces;
using Demo.Domain.Model.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly IMergeEntitiesService _mergeEntitiesService;

        public EntityController(IMergeEntitiesService mergeEntitiesService)
        {
            _mergeEntitiesService = mergeEntitiesService;
        }

        [HttpPost("merge")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> MergeEntries([FromBody] List<Entity> entities)
        {
            try
            {
                var mergedEntity = _mergeEntitiesService.MergeEntities(entities);
                if (mergedEntity == null)
                    return BadRequest("Invalid entity type or priority settings.");

                return Ok(mergedEntity);
            }
            catch (Exception e)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while merging entities.");
            }
        }
    }
}
