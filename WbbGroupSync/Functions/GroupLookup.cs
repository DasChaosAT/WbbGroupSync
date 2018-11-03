using System.Collections.Generic;
using System.Linq;
using WbbGroupSync.Definitions;

namespace WbbGroupSync.Functions
{
    using EfficientLookupTable = Dictionary<int, Dictionary<int, List<int>>>;
    using RankTable = Dictionary<int, List<int>>;
    using GroupList = List<int>;

    internal class GroupLookup
    {
        //Maybe replace with own Lists from other classes
        private static readonly List<int> FactionIds = new List<int>
        {
            1, 2, 3, 4, 5
        };
        private static readonly List<int> GangIds = new List<int>
        {
            1, 2, 3, 4, 5
        };
        private static readonly List<int> CompanyIds = new List<int>
        {
            1, 2, 3, 4, 5
        };

        private readonly EfficientLookupTable factionTableEfficient = new EfficientLookupTable();
        private readonly EfficientLookupTable gangTableEfficient = new EfficientLookupTable();
        private readonly EfficientLookupTable companyTableEfficient = new EfficientLookupTable();

        public GroupLookup()
        {

            foreach (var factionId in FactionIds)
            {
                var selectedFactionTuples = GroupHumanLookupTable.FactionTableHuman.Where(x => x.Item1 == factionId);

                var factionTable = new RankTable();
                foreach (var factionTuple in selectedFactionTuples)
                {
                    factionTable.Add(factionTuple.Item2, factionTuple.Item3);
                }

                factionTableEfficient.Add(factionId, factionTable);
            }

            foreach (var gangId in GangIds)
            {
                var selectedGangTuples = GroupHumanLookupTable.GangTableHuman.Where(x => x.Item1 == gangId);

                var gangTable = new RankTable();
                foreach (var gangTuple in selectedGangTuples)
                {
                    gangTable.Add(gangTuple.Item2, gangTuple.Item3);
                }

                gangTableEfficient.Add(gangId, gangTable);
            }

            foreach (var companyId in CompanyIds)
            {
                var selectedCompanyTuples = GroupHumanLookupTable.CompanyTableHuman.Where(x => x.Item1 == companyId);

                var companyTable = new RankTable();
                foreach (var companyTuple in selectedCompanyTuples)
                {
                    companyTable.Add(companyTuple.Item2, companyTuple.Item3);
                }

                companyTableEfficient.Add(companyId, companyTable);
            }
        }

        #region FactionMethods
        public bool GetFactionAddList(int factionId, int rank, out List<int> factionAddList)
        {
            factionAddList = new GroupList();

            if (factionTableEfficient.ContainsKey(factionId) && factionTableEfficient[factionId].ContainsKey(rank))
            {
                factionAddList = factionTableEfficient[factionId][rank];
            }

            return factionAddList.Any();
        }

        public bool GetFactionRemoveList(int factionId, int rank, out List<int> factionRemoveList)
        {
            factionRemoveList = new GroupList();

            GetFactionAddList(factionId, rank, out var factionAddList);

            if (!factionTableEfficient.ContainsKey(factionId))
                return false;

            foreach (var factionTable in factionTableEfficient[factionId])
            {
                foreach (var groupId in factionTable.Value)
                {
                    if (factionAddList.Contains(groupId))
                        continue;

                    if (!factionRemoveList.Contains(groupId))
                    {
                        factionRemoveList.Add(groupId);
                    }
                }

            }

            return factionRemoveList.Any();
        }
        #endregion

        #region GangMethods
        public bool GetGangAddList(int gangId, int rank, out List<int> gangAddList)
        {
            gangAddList = new GroupList();

            if (gangTableEfficient.ContainsKey(gangId) && gangTableEfficient[gangId].ContainsKey(rank))
            {
                gangAddList = gangTableEfficient[gangId][rank];
            }

            return gangAddList.Any();
        }

        public bool GetGangRemoveList(int gangId, int rank, out List<int> gangRemoveList)
        {
            gangRemoveList = new GroupList();

            GetGangAddList(gangId, rank, out var gangAddList);

            if (!gangTableEfficient.ContainsKey(gangId))
                return false;

            foreach (var gangTable in gangTableEfficient[gangId])
            {
                foreach (var groupId in gangTable.Value)
                {
                    if (gangAddList.Contains(groupId))
                        continue;

                    if (!gangRemoveList.Contains(groupId))
                    {
                        gangRemoveList.Add(groupId);
                    }
                }

            }

            return gangRemoveList.Any();
        }
        #endregion

        #region CompanyMethods
        public bool GetCompanyAddList(int companyId, int rank, out List<int> companyAddList)
        {
            companyAddList = new GroupList();

            if (companyTableEfficient.ContainsKey(companyId) && companyTableEfficient[companyId].ContainsKey(rank))
            {
                companyAddList = companyTableEfficient[companyId][rank];
            }

            return companyAddList.Any();
        }

        public bool GetCompanyRemoveList(int companyId, int rank, out List<int> companyRemoveList)
        {
            companyRemoveList = new GroupList();

            GetCompanyAddList(companyId, rank, out var companyAddList);

            if (!companyTableEfficient.ContainsKey(companyId))
                return false;

            foreach (var companyTable in companyTableEfficient[companyId])
            {
                foreach (var groupId in companyTable.Value)
                {
                    if (companyAddList.Contains(groupId))
                        continue;

                    if (!companyRemoveList.Contains(groupId))
                    {
                        companyRemoveList.Add(groupId);
                    }
                }

            }

            return companyRemoveList.Any();
        }
        #endregion
    }
}
