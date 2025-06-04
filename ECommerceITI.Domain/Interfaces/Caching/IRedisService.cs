namespace ECommerceITI.Domain.Interfaces.Caching
{
    public interface IRedisService
    {
        T? GetData<T>(string Key);
        void SetData<T>(string Key, T Data);
    }
}
