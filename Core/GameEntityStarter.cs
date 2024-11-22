using System.Linq;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Exerussus.GameEntity.Core
{
    public abstract class GameEntityStarter : EcsStarter
    {
        private Signal _signal;
        private GameEntityPooler _pooler;
        protected override Signal Signal => _signal;
        public EcsWorld World => _world;
        public Signal SignalInstance => _signal;
        public GameEntityPooler Pooler => _pooler;
        
        public static GameEntityStarter Instance;
        private static bool _isInitialized;
        public static bool IsQuitting { get; protected set; }

        public static T GetInstance<T>() where T : GameEntityStarter
        {
            if (IsQuitting)
            {
                Debug.Log("Is quitting, так что null");
                return null;
            }
            
            if (!_isInitialized || Instance == null)
            {
                _isInitialized = true;
                Instance = new GameObject {name = $"{typeof(T).Name}"}.AddComponent<T>();
                Instance._signal = Instance.SetSignal();
                Instance.PreInitialize();
                Instance._pooler = Instance.GameShare.GetSharedObject<GameEntityPooler>();
                Instance.Initialize();
                Application.quitting += () => IsQuitting = true;
            }
            return (T)Instance;
        }

        protected override EcsGroup[] GetGroups()
        {
            var custom = GetGroups(_world, GameShare);
            var core = new EcsGroup[]
            {
                new GameEntityGroup()
            };

            return custom.Concat(core).ToArray();
        }

        protected abstract EcsGroup[] GetGroups(EcsWorld world, GameShare gameShare);

        public virtual Signal SetSignal()
        {
            return new Signal();
        }

        protected virtual void OnDisable()
        {
            _isInitialized = false;
            Instance = null;
        }

        public static void ClearInstance()
        {
            _isInitialized = false;
            Instance = null;
            IsQuitting = false;
        }
    }

#if UNITY_EDITOR

    [InitializeOnLoad]
    public static class StaticCleaner
    {
        static StaticCleaner()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode || state == PlayModeStateChange.ExitingEditMode)
            {
                GameEntityStarter.ClearInstance();
            }
        }
    }
#endif
}
