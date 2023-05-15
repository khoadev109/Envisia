using System.Reflection;

namespace Envisia.Library.Helpers
{
    public static class PathHelper
    {
        public static void Ensure(string path)
        {
            if (File.Exists(path))
            {
                return;
            }

            var folder = Path.GetDirectoryName(path);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using FileStream file = File.Create(path);
        }

        public static string Get(string path)
        {
            var returnPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);

            return returnPath;
        }

        public static string GetProjectPath(string folder)
        {
            string projectPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            return Path.Combine(projectPath, folder);
        }
    }
}
