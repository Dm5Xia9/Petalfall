using UnityEngine;

[System.Serializable]
public struct FlowerParameters
{
    public Color[] BudColors;

    public int StemType;
    public int BudType;
    public int StemLength;
    public int LeafCount;
    public float BranchCount;

    public float LightPowerBud;

    public FlowerParameters(Color[] budColors, int stemType, int budType, int stemLength, int leafCount, float branchCount, float lightPowerBud)
    {
        BudColors = budColors;
        StemType = stemType;
        BudType = budType;
        StemLength = stemLength;
        LeafCount = leafCount;
        LightPowerBud = lightPowerBud;
        BranchCount = branchCount;
    }
}