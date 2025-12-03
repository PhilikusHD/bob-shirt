using LinqToDB.Mapping;

namespace Bob.Core.Domain
{
    [Table("PRODUCT_TYPE")]
    public class ProductType
    {
        public ProductType() { }

        [PrimaryKey]
        [Column("TYPEID")]
        public int TypeId { get; set; }
        [Column("TYPENAME")]
        public string TypeName { get; set; }

        public ProductType(int typeId, string typeName)
        {
            TypeId = typeId;
            TypeName = typeName;
        }
    }
}
