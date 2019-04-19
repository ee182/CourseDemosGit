using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RenderCubemapHelper))]
public class RenderCubemapHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Take a picture !"))
        {
            var renderCubemapHelper = (RenderCubemapHelper)target;

            GameObject go = new GameObject("RenderCubemapCamera");
            go.AddComponent<Camera>();

            var renderCamera = go.GetComponent<Camera>();

            renderCamera.transform.position = renderCubemapHelper.transform.position;

            // Note: RenderToCubemap() has orientation issue.
            //       Setting the renderCamera's rotation takes no effect!
            renderCamera.transform.rotation = renderCubemapHelper.transform.rotation;

            // render into cubemap
            renderCamera.RenderToCubemap(renderCubemapHelper.cubemap);

            // destroy temporary camera
            DestroyImmediate(go);
        }
    }
}
