using UnityEngine;

// To pause and unpause a stopwatch, enable/disable the gameobject
public class Stopwatch : MonoBehaviour
{
    public float ElapsedTime { get; private set; }

    public string FormattedTime
    {
        get
        {
            int minutes = (int)(ElapsedTime / 60) % 60;
            int seconds = (int)(ElapsedTime % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void Update()
    {
        // Note: if we ever start messing with time scale for a mechanic we should consider using
        // Time.unscaledDeltaTime. I'm not doing so now because I was running into an issue where
        // my game would freeze and the time would jump (correctly), but this look's bad to the user
        // and I don't have time to diagnose further.
        ElapsedTime += Time.deltaTime;
    }

    public void Clear() => ElapsedTime = 0f; // Note: Reset() is a Unity keyword for use in the editor, so we use clear instead
}
