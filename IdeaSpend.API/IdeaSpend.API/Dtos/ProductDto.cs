namespace IdeaSpend.API
{
    /// <summary>
    /// The simplify object of the <see cref="ProductEntity"/> with extra property of the <see cref="CatalogEntity"/>
    /// </summary>
    public class ProductDto
    {
        #region Product Entity Properties

        public string ProductName { get; set; }
        public string Seller { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }

        #endregion

        #region Catalog Entity Properties

        public string CatalogName { get; set; }

        #endregion
    }
}
