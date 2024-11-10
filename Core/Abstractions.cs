
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus.GameEntity.Core
{
    public interface IGameEntityBootstrapper
    {
        public EcsPackedEntity EntityPack { get; }
        public GameObject GameObject { get; }
        public bool Activated { get; }
        public bool IsQuitting { get; }
        public abstract EcsWorld World { get; }
        public abstract GameEntityPooler Pooler { get; } 
        public abstract List<IGameEntityComponent> Components { get; }
        public abstract void Start();
    }

    public interface IGameEntityComponent
    {
        public IGameEntityBootstrapper GameEntity { get; set; }
        public abstract void InvokeOnDeactivate(int entity);
        public abstract void InvokeOnActivate(int entity);
    }
}