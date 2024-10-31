using Exerussus.GameEntity.Core;

namespace Exerussus.GameEntity.Plugins.Exerussus.GameEntity
{
    public static class GameEntityExtensions
    {
        public static void SetEntityNameStarted(this GameEntityBootstrapper gameEntity)
        {
#if UNITY_EDITOR
            var bracketIndex = gameEntity.name.IndexOf('(');
            if (bracketIndex != -1) gameEntity.name = gameEntity.name.Substring(0, bracketIndex).Trim();
            bracketIndex = gameEntity.name.IndexOf('|');
            if (bracketIndex != -1) gameEntity.name = gameEntity.name.Substring(0, bracketIndex).Trim();
            gameEntity.name = $"{gameEntity.name} | {gameEntity.EntityPack.Id}";
#endif
        }
        
        public static void SetEntityNameDestroyed(this GameEntityBootstrapper gameEntity)
        {
#if UNITY_EDITOR
            var bracketIndex = gameEntity.name.IndexOf('(');
            if (bracketIndex != -1) gameEntity.name = gameEntity.name.Substring(0, bracketIndex).Trim();
            bracketIndex = gameEntity.name.IndexOf('|');
            if (bracketIndex != -1) gameEntity.name = gameEntity.name.Substring(0, bracketIndex).Trim();
            gameEntity.name = $"{gameEntity.name} | destroyed";
#endif
        }
    }
}