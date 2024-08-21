using BackEnd.Configs.Enviroments;
using System.Text;

namespace BackEnd.utils
{
    public class GenerateSystem
    {
        public static string GenerateUserId()
        {
            var env = new EnviromentVariables();
            var index = DateTime.Now.Ticks.ToString();
            var hasdedValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(index+env.GetSecretKey()));
            return $"user{hasdedValue}";
        }
    }
}
