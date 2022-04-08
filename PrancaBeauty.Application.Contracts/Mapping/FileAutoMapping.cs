using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Files;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class FileAutoMapping:Profile
    {
        public FileAutoMapping()
        {
            CreateMap<viCompo_ModalFileList, InpGetFileListForFileManager>();
            CreateMap<outGetFileDetailsForFileSelector, vmCompo_FileSelector>();
            CreateMap<OutGetFileListForFileManager, vmCompo_ModalFileList>();
        }
    }
}
