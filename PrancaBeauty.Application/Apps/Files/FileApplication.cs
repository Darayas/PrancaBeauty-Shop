using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Exceptions;
using PrancaBeauty.Domin.FileServer.FileAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Files
{
    public class FileApplication : IFileApplication
    {
        private readonly ILogger _Logger;
        private readonly IFileRepository _FileRepository;
        public FileApplication(IFileRepository fileRepository, ILogger logger)
        {
            _FileRepository = fileRepository;
            _Logger = logger;
        }

        public async Task<OperationResult> AddFileAsync(InpAddFile Input)
        {
            try
            {
                if (Input is null)
                    throw new ArgumentInvalidException("Input cant be null");

                tblFiles tFile = new tblFiles()
                {
                    Id = new Guid().SequentialGuid(),
                    Date = DateTime.Now,
                    FileServerId = Guid.Parse(Input.FileServerId),
                    UserId = Input.UserId != null ? Guid.Parse(Input.UserId) : null,
                    FileName = Input.FileName,
                    IsPrivate = Input.IsPrivate,
                    MimeType = Input.MimeType,
                    Path = Input.Path,
                    SizeOnDisk = Input.SizeOnDisk,
                    Title = Input.Title
                };

                await _FileRepository.AddAsync(tFile, default, true);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }


    }
}
