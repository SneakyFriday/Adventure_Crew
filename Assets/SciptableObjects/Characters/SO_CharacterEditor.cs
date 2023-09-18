// using UnityEditor;
// using UnityEngine;
//
// [CustomEditor(typeof(SO_Character))]
// public class SO_CharacterEditor : Editor
// {
//     private SerializedProperty characterSprite;
//     private SerializedProperty characterName;
//     private SerializedProperty characterAttentionValue;
//     private SerializedProperty characterClass;
//     private SerializedProperty currentCharacterPlace;
//     private SerializedProperty desiredObjects;
//     private SerializedProperty characterMoodSprites;
//     private SerializedProperty characterPlaceSpecificDialogue;
//     private SerializedProperty characterGenericDialogueAndAnswers;
//     private SerializedProperty firstMet;
//     private SerializedProperty mainMet;
//     private SerializedProperty interceptMet;
//     private SerializedProperty completionMet;
//
//     private void OnEnable()
//     {
//         characterSprite = serializedObject.FindProperty("characterSprite");
//         characterName = serializedObject.FindProperty("characterName");
//         characterAttentionValue = serializedObject.FindProperty("characterAttentionValue");
//         characterClass = serializedObject.FindProperty("characterClass");
//         currentCharacterPlace = serializedObject.FindProperty("currentCharacterPlace");
//         desiredObjects = serializedObject.FindProperty("desiredObjects");
//         characterMoodSprites = serializedObject.FindProperty("characterMoodSprites");
//         characterPlaceSpecificDialogue = serializedObject.FindProperty("characterPlaceSpecificDialogue");
//         characterGenericDialogueAndAnswers = serializedObject.FindProperty("characterGenericDialogueAndAnswers");
//         firstMet = serializedObject.FindProperty("firstMet");
//         mainMet = serializedObject.FindProperty("mainMet");
//         interceptMet = serializedObject.FindProperty("interceptMet");
//         completionMet = serializedObject.FindProperty("completionMet");
//     }
//
//     public override void OnInspectorGUI()
//     {
//         serializedObject.Update();
//
//         EditorGUILayout.PropertyField(characterSprite, new GUIContent("Character Sprite"), false);
//         EditorGUILayout.Space();
//
//         EditorGUILayout.LabelField("Character Attributes", EditorStyles.boldLabel);
//         EditorGUILayout.PropertyField(characterName, new GUIContent("Name"));
//         EditorGUILayout.PropertyField(characterAttentionValue, new GUIContent("Attention Value"));
//         EditorGUILayout.PropertyField(characterClass, new GUIContent("Character Class"));
//         EditorGUILayout.PropertyField(currentCharacterPlace, new GUIContent("Current Place"));
//         EditorGUILayout.Space();
//
//         EditorGUILayout.LabelField("Desired Objects", EditorStyles.boldLabel);
//         EditorGUILayout.PropertyField(desiredObjects, new GUIContent("Objects"), true);
//         EditorGUILayout.Space();
//
//         EditorGUILayout.LabelField("Character Moods", EditorStyles.boldLabel);
//         EditorGUILayout.PropertyField(characterMoodSprites, true);
//         EditorGUILayout.Space();
//
//         EditorGUILayout.LabelField("Place Specific Dialogues", EditorStyles.boldLabel);
//         DrawDialogueAndAnswers(characterPlaceSpecificDialogue);
//         EditorGUILayout.Space();
//
//         EditorGUILayout.LabelField("Generic Dialogue for Testing", EditorStyles.boldLabel);
//         DrawDialogueAndAnswers(characterGenericDialogueAndAnswers);
//         EditorGUILayout.Space();
//
//         EditorGUILayout.LabelField("Interaction Flags", EditorStyles.boldLabel);
//         EditorGUILayout.PropertyField(firstMet, new GUIContent("First Met"));
//         EditorGUILayout.PropertyField(mainMet, new GUIContent("Main Met"));
//         EditorGUILayout.PropertyField(interceptMet, new GUIContent("Intercept Met"));
//         EditorGUILayout.PropertyField(completionMet, new GUIContent("Completion Met"));
//
//         serializedObject.ApplyModifiedProperties();
//
//         if (GUI.changed)
//         {
//             EditorUtility.SetDirty(target);
//         }
//     }
//
//     private void DrawDialogueAndAnswers(SerializedProperty dialogueProperty)
//     {
//         dialogueProperty.isExpanded = EditorGUILayout.Foldout(dialogueProperty.isExpanded, dialogueProperty.displayName);
//         
//         
//         if (dialogueProperty.isExpanded)
//         {
//             EditorGUI.indentLevel++;
//             EditorGUILayout.PropertyField(dialogueProperty.FindPropertyRelative("Array.size"));
//             for (int i = 0; i < dialogueProperty.arraySize; i++)
//             {
//                 EditorGUILayout.PropertyField(dialogueProperty.GetArrayElementAtIndex(i), new GUIContent("Dialogue " + i));
//             }
//             EditorGUI.indentLevel--;
//         }
//     }
// }
