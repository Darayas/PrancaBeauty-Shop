﻿using Framework.Domain;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Entities;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.FileAgg.Entities
{
    public class tblFiles : IEntity
    {
        public Guid Id { get; set; }
        public Guid FileServerId { get; set; }
        public Guid FileTypeId { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public long SizeOnDisk { get; set; }
        public DateTime Date { get; set; }
        public bool IsPrivate { get; set; }


        public virtual tblFileServers tblFileServer { get; set; }
        public virtual tblUsers tblUser { get; set; }
        public virtual tblProductMedia tblProductMedia { get; set; }
        public virtual tblProductReviewsMedia tblProductReviewsMedia { get; set; }
        public virtual tblFileTypes tblFileTypes { get; set; }
        public virtual ICollection<tblCountries> tblCountries { get; set; }
        public virtual ICollection<tblCategoris> tblCategoris { get; set; }
        public virtual ICollection<tblProductTopic> tblProductTopic { get; set; }
        public virtual ICollection<tblSeller_Translates> tblSeller_Translates { get; set; }
        public virtual ICollection<tblUsers> tblUserProfile { get; set; }

    }
}
