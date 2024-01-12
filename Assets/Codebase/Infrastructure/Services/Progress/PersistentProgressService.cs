using Codebase.Data;

namespace Codebase.Infrastructure.Services.Progress
{
    class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; } = new PlayerProgress();
    }
}