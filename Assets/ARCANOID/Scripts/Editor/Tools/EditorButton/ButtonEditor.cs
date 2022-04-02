using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(object), true, isFallback = false)]
[CanEditMultipleObjects]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        foreach (var targetObject in targets)
        {
            var methods = targetObject.GetType()
                .GetMethods().Where(method => 
                    method.GetCustomAttributes().Any(attr => 
                        attr.GetType() == typeof(EditorButtonAttribute)));

            foreach (var method in methods)
            {
                var attribute = (EditorButtonAttribute) method.GetCustomAttribute(typeof(EditorButtonAttribute));
                if (GUILayout.Button(attribute.Name))
                {
                    method.Invoke(targetObject, null);
                }
            }
        }
    }
}
