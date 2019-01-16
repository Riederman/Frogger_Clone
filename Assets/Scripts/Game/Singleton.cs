using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // If instance is null destroy all but first in scene
                T[] inScene = FindObjectsOfType<T>();

                if (inScene.Length >= 0)
                {
                    instance = inScene[0];
                }

                for (int i = 1; i < inScene.Length; i++)
                {
                    Destroy(inScene[i].gameObject);
                }

                if (instance == null)
                {
                    // If instance is still null add it to scene
                    GameObject gameObj = new GameObject();
                    gameObj.name = typeof(T).ToString();
                    instance = gameObj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}