using UnityEditor;
using UnityEngine;

public class Tools : ScriptableObject
{
    // 重置模型的轴心为中心
    [MenuItem("Tools/ResetPivot")]
    static void ResetPivot()
    {
        // 获取选中的物体
        var target = Selection.activeGameObject;
        const string dialogTitle = "Tools/ResetPivot";

        if (target == null)
        {
            EditorUtility.DisplayDialog(dialogTitle, "没有选中需要重置轴心的物体!!!", "确定");
            return;
        }

        // 获取目标物体下所有网格渲染
        var meshRenderers = target.GetComponentsInChildren<MeshRenderer>(true);
        if (meshRenderers.Length == 0)
        {
            EditorUtility.DisplayDialog(dialogTitle, "选中的物体不是有效模型物体!!!", "确定");
            return;
        }
        // 将所有的网格渲染的边界进行合并
        var centerBounds = meshRenderers[0].bounds;
        for (var i = 1; i < meshRenderers.Length; i++)
        {
            centerBounds.Encapsulate(meshRenderers[i].bounds);
        }
        // 创建目标的父物体
        var targetParent = new GameObject(target.name + "-Parent").transform;

        // 如果目标原来已有父物体,则将创建目标父物体的父物体设为原父物体;
        var originalParent = target.transform.parent;
        if (originalParent != null)
        {
            targetParent.SetParent(originalParent);
        }
        // 设置目标父物体的位置为合并后的网格渲染边界中心
        targetParent.position = centerBounds.center;
        // 设置目标物体的父物体
        target.transform.parent = targetParent;

        Selection.activeGameObject = targetParent.gameObject;
        EditorUtility.DisplayDialog(dialogTitle, "重置模型物体的轴心完成!", "确定");
    }
}