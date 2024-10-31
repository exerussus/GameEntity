using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace Exerussus.GameEntity.Core
{
    public class GameEntityGroup : EcsGroup<GameEntityPooler>
    {
        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
#if UNITY_EDITOR
            fixedUpdateSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
        }
    }
}