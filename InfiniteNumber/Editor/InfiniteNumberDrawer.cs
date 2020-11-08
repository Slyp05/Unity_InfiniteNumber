using System.Reflection;
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
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        // get serialized property
        SerializedProperty valueStringProp = property.FindPropertyRelative("serializedValue");

        // get tooltip
        string tooltipText = null;
        TooltipAttribute tooltip = GetTooltip(fieldInfo, true);
        if (tooltip != null)
            tooltipText = tooltip.tooltip;

        // cache rect width
        float rectWidth = rect.width;

        // show label as int field for scroll capabilities
        rect.width = EditorGUIUtility.labelWidth + 2;

        EditorGUI.BeginChangeCheck();
            long scrollVal = EditorGUI.LongField(rect, new GUIContent(property.displayName, tooltipText), 0);
        if (EditorGUI.EndChangeCheck())
        {
            InfiniteNumber nb = new InfiniteNumber(valueStringProp.stringValue);
            nb += scrollVal;

            valueStringProp.stringValue = nb.ToString();
        }

        rect.x += rect.width;
        rect.width = rectWidth - rect.width;

        // show text area
        EditorGUI.BeginProperty(rect, label, property);
        {
            EditorGUI.BeginChangeCheck();
            string newVal = EditorGUI.DelayedTextField(rect, new GUIContent(""), InfiniteNumber.FormatString(valueStringProp.stringValue),
                EditorStyles.numberField);
            if (EditorGUI.EndChangeCheck())
                valueStringProp.stringValue = newVal;
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }

    TooltipAttribute GetTooltip(FieldInfo field, bool inherit)
    {
        TooltipAttribute[] attributes = field.GetCustomAttributes(typeof(TooltipAttribute), inherit) as TooltipAttribute[];

        return attributes.Length > 0 ? attributes[0] : null;
    }
}
