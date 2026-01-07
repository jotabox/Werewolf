using UnityEngine;

namespace Werewolf.Input
{
    public interface IInputService : Werewolf.Core.IGameService
    {
        Vector2 Move { get; }
        bool JumpPressed { get; }
        bool DashPressed { get; }
        bool AttackPressed { get; }
    }
}
