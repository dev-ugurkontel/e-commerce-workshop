using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Utils;
using Core.Validation;
using Core.Utils.Results;
using DataAccess.EF.Abstract;
using DataAccess.EF.Concrete;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CampaignService : ICampaignService
    {
        private readonly CampaignRepositoryBase _campaignRepository;

        public CampaignService(CampaignRepositoryBase campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        [ValidationAspect(typeof(CampaignValidator))]
        public IDataResult<CampaignResponse> Add(CampaignRequest data)
        {
            var entity = new Campaign()
            {
                CampaignName = data.CampaignName,
                CampaignCode = data.CampaignCode,
                CampaignDiscountRate = data.CampaignDiscountRate,
                CampaignStatus = data.CampaignStatus,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                EndDate = data.EndDate,
                StartDate = data.StartDate
            };

            _campaignRepository.Add(entity);

            var campaignResponse = new CampaignResponse()
            {
                CampaignId = entity.CampaignId,
                CampaignName = entity.CampaignName,
                CampaignCode = entity.CampaignCode,
                CampaignDiscountRate = entity.CampaignDiscountRate,
                CampaignStatus = entity.CampaignStatus,
                CreateDate = entity.CreateDate,
                EditDate = entity.EditDate,
                EndDate = entity.EndDate,
                StartDate = entity.StartDate
            };

            return new SuccessDataResult<CampaignResponse>(campaignResponse, "Kampanya kaydedildi.");
        }

        public IResult Delete(int id)
        {
            var campaign = _campaignRepository.Get(p => p.CampaignId == id);
            _campaignRepository.Delete(campaign);
            return new SuccessResult("Kampanya Silindi.");
        }

        public IDataResult<CampaignResponse> Get(int id)
        {
            var campaign = _campaignRepository.Get(c => c.CampaignId == id);
            if (campaign is null)
            {
                return new ErrorDataResult<CampaignResponse>(default, "Descriptive error message here.");
            }

            CampaignResponse campaignResponse = new()
            {
                CampaignId = campaign.CampaignId,
                CampaignName = campaign.CampaignName,
                CampaignCode = campaign.CampaignCode,
                CampaignDiscountRate = campaign.CampaignDiscountRate,
                CampaignStatus = campaign.CampaignStatus,
                CreateDate = campaign.CreateDate,
                EditDate = campaign.EditDate,
                EndDate = campaign.EndDate,
                StartDate = campaign.StartDate
            };

            return new SuccessDataResult<CampaignResponse>(campaignResponse, "Kampanya bilgisi getirildi.");
        }

        public IDataResult<List<CampaignResponse>> GetAll()
        {
            var campaignList = _campaignRepository.GetAll().Select(c => new CampaignResponse()
            {
                CampaignId = c.CampaignId,
                CampaignName = c.CampaignName,
                CampaignCode = c.CampaignCode,
                CampaignDiscountRate = c.CampaignDiscountRate,
                CampaignStatus = c.CampaignStatus,
                CreateDate = c.CreateDate,
                EditDate = c.EditDate,
                EndDate = c.EndDate,
                StartDate = c.StartDate
            }).ToList();

            return new SuccessDataResult<List<CampaignResponse>>(campaignList, "Kampanya bilgileri getirildi.");
        }
        [ValidationAspect(typeof(CampaignValidator))]
        public IResult Update(int id, CampaignRequest data)
        {
            var campaign = _campaignRepository.Get(p => p.CampaignId == id);
            campaign.CampaignName = data.CampaignName;
            campaign.CampaignCode = data.CampaignCode;
            campaign.CampaignDiscountRate = data.CampaignDiscountRate;
            campaign.CampaignStatus = data.CampaignStatus;
            campaign.EditDate = DateTime.Now;
            campaign.StartDate = data.StartDate;
            campaign.EndDate = data.EndDate;

            _campaignRepository.Update(campaign);
            return new SuccessResult("Kampanya bilgileri güncellendi.");
        }
    }
}
