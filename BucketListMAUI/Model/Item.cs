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

    [Column("creation_dt")]
    public DateTime CreationDate { get; set; }

    [ForeignKey(typeof(UserList))]
    [Column("parentid")]
    public int ParentId { get; set; }

    [Column("iscompleted")]
    public bool IsCompleted { get; set; }


    public Item()
    {
        CreationDate = DateTime.Now;
    }

    public Item(Item item)
    {
        this.Name = item.Name;
        this.Description = item.Description;
        this.Category = item.Category;
        this.ParentId = item.ParentId;
        this.CreationDate = item.CreationDate;

    }
    public Item(string itemName)
    {
        this.Name = itemName;
        this.CreationDate = DateTime.Now;
    }

}


