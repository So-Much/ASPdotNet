namespace BackEnd.Configs.Enviroments
{
    public class EnviromentVariables
    {
        public EnviromentVariables()
        {
            DotNetEnv.Env.Load("./Configs/Enviroments/.env");
        }
        public string GetConnectionString()
        {
            return DotNetEnv.Env.GetString("DEFAULT_CONNECTION_STRING", "DefaultConnectionString is not exists!!!");
        }
        public string GetSecretKey()
        {
            return DotNetEnv.Env.GetString("SECRET_KEY", "SecretKey is not exists!!!");
        }
        public string GetJwtKey()
        {
            return DotNetEnv.Env.GetString("JWT_KEY", "JwtKey is not exists!!!");
        }
    }
}
