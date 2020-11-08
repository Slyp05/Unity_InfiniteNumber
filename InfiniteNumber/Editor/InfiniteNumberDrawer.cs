using UnityEditor;
using UnityEngine;

/*

    InfiniteNumber created by Lucas Sarkadi.

    Creative Commons Zero v1.0 Universal licence, 
    meaning it's free to use in any project with no need to ask permission or credits the author.

    Check out the github page for more informations:
    https://github.com/Slyp05/Unity_InfiniteNumber

*/
[CustomPropertyDrawer(typeof(InfiniteNumber))]
public class InfiniteNumberDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty valueStringProp = property.FindPropertyRelative("serializedValue");
        
        EditorGUI.BeginProperty(position, label, property);
        {
            EditorGUI.BeginChangeCheck();
                string newVal = EditorGUI.DelayedTextField(position, new GUIContent(property.displayName), 
                    InfiniteNumber.FormatString(valueStringProp.stringValue));
            if (EditorGUI.EndChangeCheck())
                valueStringProp.stringValue = newVal;
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }
}