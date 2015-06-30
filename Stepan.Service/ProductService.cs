using Stepan.Common;
using Stepan.Common.Repository;
using Stepan.Common.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stepan.Service
{
    public class ProductService : IProductService
    {
        readonly IProductRepository repo;
        public ProductService(IProductRepository repo)
        {
            this.repo = repo;
        }
        public IEnumerable<ProductFinderEx> GetAllCategories()
        {
            return repo.GetAllCategories();
        }

        public IEnumerable<ProductFinderEx> GetCategory(string category)
        {
            return repo.GetCategory(category);
        }

        public IEnumerable<ProductFinderEx> GetAllBrandNames()
        {
            return repo.GetAllBrandNames();
        }

        public IEnumerable<ProductFinderEx> GetBrandName(string brand)
        {
            return repo.GetBrandName(brand);
        }

        public IEnumerable<ProductFinderEx> GetAllChemicalGroups()
        {
            return repo.GetAllChemicalGroups();
        }

        public IEnumerable<ProductFinderEx> GetChemicalGroup(string chemGroup)
        {
            return repo.GetChemicalGroup(chemGroup);
        }

        public IEnumerable<ProductFinderEx> GetAllChemicalNames()
        {
            return repo.GetAllChemicalNames();
        }

        public IEnumerable<ProductFinderEx> GetChemicalName(string name)
        {
            return repo.GetChemicalName(name);
        }

        public IEnumerable<ProductFinderEx> GetSearchResult(string query)
        {
            return repo.GetSearchResult(query);
        }

        public IEnumerable<SubCategoryEx> GetSubCategoryOfCategory(string category)
        {
            return repo.GetSubCategoryOfCategory(category);
        }

        public IEnumerable<ProductFinderEx> GetDescriptionOfSubCategory(string brand)
        {
            return repo.GetDescriptionOfSubCategory(brand);
        }

        public IEnumerable<ProductFinderEx> GetData(string parameter)
        {
            return repo.GetData(parameter);
        }
       

    }
}