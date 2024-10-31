using Exerussus._1Attributes;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus.GameEntity.Core
{
    public abstract class GameEntityBootstrapper : MonoBehaviour
    {
        [ReadOnly] protected EcsPackedEntity _entityPack;
        [ReadOnly] protected bool _isActivated;
        public EcsPackedEntity EntityPack => _entityPack;
        public bool Activated => _isActivated;
        public bool IsQuitting { get; protected set; }

        protected void SetQuitting()
        {
            IsQuitting = true;
        }
    }
}