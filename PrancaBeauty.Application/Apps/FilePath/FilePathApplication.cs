using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FilePath
{
    public class FilePathApplication : IFilePathApplication
    {
        private readonly ILogger _Logger;
        private readonly IFilePathRepository _FilePathRepository;

        public FilePathApplication(IFilePathRepository filePathRepository, ILogger logger)
        {
            _FilePathRepository = filePathRepository;
            _Logger = logger;
        }

        public async Task<string> GetIdByPathAsync(string FileServerId, string Path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FileServerId))
                    throw new ArgumentInvalidException($"'{nameof(FileServer)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(Path))
                    throw new ArgumentInvalidException($"'{nameof(Path)}' cannot be null or whitespace.");

                var qData = await _FilePathRepository.Get.Where(a => a.FileServerId == Guid.Parse(FileServerId))
                                                         .Where(a => a.Path == Path)
                                                         .Select(a => a.Id.ToString())
                                                         .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
