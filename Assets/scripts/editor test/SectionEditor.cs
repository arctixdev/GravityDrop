using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class SectionEditor : Editor
{
    private string currentSection;

    public override void OnInspectorGUI()
    {
        MonoBehaviour targetScript = (MonoBehaviour)target;
        SerializedObject serializedObject = new SerializedObject(targetScript);
        SerializedProperty property = serializedObject.GetIterator();

        while (property.NextVisible(true))
        {
            if (property.propertyPath == "m_Script") continue;

            SectionAttribute[] attributes = (SectionAttribute[])property.GetCustomAttributes(typeof(SectionAttribute), true);
            if (attributes.Length > 0)
            {
                SectionAttribute sectionAttribute = attributes[0];
                if (currentSection != sectionAttribute.title)
                {
                    currentSection = sectionAttribute.title;
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField(currentSection, EditorStyles.boldLabel);
                }
            }

            EditorGUILayout.PropertyField(property, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}