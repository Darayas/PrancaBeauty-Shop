using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductTopics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductTopic
{
    public interface IProductTopicApplication
    {
        Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
    }
}