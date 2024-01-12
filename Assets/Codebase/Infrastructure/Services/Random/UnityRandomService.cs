namespace Codebase.Infrastructure.Services.Random
{
    class UnityRandomService : IRandomService
    {
        public int Range(int min, int max) => UnityEngine.Random.Range(min, max);
        public float Range(float min, float max) => UnityEngine.Random.Range(min, max);
    }
}