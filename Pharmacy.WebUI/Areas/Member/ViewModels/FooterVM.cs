﻿using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Areas.Member.ViewModels
{
    public class FooterVM
    {
        public ICollection<Category> Categories { get; set; }
        public ICollection<FAQCategory> FAQCategories { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Brand> Brands { get; set; }
        public ICollection<FooterE> FooterEs { get; set; }
    }
}