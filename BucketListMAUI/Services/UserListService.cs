﻿namespace BucketListMAUI.Services;
public class UserListService
{
    readonly DatabaseHandler _db = new();

    public UserListService()
    {
        _db = new DatabaseHandler();
    }

    public UserListService(DatabaseHandler db)
    {
        Guard.IsNotNull(db, nameof(db));

        _db = db;
    }

    public List<UserList> GetUserLists()
    {
        var returnLists = _db.GetAllQuery<UserList>();

        returnLists = returnLists.Where(x => x.ArchiveDate is null).ToList<UserList>();
        return returnLists;
    }

    public List<UserList> GetArchivedUserLists()
    {
        var returnLists = _db.GetAllQuery<UserList>();

        returnLists = returnLists.Where(x => x.ArchiveDate is not null).ToList<UserList>();
        return returnLists;
    }

    public List<UserList> GetUserListByName(string name)
    {
        Guard.IsNotNullOrEmpty(name, nameof(name));

        var returnLists = _db.GetQueryByName<UserList>(name);
        return returnLists;
    }

    public UserList GetUserListById(UserList ul)
    {
        Guard.IsNotNull(ul, nameof(ul));

        var returnLists = _db.GetQueryById<UserList>(ul.Id);
        return returnLists;
    }

    public UserList CreateUserList(UserList newlist)
    {
        Guard.IsNotNull(newlist, nameof(newlist));

        newlist.CreationDate = DateTime.Now;

        return _db.Insert(newlist);

    }

    public void ArchiveUserList(UserList delList)
    {
        Guard.IsNotNull(delList, nameof(delList));

        delList.ArchiveDate = DateTime.Now;

        _db.Delete<UserList>(delList);

    }

    public void UnarchiveUserList(UserList userList)
    {
        Guard.IsNotNull(userList);

        _db.Update<UserList>(userList);

    }

}


