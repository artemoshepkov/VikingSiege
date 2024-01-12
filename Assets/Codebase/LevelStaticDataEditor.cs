using System.Linq;
using Codebase.Data;
using Codebase.Data.ScriptableObjects;
using Codebase.Spawner;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData) target;

            if (GUILayout.Button("Collect"))
            {
                levelData.SceneKey = SceneManager.GetActiveScene().name;

                levelData.EnemySpawners = FindObjectsOfType<SpawnerMarker>()
                    .Select(s => new EnemySpawnerData(s.EnemyTypeId, s.transform.position))
                    .ToList();
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}