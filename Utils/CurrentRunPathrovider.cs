using System.Reflection;

namespace Utils
{
    public static class CurrentRunPathrovider
    {
        private static readonly string delimeter = "/";
        public static string GetScreenShotOutputFolderPath(string folderName)
        {
            var testAssemblyPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var absolutePath = testAssemblyPath.AbsolutePath;
            var solutionPath = absolutePath[..absolutePath.LastIndexOf(@"bin", StringComparison.Ordinal)];
            return new Uri(@solutionPath + folderName + delimeter).LocalPath;
            
        }
    }
}