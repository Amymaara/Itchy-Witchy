using UnityEngine;

public class PotionInteractables : InteractableObject
{

    public enum PotionMaterial
    {
        cupidsTears,
        sage,
        moonWater,
        dragonsBlood
    }

    public enum Recipe
    {
        None,
        Knowledge,
        Love
    }

    public PotionMaterial material;
    public Recipe recipe;
    public Color fillColour;
   

}
