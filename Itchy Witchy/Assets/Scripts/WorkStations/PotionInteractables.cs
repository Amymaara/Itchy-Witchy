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


    public PotionMaterial material;
    public Color fillColour;
   

}
