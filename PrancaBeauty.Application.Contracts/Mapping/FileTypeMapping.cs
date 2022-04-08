using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.FileTypes;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class FileTypeMapping : Profile
    {
        public FileTypeMapping()
        {
            CreateMap<outGetFileTypeListForCombo, vmCompo_ComboFileTypes>();

        }
    }
}
