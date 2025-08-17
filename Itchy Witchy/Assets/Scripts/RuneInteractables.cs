using UnityEngine;

public class RuneInteractables : InteractableObject
{
    public enum Material
    {
        Wood,
        Bone,
        Stone
    }

    public Material material;

    public float skillAcurracy = 0; // how accurate is the skill based part of the assignment

}
