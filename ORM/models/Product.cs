using ORM.orm;

namespace ORM.models
{
    public class Product : Orm
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        
        public string Description { get; set; } = "";

        public int Quantity { get; set; }

        static Product()
        {
            Int("products", "Id",
                (product) => (product as Product).Id,
                (product, value) => (product as Product).Id = value);
            
            String("products", "Title", (product) => (product as Product).Title,
                (product, value) => (product as Product).Title = value);
            
            String("products", "Description", (product) => (product as Product).Description,
                (product, value) => (product as Product).Description = value);
            
            Int("products", "Quantity", (product) => (product as Product).Quantity,
                (product, value) => (product as Product).Quantity = value);

            PrimaryKey("products", "Id");
        }

        protected override string TableName()
        {
            return "products";
        }
    }
}