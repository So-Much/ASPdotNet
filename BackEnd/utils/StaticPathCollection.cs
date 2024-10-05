using BackEnd.Configs.Enviroments;

namespace BackEnd.utils
{
    public static class StaticPathCollection
    {
        private static readonly EnviromentVariables _EnviromentVariables = new EnviromentVariables();
        private static readonly string _UploadProjectPath = $"{Environment.CurrentDirectory}/uploads";
        private static readonly string _StaticPath = _EnviromentVariables.GetStaticPath();
        public static string GetUploadProjectPath() => _UploadProjectPath;
        public static string GetStaticPath() => _StaticPath;
        public static string GetStaticPath(string path) => $"{_StaticPath}/{path}";
        public static string GetUploadProjectPath(string path) => $"{_UploadProjectPath}/{path}";
    }
}
