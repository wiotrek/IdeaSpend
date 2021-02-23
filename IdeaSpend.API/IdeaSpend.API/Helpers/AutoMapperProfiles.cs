using AutoMapper;

namespace IdeaSpend.API
{
    /// <summary>
    /// Documentation: https://docs.automapper.org/en/stable/index.html#
    /// More examples: https://docs.automapper.org/en/stable/Attribute-mapping.html
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        #region Constructor

        public AutoMapperProfiles()
        {
            CatalogForCatalogDto();
        }

        #endregion

        #region Private Members

        private void CatalogForCatalogDto()
        {
            CreateMap<CatalogEntity, CatalogDto>();
        }

        #endregion
    }
}