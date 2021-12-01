using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_Input_Keywords_Input
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Similarity { get; set; }
        public bool IsDelete { get; set; }
    }
}
