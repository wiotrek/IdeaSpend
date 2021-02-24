using AutoMapper;

namespace IdeaSpend.API
{
    /// <summary>
    /// Documentation: https://docs.automapper.org/en/stable/index.html#
    /// Using example: https://docs.automapper.org/en/stable/Attribute-mapping.html
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        #region Constructor

        public AutoMapperProfiles()
        {
            CatalogForCatalogDto();
            ProductForProductDto();
        }

        #endregion

        #region Private Members

        private void CatalogForCatalogDto()
        {
            CreateMap<CatalogEntity, CatalogDto>();
        }

        private void ProductForProductDto()
        {
            CreateMap<ProductEntity, ProductDto>()
                .ForMember(
                    desc => desc.CatalogName,
                    opt => opt.MapFrom(src => src.Catalog.CatalogName));
        }

        #endregion
    }
}