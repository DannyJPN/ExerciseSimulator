using System.Threading;
using System.Threading.Tasks;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Actions
{
    public interface IAvatarAction
    {
        string ActionId { get; }
        bool CanRun(AvatarRuntimeState runtimeState);
        Task<bool> ExecuteAsync(AvatarRuntimeState runtimeState, CancellationToken cancellationToken);
    }
}
