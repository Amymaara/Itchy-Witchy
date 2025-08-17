using UnityEngine;

public class RuneInteractables : InteractableObject
{
    
    public enum RuneMaterial
    {
        Wood,
        Bone,
        Stone
    }

    public enum Stamp
    {
        None,
        Star,
        Square,
        Triangle
    }

    public RuneMaterial material;
    public Stamp stamp;

    public float skillAcurracy = 0; // how accurate is the skill based part of the assignment

}
