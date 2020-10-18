using System.Collections;
using System.Collections.Generic;
using GameManagers;
using UnityEditor;
using UnityEngine;


namespace Editor
{
    [CustomPropertyDrawer(typeof(OddRangeAttribute))]
    
    public class OddEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                EditorGUI.LabelField(position, label.text, "OddRange is only compatible with int");
            }

            OddRangeAttribute range = (OddRangeAttribute) attribute;

            int currentValue = property.intValue;

            currentValue = Mathf.Clamp(currentValue, range.Min, range.Max);

            if ((currentValue & 1) == 0) // Vérifie if impair ou pas
            {
                currentValue++;
            }
            
            
            int newValue = EditorGUI.IntField(position, label.text, currentValue);
			
            if ((newValue & 1) == 0)   // Vérifie if impair ou pas
            {
                if (newValue < currentValue)  // si la nouvelle valeur est inférieur au valeur d'avant alors --
                {
                    newValue--;
                }
                else
                {
                    newValue++;
                }
            }

            newValue = Mathf.Clamp(newValue, range.Min, range.Max);

            property.intValue = newValue;


        }
    }
}