using System.Collections.Generic;
using System.Linq;
using Exerussus._1Attributes;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus.GameEntity.Core
{
    public abstract class GameEntityBootstrapper<T> : GameEntityBootstrapper where T : GameEntityStarter
    {
        [SerializeField, HideInInspector] private List<GameEntityComponent<T>> gameEntityComponent;
        
        private T _core;
        private bool _isStarterInitialized;
        public List<GameEntityComponent<T>> Components => gameEntityComponent;
        public EcsWorld World => Core.World;
        public GameEntityPooler Pooler => Core.Pooler;

        public T Core
        {
            get
            {
                if (!_isStarterInitialized) _core = GameEntityStarter.GetInstance<T>();
                return _core;
            } 
        }

        [Button]
        public void Start()
        {
            if (_isActivated) return;
            if (!Application.isPlaying) return;
            Application.quitting += SetQuitting;
            gameEntityComponent = GetComponents<GameEntityComponent<T>>().ToList();
            
            if (gameEntityComponent is not {Count: > 0})
            {
                gameEntityComponent = new List<GameEntityComponent<T>>(GetComponents<GameEntityComponent<T>>());
                if (gameEntityComponent is not {Count: > 0}) return;
            }
            _entityPack = World.PackEntity(World.NewEntity());
            
            ref var gameEntityData = ref Pooler.GameEntity.AddOrGet(_entityPack.Id);
            gameEntityData.Value = this;
            
            foreach (var entityComponent in gameEntityComponent)
            {
                entityComponent.gameEntity = this;
                entityComponent.InvokeOnActivate(_entityPack.Id);
            }
            
            _isActivated = true;
            OnStartEntity();
            gameObject.SetActive(true);
        }

        [Button]
        public void OnDestroy()
        {
            if (IsQuitting) return;
            if (_isActivated && _entityPack.Unpack(World, out var entity))
            {
                if (_isActivated)
                {
                    foreach (var gearComponent in gameEntityComponent)
                    {
                        gearComponent.InvokeOnDeactivate(_entityPack.Id);
                    }

                    OnBeforeDestroyEntity();
                    World.DelEntity(_entityPack.Id);
                    _isActivated = false;
                    OnAfterDestroyEntity();
                }
            }
            gameObject.SetActive(false);
        }
        
        [Button]
        public void Reinitialize()
        {
            OnDestroy();
            Start();
        }
        
        public virtual void OnStartEntity() {}
        public virtual void OnBeforeDestroyEntity() {}
        public virtual void OnAfterDestroyEntity() {}
    }
}