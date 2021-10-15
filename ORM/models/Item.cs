using ORM.orm;

namespace ORM.models
{
    public class Item : Orm
    {
        const string TABLE_NAME = "items";

        public int ItemId { get; set; }

        public string Name { get; set; } = "";

        public int Quantity { get; set; }

        static Item()
        {
            Int(TABLE_NAME, "itemId",
                (item) => (item as Item).ItemId,
                (item, value) => (item as Item).ItemId = value);
            
            Int(TABLE_NAME, "quantity",
                (item) => (item as Item).Quantity,
                (item, value) => (item as Item).Quantity = value);
            
            String(TABLE_NAME, "name",
                (item) => (item as Item).Name,
                (item, value) => (item as Item).Name = value);

            PrimaryKey(TABLE_NAME, "itemId");
        }

        protected override string TableName()
        {
            return TABLE_NAME;
        }
    }
}