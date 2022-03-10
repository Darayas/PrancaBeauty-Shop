using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProdcutReviewsMedia
{
    public class OutGetAllReviewMedia
    {
        public string Id { get; set; }
        public string CommentId { get; set; }
        public string MimeType { get; set; }
        public string FileUrl { get; set; }
    }
}
