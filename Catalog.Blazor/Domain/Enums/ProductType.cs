namespace Catalog.Domain.Enums
{
    public enum ProductType
    {
        Organic,
        NonOrganic
    }

    public static class ProductTypeHelper
    {
        public static string GetTypeName(ProductType p) =>
            p switch
            {
                ProductType.Organic => "Orgânico",
                ProductType.NonOrganic => "Não Orgânico",
                _ => p.ToString(),
            };
    }
}