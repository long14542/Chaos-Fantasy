using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteAnimationCreator : MonoBehaviour
{
    public string fileName;
    public Sprite[] sprites;
    public float frameRate = 5f; // FPS của animation

    [ContextMenu("Create Animation")]
    public void CreateAni()
    {
        AnimationClip clip = new AnimationClip();
        clip.frameRate = frameRate;

        EditorCurveBinding spriteBinding = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "",
            propertyName = "m_Sprite"
        };

        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe
            {
                time = (float)i / frameRate, // Điều chỉnh theo frameRate
                value = sprites[i]
            };
        }

        AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);

        // Tạo asset cho animation clip
        AssetDatabase.CreateAsset(clip, "Assets/Animations/" + fileName + ".anim");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // Tạo Animator Controller và gán animation vào đó
        var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPathWithClip
                ("Assets/Animations/" + fileName + ".controller", clip);

        fileName = "";
        sprites = new Sprite[0]; // Reset lại
    }
}
