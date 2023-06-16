using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BucketListMAUI.Model;
[Table("Items")]
public class Item : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("category")]
    public string Category { get; set; }

    [ForeignKey(typeof(UserList))]
    [Column("parentid")]
    public int ParentId { get; set; }

    [Column("iscompleted")]
    public bool IsCompleted { get; set; }


    public Item()
    {
    }

    public Item(Item item)
    {
        this.Name = item.Name;
        this.Description = item.Description;
        this.Category = item.Category;
        this.ParentId = item.ParentId;

    }
    public Item(string itemName)
    {
        this.Name = itemName;
    }
}


