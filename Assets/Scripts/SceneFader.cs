using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    [SerializeField] Texture2D fadeOutTexture;
    [SerializeField] float fadeSpeed = 0.8f;
    [SerializeField] bool fadeSceneOnLoad = true;

    int drawDepth = -1000;
    int fadeDirection = -1;
    float alpha = 1f;

    private void OnGUI()
    {
        if (fadeSceneOnLoad)
        {
            alpha += fadeDirection * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
        }
    }

    public float BeginFade(int direction)
    {
        fadeDirection = direction;
        return fadeSpeed;
    }
}
