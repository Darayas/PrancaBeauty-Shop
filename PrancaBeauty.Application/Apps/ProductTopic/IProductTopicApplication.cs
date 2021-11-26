using PrancaBeauty.Application.Contracts.ProductTopics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductTopic
{
    public interface IProductTopicApplication
    {
        Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
    }
}