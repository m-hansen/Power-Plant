using UnityEngine;

public abstract class Singleton : MonoBehaviour
{
    public static bool IsQuitting { get; private set; }

    private void OnApplicationQuit()
    {
        IsQuitting = true;
    }
}

public abstract class Singleton<T> : Singleton
    where T : MonoBehaviour
{
    private static T __instance;
    private static readonly object __lock = new object();

    public static T Instance
    {
        get
        {
            if (IsQuitting)
            {
                return null;
            }

            lock(__lock)
            {
                if (__instance != null)
                {
                    return __instance;
                }

                T[] instances = FindObjectsOfType<T>();
                int numInstances = instances.Length;
                if (numInstances == 1)
                {
                    __instance = instances[0];
                    return instances[0];
                }
                else if (numInstances > 1)
                {
                    Debug.LogError($"Multiple instances of '{nameof(Singleton)}' found. This is invalid.");
                    return instances[0];

                    // Note: we could try to salvage this by destroying other singleton instances...
                    // I'm not doing so here because this scenario should not happen in the first place and may have unexpected behavior.
                    // If you do see this error, assume something is wrong and the program should be halted.
                }
                else
                {
                    Debug.LogError($"No instances of '{nameof(Singleton)}' were found. We expected one to have been added in the scene. If you do not want to add one to the scene, then we can add them programatically. See the notes in the code.");
                    // Note: We intended for this to have been added in the scene but we can try to fix it here.
                    // TODO: Create a new game object, name it, add the component (IMPORTANT), then assign it to __instance, and return it
                    return null;
                }
            }
        }
    }
}
