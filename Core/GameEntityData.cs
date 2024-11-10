using Exerussus._1EasyEcs.Scripts.Core;

namespace Exerussus.GameEntity.Core
{
    public static class GameEntityData
    {
        public struct GameEntity : IEcsComponent
        {
            public IGameEntityBootstrapper Value;
        }
        
        public struct UniqueId : IEcsComponent
        {
            public int Value;
        }
    }
}