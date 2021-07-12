﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Files
{
    public class OutGetFileInfo
    {
        public string FileServerId { get; set; }
        public string FileServerName { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public long SizeOnDisk { get; set; }
        public string MimeType { get; set; } // image/jpg, image/png , application/zip
        public DateTime Date { get; set; }
        public bool IsPrivate { get; set; }
    }
}
