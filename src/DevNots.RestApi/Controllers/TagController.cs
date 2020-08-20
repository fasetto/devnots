using System.Threading.Tasks;
using DevNots.Application.Contracts;
using DevNots.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevNots.RestApi.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService tagService;
        public TagController(TagService tagService)
        {
            this.tagService = tagService;
        }

        /// <summary>
        /// Create a tag
        /// </summary>
        /// <param name="tag">Tag object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest request)
        {
            var response = await tagService.CreateTagAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return StatusCode(201, new { id = response.Result });
        }

        /// <summary>
        /// Delete the tag
        /// </summary>
        /// <param name="request">DeleteTagRequest object</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTag([FromBody] DeleteTagRequest request)
        {
            var response = await tagService.DeleteTagAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return Ok(new { message = "Tag deleted." });
        }

        /// <summary>
        /// Get list of tags
        /// </summary>
        /// <param name="request">GetTagListRequest object</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTagList([FromBody] GetTagListRequest request)
        {
            var response = await tagService.GetTagListAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            return Ok(response.Result);
        }

    }
}
