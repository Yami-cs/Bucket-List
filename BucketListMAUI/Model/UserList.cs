using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketListMAUI.Model
{
    [Table("UserLists")]
    public class UserList: ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public int Percentage { get; set; }
        

        [OneToMany]
        public List<Item> Items { get; set; }

        public enum ListType
        {
            WeeklyGrocery,
            ForAParty,
            OutOfSnacks
        }

        [Column("creation_dt")]
        public DateTime CreationDate { get; set; }

        [Column("archive_dt")]
        public DateTime? ArchiveDate { get; set; }

        public UserList()
        {
            Items = new();
        }

        public UserList(UserList ul)
        {
            this.Id = ul.Id;
            this.Name = ul.Name;
            this.Items = ul.Items;
        }

        public UserList(string name)
        {
            Name = name;
            Items = new();
        }
    }
}
