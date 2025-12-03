using LinqToDB.Mapping;

namespace Bob.Core.Domain
{
    [Table("COLOR")]
    public sealed class Color
    {
        public Color() { }

        [PrimaryKey]
        [Column("COLORID")]
        public int Id { get; set; }

        [Column("COLORNAME")]
        public string ColorName { get; set; }

        public Color(int id, string color)
        {
            Id = id;
            ColorName = color;
        }
    }
}
