using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// object unique id

public enum SceneType
{
	None,
	Load,
	Main,
	Jump,
	Battle,
}

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;
	public static SceneType sceneType = SceneType.None;

	public WalkerConfigs walkerConfigs;
	public XianConfigs xianConfigs;
	public LevelConfigs levelConfigs;
	public MonsterConfigs monsterConfigs;

	public GameObject walkerPrefab;
	public GameObject xianPrefab;
	public GameObject monsterPrefab;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void OnBeforeSceneLoadRuntimeMethod()
	{
#if UNITY_EDITOR
		var editorActiveSceneName = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name.ToLower();
		if (editorActiveSceneName.Contains("test") || editorActiveSceneName.Contains("debug"))
		{
			return;
		}
#endif

		GameManager.sceneType = SceneType.Load;
		SceneManager.LoadScene ("LoadScene");
	}

	void Awake()
	{
		if (instance != null) 
		{
			Destroy (gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (gameObject);
	}

}
