using Stepan.Common;
using Stepan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Stepan.Repository.Models
{
    public class ProductRepository : IProductRepository
    {
        StepanEktron_oldEntities db = new StepanEktron_oldEntities();

        public IEnumerable<ProductFinderEx> GetAllCategories()
        {
            var _products = db.ProductFinders.ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetCategory(string category)
        {
            var _products = db.ProductFinders
                .Where(y => y.Category == category).ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetAllBrandNames()
        {
            var _products = db.ProductFinders.ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetBrandName(string brand)
        {
            var _products = db.ProductFinders
                .Where(y => y.Brand == brand).ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetAllChemicalGroups()
        {
            var _products = (from p in db.ProductFinders
                        join t in db.ProductTaxonomySummaries
                        on p.id equals t.item_id
                        select p).OrderBy(p => p.Name).ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetChemicalGroup(string chemGroup)
        {
            var _products = (from x in db.ProductFinders
                        join y in db.ProductTaxonomySummaries
                        on x.id equals y.item_id
                        where (y.chemGroups.Contains(chemGroup))
                        select x).OrderBy(x => x.Name).ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetAllChemicalNames()
        {
            var _products = (from a in db.ProductFinders
                        join b in db.ProductFinderLinks on a.id equals b.ProductID
                        join c in db.ProductFinderTaxonomies on
                        b.TaxonomyID equals c.id
                        select a).OrderBy(a => a.Name).ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetChemicalName(string name)
        {
            var _products = (from a in db.ProductFinders
                        join b in db.ProductFinderLinks on
                        a.id equals b.ProductID
                        join c in db.ProductFinderTaxonomies
                        on b.TaxonomyID equals c.id
                        where c.Name == name
                        select a).OrderBy(a => a.Name).ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetSearchResult(string query)
        {
            var _products = (from a in db.ProductFinders
                        where a.Name == query || a.Brand == query || a.Category == query || a.ChemicalDescription == query
                        select a).OrderBy(a => a.Name).ToList();
            return Transform(_products);
        }

        public IEnumerable<SubCategoryEx> GetSubCategoryOfCategory(string category)
        {
            var data = (from a in db.ProductFinders
                        where a.Category == category
                        select new SubCategoryEx {
                            Brand = a.Brand,
                        }).OrderBy(a => a.Brand).ToList();
            return data;
        }

        public IEnumerable<ProductFinderEx> GetDescriptionOfSubCategory(string brand)
        {
            var _products = (from a in db.ProductFinders
                        where a.Brand == brand
                        select a ).ToList();
            return Transform(_products);
        }

        public IEnumerable<ProductFinderEx> GetData(string parameter)
        {
            string markets = "";
            string chemicalGroups = "";
            string name = "";
            string[] dataa = parameter.Split('_');

            if (dataa.Count() == 1)
            {
                markets = dataa[0].ToString();
            }
            else if (dataa.Count() == 2)
            {
                markets = dataa[0].ToString();
                chemicalGroups = dataa[1].ToString();
            }
            else if (dataa.Count() == 3)
            {
                markets = dataa[0].ToString();
                chemicalGroups = dataa[1].ToString();
                name = dataa[2].ToString();
            }
            else
            {
                markets = dataa[0].ToString();
                chemicalGroups = dataa[1].ToString();
                name = dataa[2].ToString();
            }
          
            var _products = (from a in db.ProductFinders
                        join b in db.ProductTaxonomySummaries
                        on a.id equals b.item_id
                        where b.markets.StartsWith(markets) && b.markets.EndsWith(markets) || b.chemGroups.StartsWith(chemicalGroups) && b.chemGroups.EndsWith(chemicalGroups) || a.Name == name
                        select a
                        ).ToList();

            return Transform(_products);
        }

        private ProductFinderEx Transform(ProductFinder Product)
        {
            ProductFinderEx _product = new ProductFinderEx
            {
                AcidNumber = Product.AcidNumber,
                AcidValue = Product.AcidValue,
                ApproxTgC = Product.ApproxTgC,
                CloudPoint = Product.CloudPoint,
                CMC = Product.CMC,
                Density = Product.Density,
                DravesWetting = Product.DravesWetting,
                FlashPoint = Product.FlashPoint,
                FoamDensity = Product.FoamDensity,
                FormAt25C = Product.FormAt25C,
                FreeFattyAcid = Product.FreeFattyAcid,
                FreezePoint = Product.FreezePoint,
                HLB = Product.HLB,
                HydroxylValue = Product.HydroxylValue,
                InsulationValue = Product.InsulationValue,
                IntrafacialTension = Product.IntrafacialTension,
                Kosher = Product.Kosher,
                MolesOfEO = Product.MolesOfEO,
                MolesOfPO = Product.MolesOfPO,
                Name = Product.Name,
                OH_Functionality = Product.OH_Functionality,
                PercentActive = Product.PercentActive,
                PourPoint = Product.PourPoint,
                Solids = Product.Solids,
                SpecificGravity = Product.SpecificGravity,
                SurfaceTension = Product.SurfaceTension,
                ThermalStability = Product.ThermalStability,
                Triglycerides = Product.Triglycerides,
                ViscosityAt200C = Product.ViscosityAt200C,
                ViscosityAt25C = Product.ViscosityAt25C,
                ViscosityAtC = Product.ViscosityAtC,
                VOC = Product.VOC
            };

            return _product;
        }

        private IEnumerable<ProductFinderEx> Transform(List<ProductFinder> Products)
        {
            List<ProductFinderEx> _products =new List<ProductFinderEx>();

            foreach (ProductFinder product in Products)
            {
                _products.Add(Transform(product));
            }

            return _products;
        }
    }
}