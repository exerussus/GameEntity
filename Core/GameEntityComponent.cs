using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus.GameEntity.Core
{
    [RequireComponent(typeof(GameEntityBootstrapper))]
    public abstract class GameEntityComponent<T> : MonoBehaviour where T : GameEntityStarter
    {
        [SerializeField, HideInInspector] public GameEntityBootstrapper<T> gameEntity;
        public EcsPackedEntity EntityPack => gameEntity.EntityPack;
        public bool IsActivated { get; private set; }
        public bool IsQuitting => gameEntity.IsQuitting;
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
            if (gameEntity != null) return;
            gameEntity = GetComponent<GameEntityBootstrapper<T>>();
            if (gameEntity == null)
            {
                gameEntity = gameObject.AddComponent<GameEntityBootstrapper<T>>();
                gameEntity.Start();
            }
            else
            {
                if (!gameEntity.Activated)
                {
                    gameEntity.Start();
                    return;
                }
                if (!gameEntity.EntityPack.Unpack(gameEntity.World, out var entity)) return;
                gameEntity.Components.Add(this);
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