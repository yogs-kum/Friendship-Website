using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Caching;

using Application.Presentation;
using Application.Common;

namespace Presentation
{
    public class CacheMgr
    {
        private static Cache theCacheMgr = new Cache();

        public enum CacheKeys
        {
            Mst_Groups, Mst_Country, Mst_State, Mst_Organization, Mst_Location, Mst_Gender, Mst_EmailCategory, Mst_Race, Mst_MaritalStatus,
            Mst_ProfileSections, Mst_Music, Mst_MovieGenre, Mst_FoodPreference
        }

        public static void AddToCache(string theKey, object theObj)
        {
            theCacheMgr.Insert(theKey, theObj, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static object GetFromCache(string theKey)
        {
            object theObj = theCacheMgr.Get(theKey);
            if (theObj == null)
            {
                DataSet theCacheDS = RefreshCache(theKey);
                theCacheDS.Tables[0].TableName = theKey;
                AddToCache(theKey, theCacheDS.Tables[0]);
                theObj = theCacheDS.Tables[0];
            }
            return theObj;
        }

        private static DataSet RefreshCache(string theKey)
        {
            string theParameter = string.Format("?theKey={0}", theKey);
            string theResult = ObjectFactory.GetStringAsync("Administration/SystemUtility/RefreshSystemCache", theParameter, "SystemUtilityController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            return theDS;
        }

        public static void ClearCache()
        {
            List<string> theCacheList = new List<string>();
            System.Collections.IDictionaryEnumerator theEnumerator = theCacheMgr.GetEnumerator();
            while (theEnumerator.MoveNext())
            {
                theCacheList.Add(theEnumerator.Key.ToString());
            }
            for (int i = 0; i < theCacheList.Count; i++)
            {
                theCacheMgr.Remove(theCacheList[i]);
            }
        }
    }
}