using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application.Services.Security.AntiShell
{
    public class AniShell : IAniShell
    {
        private readonly ILogger _Logger;
        public AniShell(ILogger logger)
        {
            _Logger = logger;
        }

        public async Task<bool> ValidationExtentionAsync(IFormFile _FormFile)
        {
            var _FileInfo = await GetRealExtentionAsync(_FormFile);
            if (_FileInfo.Item1 == null)
                return false;

            if (_FileInfo.Item1 == "png")
            {
                if (_FormFile.ContentType != _FileInfo.Item2)
                    return false;
            }
            else if (_FileInfo.Item1 == "jpg")
            {
                if (_FormFile.ContentType != _FileInfo.Item2 /*jpg*/ && _FormFile.ContentType != "image/jpeg")
                    return false;
            }
            else if (_FileInfo.Item1 == "gif")
            {
                if (_FormFile.ContentType != _FileInfo.Item2)
                    return false;
            }
            else if (_FileInfo.Item1 == "bmp")
            {
                if (_FormFile.ContentType != _FileInfo.Item2)
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<(string, string)> GetRealExtentionAsync(IFormFile _FormFile)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException(nameof(_FormFile));

                byte[] buffer = new byte[50];
                await _FormFile.OpenReadStream().ReadAsync(buffer, 0, 50);

                string hex = "";
                foreach (var item in buffer)
                {
                    hex += string.Format("{0:X}", item) + " ";
                }

                if (hex.StartsWith("89 50 4E 47 0D 0A 1A 0A"))
                {
                    return ("png", "image/png");
                }
                else if (hex.StartsWith("FF D8"))
                {
                    return ("jpg", "image/jpg");
                }
                else if (hex.StartsWith("47 49 46 38 37 61") || hex.StartsWith("47 49 46 38 39 61"))
                {
                    return ("gif", "image/gif");
                }
                else if (hex.StartsWith("42 4D"))
                {
                    return ("bmp", "image/bmp");
                }
                else if (hex.StartsWith("66 74 79 70 4D 53 4E 56") || hex.StartsWith("66 74 79 70 69 73 6F 6D"))
                {
                    return ("mp4", "video/mp4");
                }
                else if (hex.StartsWith("50 4B 03 04"))
                {
                    return ("zip", "application/zip");
                }
                else if (hex.StartsWith("49 44 33"))
                {
                    return ("mp3", "audio/mpeg");
                }
                else if (hex.StartsWith("52 61 72 21 1A 07 00") || hex.StartsWith("52 61 72 21 1A 07 01 00"))
                {
                    return ("rar", "application/rar");
                }
                else
                    throw new FileFormatException("File format not found.");
            }
            catch (FileFormatException ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
            catch (ArgumentInvalidException)
            {
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }
    }
}
