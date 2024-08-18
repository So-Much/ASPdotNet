namespace BackEnd.Configs.Enviroments
{
    public class DatabaseConnection
    {
        public static string GetConnectionDatabaseString() {
            DotNetEnv.Env.Load("./Configs/Enviroments/.env");
            return DotNetEnv.Env.GetString("DEFAULT_CONNECTION_STRING", "DefaultConnectionString is not exists!!!");
        }
    }
}
