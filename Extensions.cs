using Exerussus.GameEntity.Core;

namespace Exerussus.GameEntity.Extensions
{
    public static class GameEntityExtensions
    {
        public static void SetEntityNameStarted(this IGameEntityBootstrapper gameEntity)
        {
#if UNITY_EDITOR
            var bracketIndex = gameEntity.GameObject.name.IndexOf('(');
            if (bracketIndex != -1) gameEntity.GameObject.name = gameEntity.GameObject.name.Substring(0, bracketIndex).Trim();
            bracketIndex = gameEntity.GameObject.name.IndexOf('|');
            if (bracketIndex != -1) gameEntity.GameObject.name = gameEntity.GameObject.name.Substring(0, bracketIndex).Trim();
            gameEntity.GameObject.name = $"{gameEntity.GameObject.name} | {gameEntity.EntityPack.Id}";
#endif
        }
        
        public static void SetEntityNameDestroyed(this IGameEntityBootstrapper gameEntity)
        {
#if UNITY_EDITOR
            var bracketIndex = gameEntity.GameObject.name.IndexOf('(');
            if (bracketIndex != -1) gameEntity.GameObject.name = gameEntity.GameObject.name.Substring(0, bracketIndex).Trim();
            bracketIndex = gameEntity.GameObject.name.IndexOf('|');
            if (bracketIndex != -1) gameEntity.GameObject.name = gameEntity.GameObject.name.Substring(0, bracketIndex).Trim();
            gameEntity.GameObject.name = $"{gameEntity.GameObject.name} | destroyed";
#endif
        }
    }
}