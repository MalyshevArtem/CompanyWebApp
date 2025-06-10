using DataAccess.Models;
using CompanyWebAppUI.Models;

namespace CompanyWebAppUI.ViewModels
{
    public class RewardPenaltyViewModel
    {
        private RewardPenalty _rewardPenalty;

        public RewardPenaltyViewModel(RewardPenalty rewardPenalty)
        {
            _rewardPenalty = rewardPenalty;
        }

        public RewardPenaltyModel GetRewardPenaltyModel()
        {
            return new RewardPenaltyModel()
            {
                Type = _rewardPenalty.Type,
                DocId = _rewardPenalty.DocId,
                DocDate = _rewardPenalty.DocDate != null ? _rewardPenalty.DocDate.Value.ToShortDateString() : null,
                Description = _rewardPenalty.Description,
                Details = _rewardPenalty.Details,
            };
        }
    }
}
