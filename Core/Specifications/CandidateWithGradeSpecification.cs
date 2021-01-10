﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class CandidateWithGradeSpecification: BaseSpecification<Candidate>
    {
        public CandidateWithGradeSpecification()
        {
            AddInclude(x => x.Grade);
        }

        //public ProductsWithTypesAndBrandsSpecification()
        //{
        //    AddInclude(x => x.ProductType);
        //    AddInclude(x => x.ProductBrand);
        //}

        //public ProductsWithTypesAndBrandsSpecification(int id)
        //    : base(x => x.Id == id)
        //{
        //    AddInclude(x => x.ProductType);
        //    AddInclude(x => x.ProductBrand);
        //}
    }
}
