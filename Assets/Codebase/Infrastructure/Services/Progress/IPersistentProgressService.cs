using Codebase.Data;

namespace Codebase.Infrastructure.Services.Progress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}