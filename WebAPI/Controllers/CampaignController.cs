using Business.Abstract;
using Core.Utils;
using Entities.Entity;
using Entities.Surrogate.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        [Route("List_Campaign")]
        public IActionResult List_Campaign()
        {
            var result = _campaignService.GetAll();
            if (result.Status == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Route("Save_Campaign")]
        [HttpPost]
        public IActionResult Save_Category(CampaignRequest campaign)
        {
            _campaignService.Add(campaign);
            return Ok(campaign);
        }

        [Route("Find_Campaign/{id}")]
        [HttpGet]
        public IActionResult Find_Category(int id)
        {
            var campaign = _campaignService.Get(id);
            if (campaign.Status == ResultStatus.Error)
            {
                return NotFound();
            }
            else if (campaign.Status == ResultStatus.Success)
            {
                return Ok(campaign);
            }
            else
            {
                return BadRequest();
            }

        }

        [Route("Update_Campaign/{id}")]
        [HttpPut]
        public IActionResult Update_Category(int id, CampaignRequest campaign)
        {
            _campaignService.Update(id, campaign);
            return Ok(campaign);
        }

        [Route("Delete_Campaign/{id}")]
        [HttpDelete]
        public IActionResult Delete_Category(int id)
        {
            _campaignService.Delete(id);
            return NoContent();
        }
    }
}
