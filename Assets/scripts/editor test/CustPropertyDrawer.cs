using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(ArrayLayout))]
public class CustPropertyDrawer : PropertyDrawer
{


	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.PrefixLabel(position, label);

		Rect newposition = position;
		newposition.y += 18f;
		SerializedProperty data = property.FindPropertyRelative("rows");
		// EditorGUI.LabelField(new Rect(position.x, position.y + 18f, position.width, 18f), "Rows " + data.arraySize.ToString());


		//data.rows[0][]
		for (int j = 0; j < data.arraySize; j++)
		{
			SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
			newposition.height = 18f;
			newposition.width = 20f;
			for (int i = 0; i < row.arraySize; i++)
			{
				EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(i), GUIContent.none);
				newposition.x += newposition.width;
			}

			newposition.x = position.x;
			newposition.y += 18f;
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return 18f * property.FindPropertyRelative("rows").arraySize + 18f;
	}
}
