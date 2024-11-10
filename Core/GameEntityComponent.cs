
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus.GameEntity.Core
{
    [RequireComponent(typeof(IGameEntityBootstrapper))]
    public abstract class GameEntityComponent<T> : MonoBehaviour, IGameEntityComponent where T : GameEntityStarter
    {
        public IGameEntityBootstrapper GameEntity { get; set; }
        public EcsPackedEntity EntityPack => GameEntity.EntityPack;
        public bool IsActivated { get; private set; }
        public bool IsQuitting => GameEntity.IsQuitting;
        private T _core;
        public T Core
        {
            get
            {
                if (!_isStarterInitialized) _core = GameEntityStarter.GetInstance<T>();
                return _core;
            } 
        }

        private bool _isStarterInitialized;
        
        private void Start()
        {
            if (GameEntity != null) return;
            GameEntity = GetComponent<GameEntityBootstrapper<T>>();
            if (GameEntity == null)
            {
                GameEntity = gameObject.AddComponent<GameEntityBootstrapper<T>>();
                GameEntity.Start();
            }
            else
            {
                if (!GameEntity.Activated)
                {
                    GameEntity.Start();
                    return;
                }
                if (!GameEntity.EntityPack.Unpack(GameEntity.World, out var entity)) return;
                GameEntity.Components.Add(this);
                InvokeOnActivate(entity);
            }
        }
        
        public void InvokeOnActivate(int entity)
        {
            if (IsActivated) return;

            OnActivate(entity);
            IsActivated = true;
        }

        public void InvokeOnDeactivate(int entity) 
        {
            if (!IsActivated) return;
            
            OnDeactivate(entity);
            IsActivated = false;
        }

        private void OnDestroy()
        {
            if (GameEntityStarter.IsQuitting) return;
            if (!EntityPack.Unpack(Core.World, out var entity)) return;
            
            InvokeOnDeactivate(entity);
        }

        protected virtual void OnActivate(int entity) {}
        protected virtual void OnDeactivate(int entity) {}
    }
}