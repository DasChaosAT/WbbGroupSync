using System;
using System.Collections.Generic;

namespace WbbGroupSync.Definitions
{
    using GroupList = List<int>;
    using HumanLookupTuple = Tuple<int, int, List<int>>;
    using HumanLookupTable = List<Tuple<int, int, List<int>>>;

    internal class GroupHumanLookupTable
    {
        internal static readonly HumanLookupTable FactionTableHuman = new HumanLookupTable
        {
            //Description:
            //new HumanLookupTuple(factionId, rank, GroupList)
            //factionId = is the Id of the faction
            //rank      = is the rank which gets the group associations
            //GroupList = ForumGroups Ids which should be assigned
            //
            //Example:
            //new HumanLookupTuple(1, 2, new GroupList {4, 5, 6})
            new HumanLookupTuple(1, 2, new GroupList {30, 29, 72}),
            new HumanLookupTuple(1, 3, new GroupList {78, 56, 50}),
        };

        internal static readonly HumanLookupTable GangTableHuman = new HumanLookupTable
        {
            //Description:
            //new HumanLookupTuple(factionId, rank, GroupList)
            //factionId = is the Id of the faction
            //rank      = is the rank which gets the group associations
            //GroupList = ForumGroups Ids which should be assigned
            //
            //Example:
            //new HumanLookupTuple(1, 2, new GroupList {4, 5, 6})
        };

        internal static readonly HumanLookupTable CompanyTableHuman = new HumanLookupTable
        {
            //Description:
            //new HumanLookupTuple(factionId, rank, GroupList)
            //factionId = is the Id of the faction
            //rank      = is the rank which gets the group associations
            //GroupList = ForumGroups Ids which should be assigned
            //
            //Example:
            //new HumanLookupTuple(1, 2, new GroupList {4, 5, 6})
        };
    }
}
