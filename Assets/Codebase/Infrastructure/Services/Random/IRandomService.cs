namespace Codebase.Infrastructure.Services.Random
{
    public interface IRandomService : IService
    {
        int Range(int min, int max);
        float Range(float min, float max);
    }
}