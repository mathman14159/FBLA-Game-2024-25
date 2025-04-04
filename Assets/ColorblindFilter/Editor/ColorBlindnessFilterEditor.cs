using UnityEditor;

namespace ColorblindFilter.Editor
{
    [CustomEditor(typeof(global::ColorblindFilter.Scripts.ColorblindFilter))]
    public class ColorBlindnessFilterEditor : UnityEditor.Editor
    {
        private global::ColorblindFilter.Scripts.ColorblindFilter _colorblindFilter;

        private SerializedProperty _blindnessType;
        private SerializedProperty _useFilter;

        private void OnEnable()
        {
            _colorblindFilter = target as global::ColorblindFilter.Scripts.ColorblindFilter;
            _blindnessType = serializedObject.FindProperty("_blindnessType");
            _useFilter = serializedObject.FindProperty("_useFilter");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_useFilter);
            EditorGUILayout.PropertyField(_blindnessType);

            serializedObject.ApplyModifiedProperties();
        }
    }
}