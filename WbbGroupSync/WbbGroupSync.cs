using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using WbbGroupSync.Definitions;
using WbbGroupSync.Functions;

namespace WbbGroupSync
{
    public class WbbGroupSync
    {
        //Maybe place this in Database
        private static List<UserGroupObj> FactionGroupList = new List<UserGroupObj>();
        private static List<UserGroupObj> GangGroupList = new List<UserGroupObj>();
        private static List<UserGroupObj> CompanyGroupList = new List<UserGroupObj>();

        private const string KEY = ""; //128-Character Key

        private const string REQUEST_URL = "https://example.com/WbbGroupSync.php"; //URL to PHP-File

        private const int TIMER_MINUTES = 1;

        private static readonly HttpClient Client = new HttpClient();

        private static readonly GroupLookup Lookup = new GroupLookup();

        private static Timer timer;
        private static bool onceTimeStart = false;

        public static bool StartWbbGroupSync()
        {
            if (onceTimeStart)
                return false;

            ConfigTimer();

            onceTimeStart = true;
            return true;
        }

        private static async Task<string> MakePostRequest(string requestUrl, List<UserGroupObj> factionGroupList, 
            List<UserGroupObj> gangGroupList, List<UserGroupObj> companyGroupList, string key)
        {
            var values = new Dictionary<string, string>
            {
                {"FactionGroupList", JsonConvert.SerializeObject(factionGroupList)},
                {"GangGroupList", JsonConvert.SerializeObject(gangGroupList)},
                {"CompanyGroupList", JsonConvert.SerializeObject(companyGroupList)},
                {"Key", key}
            };

            var content = new FormUrlEncodedContent(values);
            var response = await Client.PostAsync(requestUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }


        private static void ConfigTimer()
        {
            timer = new Timer(TIMER_MINUTES * 60 * 1000);
            timer.Elapsed += OnTimerEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimerEvent(object source, ElapsedEventArgs e)
        {
            var result = MakePostRequest(REQUEST_URL, FactionGroupList, GangGroupList, CompanyGroupList, KEY).Result;

            FactionGroupList = new List<UserGroupObj>();
            GangGroupList = new List<UserGroupObj>();
            CompanyGroupList = new List<UserGroupObj>();
        }

        public static void AddUserToFaction(int forumId, int factionId, int rank)
        {
            Lookup.GetFactionAddList(factionId, rank, out var addList);
            Lookup.GetFactionRemoveList(factionId, rank, out var removeList);

            UserGroupObj userObj = new UserGroupObj(forumId, addList, removeList);

            FactionGroupList.Remove(FactionGroupList.FirstOrDefault(x => x.ForumId == forumId));
            FactionGroupList.Add(userObj);

        }

        public static void AddUserToGang(int forumId, int gangId, int rank)
        {
            Lookup.GetGangAddList(gangId, rank, out var addList);
            Lookup.GetGangRemoveList(gangId, rank, out var removeList);

            UserGroupObj userObj = new UserGroupObj(forumId, addList, removeList);

            GangGroupList.Remove(GangGroupList.FirstOrDefault(x => x.ForumId == forumId));
            GangGroupList.Add(userObj);
        }

        public static void AddUserToCompany(int forumId, int companyId, int rank)
        {

            Lookup.GetCompanyAddList(companyId, rank, out var addList);
            Lookup.GetCompanyRemoveList(companyId, rank, out var removeList);

            UserGroupObj userObj = new UserGroupObj(forumId, addList, removeList);

            CompanyGroupList.Remove(CompanyGroupList.FirstOrDefault(x => x.ForumId == forumId));
            CompanyGroupList.Add(userObj);
        }

    }
}
