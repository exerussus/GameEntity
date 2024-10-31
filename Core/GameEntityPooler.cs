using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace Exerussus.GameEntity.Core
{
    public class GameEntityPooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            GameEntity = new(world);
        }

        public PoolerModule<GameEntityData.GameEntity> GameEntity { get; private set; }
    }
}