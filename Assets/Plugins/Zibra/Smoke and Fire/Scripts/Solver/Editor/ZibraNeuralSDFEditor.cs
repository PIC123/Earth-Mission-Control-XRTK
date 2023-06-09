#if ZIBRA_SMOKE_AND_FIRE_PAID_VERSION

using com.zibraai.smoke_and_fire.SDFObjects;
using UnityEditor;
using UnityEngine;

namespace com.zibraai.smoke_and_fire.Editor.SDFObjects
{
    [CustomEditor(typeof(NeuralSDF))]
    [CanEditMultipleObjects]
    public class NeuralSDFEditor : UnityEditor.Editor
    {
        static NeuralSDFEditor EditorInstance;

        private NeuralSDF[] NeuralSDFs;

        SerializedProperty InvertSDF;
        SerializedProperty SurfaceDistance;

        [MenuItem("Zibra AI/Zibra AI - Smoke And Fire/Generate all Neural SDFs in the Scene", false, 20)]
        static void GenerateAllSDFs()
        {
            if (EditorApplication.isPlaying)
            {
                Debug.LogWarning("Neural colliders can only be generated in edit mode.");
                return;
            }

            if (ZibraServerAuthenticationManager.GetInstance().GetStatus() != ZibraServerAuthenticationManager.Status.OK)
            {
                Debug.LogWarning("Licence key validation in process");
                return;
            }

            // Find all neural colliders in the scene
            NeuralSDF[] allNeuralSDF = FindObjectsOfType<NeuralSDF>();

            if (allNeuralSDF.Length == 0)
            {
                Debug.LogWarning("No neural colliders found in the scene.");
                return;
            }

            // Find all corresponding game objects
            GameObject[] allNeraulCollidersGameObjects = new GameObject[allNeuralSDF.Length];
            for (int i = 0; i < allNeuralSDF.Length; i++)
            {
                allNeraulCollidersGameObjects[i] = allNeuralSDF[i].gameObject;
            }
            // Set selection to that game objects so user can see generation progress
            Selection.objects = allNeraulCollidersGameObjects;

            // Add all colliders to the generation queue
            foreach (var neuralSDFinstance in allNeuralSDF)
            {
                if (!GenerationQueue.Contains(neuralSDFinstance) && !neuralSDFinstance.HasRepresentation())
                {
                    GenerationQueue.AddToQueue(neuralSDFinstance);
                }
            }
        }

        protected void Awake()
        {
            ZibraServerAuthenticationManager.GetInstance().Initialize();
        }
        protected void OnEnable()
        {
            EditorInstance = this;

            NeuralSDFs = new NeuralSDF[targets.Length];

            for (int i = 0; i < targets.Length; i++)
            {
                NeuralSDFs[i] = targets[i] as NeuralSDF;
            }

            serializedObject.Update();
            InvertSDF = serializedObject.FindProperty("InvertSDF");
            SurfaceDistance = serializedObject.FindProperty("SurfaceDistance");
            serializedObject.ApplyModifiedProperties();
        }

        protected void OnDisable()
        {
            if (EditorInstance == this)
            {
                EditorInstance = null;
            }
        }

        private void GenerateSDFs(bool regenerate = false)
        {
            foreach (var instance in NeuralSDFs)
            {
                if (!GenerationQueue.Contains(instance) || regenerate)
                {
                    GenerationQueue.AddToQueue(instance);
                }
            }
        }

        public void Update()
        {
            if (GenerationQueue.GetQueueLength() > 0)
                EditorInstance.Repaint();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (EditorApplication.isPlaying)
            {
                // Don't allow generation in playmode
            }
            else if (ZibraServerAuthenticationManager.GetInstance().GetStatus() !=
                     ZibraServerAuthenticationManager.Status.OK)
            {
                GUILayout.Label(ZibraServerAuthenticationManager.GetInstance().GetErrorMessage());
                GUILayout.Space(20);
            }
            else
            {
                int toGenerateCount = 0;
                int toRegenerateCount = 0;

                foreach (var instance in NeuralSDFs)
                {
                    if (!GenerationQueue.Contains(instance))
                    {
                        if (instance.objectRepresentation.HasRepresentationV3)
                        {
                            toRegenerateCount++;
                        }
                        else
                        {
                            toGenerateCount++;
                        }
                    }
                }

                int inQueueCount = NeuralSDFs.Length - toGenerateCount - toRegenerateCount;
                int fullQueueLength = GenerationQueue.GetQueueLength();
                if (fullQueueLength > 0)
                {
                    if (fullQueueLength != inQueueCount)
                    {
                        if (inQueueCount == 0)
                        {
                            GUILayout.Label($"Generating other SDFs. {fullQueueLength} left in total.");
                        }
                        else
                        {
                            GUILayout.Label(
                                $"Generating SDFs. {inQueueCount} left out of selected SDFs. {fullQueueLength} SDFs left in total.");
                        }
                    }
                    else
                    {
                        GUILayout.Label(NeuralSDFs.Length > 1 ? $"Generating SDFs. {inQueueCount} left."
                                                              : "Generating SDF.");
                    }
                    if (GUILayout.Button("Abort"))
                    {
                        GenerationQueue.Abort();
                    }
                }

                if (toGenerateCount > 0)
                {
                    GUILayout.Label(NeuralSDFs.Length > 1 ? $"{toGenerateCount} SDFs don't have representation."
                                                          : "SDF doesn't have representation.");
                    if (GUILayout.Button(NeuralSDFs.Length > 1 ? "Generate SDFs" : "Generate SDF"))
                    {
                        GenerateSDFs();
                    }
                }

                if (toRegenerateCount > 0)
                {
                    GUILayout.Label(NeuralSDFs.Length > 1 ? $"{toRegenerateCount} SDFs already generated."
                                                          : "SDF already generated.");
                    if (GUILayout.Button(NeuralSDFs.Length > 1 ? "Regenerate all selected SDFs" : "Regenerate SDF"))
                    {
                        GenerateSDFs(true);
                    }
                }
            }

            ulong totalMemoryFootprint = 0;
            foreach (var instance in NeuralSDFs)
            {
                if (instance.objectRepresentation.HasRepresentationV3)
                {
                    totalMemoryFootprint += instance.GetMemoryFootrpint();
                }
            }

            if (totalMemoryFootprint != 0)
            {
                GUILayout.Space(10);

                if (NeuralSDFs.Length > 1)
                {
                    GUILayout.Label("Multiple neural SDFs selected. Showing sum of all selected instances.");
                }
                GUILayout.Label($"Approximate VRAM footprint:{(float)totalMemoryFootprint / (1 << 20):N2}MB");
            }
            EditorGUILayout.PropertyField(InvertSDF);
            EditorGUILayout.PropertyField(SurfaceDistance);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
