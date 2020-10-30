using Filevine.PublicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary
{
    public class FilevineCollection
    {
        public static async Task<CollectionItem> GetCollectionItem(FilevineSetting setting, ItemIdentifier id, Identifier projectID, string sectionSelector)
        {
            var collectionItem = new CollectionItem();

            try
            {
                using (Connection conn = new Connection(setting))
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                    var collectionOps = new FilevineSDK.ProjectCollections();
                    collectionItem = await collectionOps.GetProjectCollectionAsync(conn.Token, projectID, sectionSelector, id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return collectionItem;
        }

        public static async Task<ItemList<CollectionItem>> GetCollectionList(FilevineSetting setting, string projectID, string sectionSelector)
        {
            var collectionList = new ItemList<CollectionItem>();

            try
            {
                using (Connection conn = new Connection(setting))
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                    var collectionOps = new FilevineSDK.ProjectCollections();
                    collectionList = await collectionOps.GetAllProjectCollectionsAsync(conn.Token, projectID, sectionSelector);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return collectionList;
        }

        public static async Task<ItemList<CollectionItem>> GetCollectionList(FilevineSetting setting, Identifier projectID, string sectionSelector)
        {
            var collectionList = new ItemList<CollectionItem>();

            try
            {
                using (Connection conn = new Connection(setting))
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                    var collectionOps = new FilevineSDK.ProjectCollections();
                    collectionList = await collectionOps.GetAllProjectCollectionsAsync(conn.Token, projectID, sectionSelector);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine(collectionList.Items);////
            return collectionList;
        }

        public static async Task<CollectionItem> CreateCollectionItem(FilevineSetting setting, Identifier projectID, string sectionSelector, CollectionItem collectionItemData)
        {
            var collectionItem = new CollectionItem();

            try
            {
                using (Connection conn = new Connection(setting))
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                    var collectionOps = new FilevineSDK.ProjectCollections();
                    collectionItem = await collectionOps.CreateProjectCollectionAsync(conn.Token, projectID, sectionSelector, collectionItemData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return collectionItem;
        }

        public static async Task<CollectionItem> UpdateCollectionItem(FilevineSetting setting, Identifier projectID, string sectionSelector, CollectionItem collectionItemData)
        {
            var collectionItem = new CollectionItem();

            try
            {
                using (Connection conn = new Connection(setting))
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                    var collectionOps = new FilevineSDK.ProjectCollections();
                    collectionItem = await collectionOps.UpdateProjectCollectionAsync(conn.Token, projectID, sectionSelector, collectionItemData);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return collectionItem;
        }

        public static async Task<bool> RemoveCollectionItem(FilevineSetting setting, Identifier projectID, string sectionSelector, ItemIdentifier id)
        {
            try
            {
                using (Connection conn = new Connection(setting))
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                    var collectionOps = new FilevineSDK.ProjectCollections();
                    await collectionOps.DeleteProjectCollectionAsync(conn.Token, projectID, sectionSelector, id);

                    return true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;
        }
    }
}
