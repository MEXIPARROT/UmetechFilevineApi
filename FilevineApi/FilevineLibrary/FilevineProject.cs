using Filevine.PublicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary
{
    public class FilevineProject
    {
        public static async Task<Project> GetProject(FilevineSetting setting, int projID)
        {
            try
            {
                var id = new Identifier();
                id.Native = projID;

                using (Connection conn = new Connection(setting))
                {
                    FilevineSDK.Projects projects = new FilevineSDK.Projects();
                    var proj = await projects.GetProjectAsync(conn.Token, id);

                    Console.WriteLine(proj.ProjectId.Native);

                    return proj;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new Project();

        }
        public static async Task<List<Project>> GetProjects(FilevineSetting setting)
        {
            var projs = new List<Project>();

            try
            {

                using (Connection conn = new Connection(setting))
                {
                    Console.WriteLine("made it");

                    FilevineSDK.Projects projects = new FilevineSDK.Projects();
                    var initalProjs = await projects.GetAllProjectsAsync(conn.Token);

                    foreach (var initalProj in initalProjs.Items)
                    {
                        projs.Add(initalProj);
                    }
                }
            }
            catch
            {
                Console.WriteLine("catch");
            }

            return projs;
        }
    }
}
