using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stepan.Common.Service
{
    public interface IProductService
    {
        IEnumerable<ProductFinderEx> GetAllCategories();

        IEnumerable<ProductFinderEx> GetCategory(string category);
        
        IEnumerable<ProductFinderEx> GetAllBrandNames();
        
        IEnumerable<ProductFinderEx> GetBrandName(string brand);
       
        IEnumerable<ProductFinderEx> GetAllChemicalGroups();
        
        IEnumerable<ProductFinderEx> GetChemicalGroup(string chemGroup);
        
        IEnumerable<ProductFinderEx> GetAllChemicalNames();
        
        IEnumerable<ProductFinderEx> GetChemicalName(string name);
        
        IEnumerable<ProductFinderEx> GetSearchResult(string query);
        
        IEnumerable<SubCategoryEx> GetSubCategoryOfCategory(string category);
        
        IEnumerable<ProductFinderEx> GetDescriptionOfSubCategory(string brand);
        
        IEnumerable<ProductFinderEx> GetData(string parameter);
       

    }
}