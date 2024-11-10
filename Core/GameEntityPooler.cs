using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;

namespace Exerussus.GameEntity.Core
{
    public class GameEntityPooler : IGroupPooler
    {
        public virtual void Initialize(EcsWorld world)
        {
            GameEntity = new(world);
            UniqueId = new(world);
            _uniqueIdCounter = new Counter();
        }

        private Counter _uniqueIdCounter;
        public PoolerModule<GameEntityData.GameEntity> GameEntity { get; private set; }
        public PoolerModule<GameEntityData.UniqueId> UniqueId { get; private set; }

        public int GetUniqueId()
        {
            return _uniqueIdCounter.GetNext();
        }
    }
}