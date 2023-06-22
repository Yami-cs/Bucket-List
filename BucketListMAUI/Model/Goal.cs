
/* Это класс Item, он отвечает за представление задач в целях */

namespace BucketListMAUI.Model;

/* Название таблицы и аттрибуты для датабазы SQLite*/
[Table("Goals")]
public class Goal : ObservableObject
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


    public Goal()
    {
        CreationDate = DateTime.Now;
    }

    public Goal(Goal item)
    {
        this.Name = item.Name;
        this.Description = item.Description;
        this.Category = item.Category;
        this.ParentId = item.ParentId;
        this.CreationDate = item.CreationDate;

    }
    public Goal(string itemName)
    {
        this.Name = itemName;
        this.CreationDate = DateTime.Now;
    }

}


