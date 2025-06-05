namespace ECommerceITI.Application.DTOs.Settings
{
    public class RedisSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string InstanceName { get; set; }
        public TimeSpan TimoutHS { get; set; }
    }
}
