using System;

namespace BucketListMAUI.Services;
public class ItemService
{
    readonly DatabaseHandler _db = new();

    public ItemService()
    {
        _db = new DatabaseHandler();
    }

    public ItemService(DatabaseHandler db)
    {
        Guard.IsNotNull(db, nameof(db));

        _db = db;
    }

    public List<Goal> GetAllItems()
    {
        var returnLists = _db.GetAllQuery<Goal>();
        return returnLists;
    }

    public List<Goal> GetItemByName(string name)
    {
        Guard.IsNotNullOrEmpty(name, nameof(name));

        var returnLists = _db.GetQueryByName<Goal>(name);
        return returnLists;
    }

    public Goal GetItemById(Goal item)
    {
        Guard.IsNotNull(item, nameof(item));

        var returnItem = _db.GetQueryById<Goal>(item.Id);
        return returnItem;
    }
    public List<Goal> GetUserListItems(UserList ul)
    {
        Guard.IsNotNull(ul, nameof(ul));

        var itemList = _db.GetQueryByParentId<Goal>(ul.Id);
        return itemList;
    }

    public List<Goal> GetItemByParentId(UserList ul)
    {
        Guard.IsNotNull(ul, nameof(ul));

        var returnLists = _db.GetQueryByParentId<Goal>(ul.Id);
        return returnLists;
    }

    public Goal CreateItem(Goal newItem)
    {
        Guard.IsNotNull(newItem, nameof(newItem));

        var item = _db.Insert(newItem);
        newItem.ParentId = item.Id;
        newItem.Id = item.Id;
        return newItem;

    }

    public void DeleteItem(Goal deletedItem)
    {
        Guard.IsNotNull(deletedItem, nameof(deletedItem));

        _db.Delete(deletedItem);
    }

    public void UpdateItem(Goal updatedItem)
    {
        Guard.IsNotNull(updatedItem, nameof(updatedItem));

        _db.Update(updatedItem);
    }
}


