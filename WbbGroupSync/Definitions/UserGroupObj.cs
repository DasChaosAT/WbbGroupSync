using System.Collections.Generic;

namespace WbbGroupSync.Definitions
{
    public class UserGroupObj
    {
        public UserGroupObj(int forumId, List<int> addGroupList, List<int> removeGroupList)
        {
            ForumId = forumId;
            AddGroupList = addGroupList;
            RemoveGroupList = removeGroupList;
        }

        public int ForumId { get;}
        public List<int> AddGroupList { get; set; }
        public List<int> RemoveGroupList { get; set; }
    }
}
