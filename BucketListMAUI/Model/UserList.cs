
/* Это класс UserList, он отвечает за представление целей пользователя */
namespace BucketListMAUI.Model
{
    /* Название таблицы и аттрибуты для датабазы SQLite*/
    [Table("UserLists")]
    public class UserList: ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public double Percentage { get; set; }

        [Ignore]
        public Color Color { get ; set; }

        [Column("color")]
        public string ColorString
        {
            get => Color.ToHex();
            set { Color = Color.FromArgb(value);
                OnPropertyChanged(nameof(Color));
            }
        }

        [OneToMany]
        public List<Item> Items { get; set; }

        [Column("creation_dt")]
        public DateTime CreationDate { get; set; }

        [Column("archive_dt")]
        public DateTime? ArchiveDate { get; set; }

        public UserList()
        {
            Items = new();
            Color = Color.FromArgb("10FFFFFF");
        }

        public UserList(UserList ul)
        {
            this.Id = ul.Id;
            this.Name = ul.Name;
            this.Items = ul.Items;
            this.Percentage = ul.Percentage;
            this.Color= ul.Color;
        }

        public UserList(string name)
        {
            Name = name;
            Items = new();
        }
    }
}
