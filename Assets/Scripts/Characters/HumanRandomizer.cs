using UnityEngine;

public class HumanRandomizer : MonoBehaviour
{
    [Header("Randomization")]
    [SerializeField] private Color[] possibleColors;
    [SerializeField] private Renderer skinRenderer;
    [SerializeField] private float sizeMin = 0.0016f;
    [SerializeField] private float sizeMax = 0.0019f;

    [SerializeField] private Mesh[] hairStylesMale;
    [SerializeField] private Mesh[] facialHairStyles;
    [SerializeField] private Mesh[] hairStylesFemale;

    [SerializeField] private Color[] hairColor;

    private Gender gender = Gender.M;

    [Header("Mesh info")]
    [SerializeField] private Mesh[] male;
    [SerializeField] private Mesh[] female;

    [SerializeField] private MeshFilter hairFilter;
    [SerializeField] private MeshFilter facialHairFilter;

    [SerializeField] private MeshRenderer hairRenderer;
    [SerializeField] private MeshRenderer facialHairRenderer;

    public void SetGender(Gender gender)
    {
        this.gender = gender;
    }

    private void Start()
    {
        Randomize();
    }

    public void Randomize()
    {
        // Remove once Character class is linked up to this
        #region Gender test 
        int r = Random.Range(0, 2);
        if (r == 0) SetGender(Gender.F);
        #endregion

        Animator animator = GetComponent<Animator>();
        animator.SetFloat("Animation Type", Random.Range(0f, 1f));

        float localScale = Random.Range(sizeMin, sizeMax);
        transform.localScale = new Vector3(localScale, localScale, localScale);

        Material material = skinRenderer.material;

        // Replace the material in the editor with a copy of it. Otherwise Unity doesn't like changing it for some reason.
        skinRenderer.material = material;

        int skinColorID = Random.Range(0, possibleColors.Length);

        bool darkSkinTone = false; // Used for hair color logic. Black people tend not to have blonde hair.

        if (skinColorID >= 7)
            darkSkinTone = true;

        Color chosenSkinColor = possibleColors[skinColorID];

        skinRenderer.material.SetColor("_Color", chosenSkinColor);

        SkinnedMeshRenderer meshRenderer = skinRenderer as SkinnedMeshRenderer;

        int hairColorID;
        if (darkSkinTone) hairColorID = Random.Range(0, 6);
        else hairColorID = Random.Range(0, hairColor.Length);

        Color chosenHairColor = hairColor[hairColorID];

        material = hairRenderer.material;
        hairRenderer.material = material;
        hairRenderer.material.SetColor("_Color", chosenHairColor);

        material = facialHairRenderer.material;
        facialHairRenderer.material = material;
        facialHairRenderer.material.SetColor("_Color", chosenHairColor);

        if (gender == Gender.M)
        {
            meshRenderer.sharedMesh = male[Random.Range(0, male.Length)];
            hairFilter.sharedMesh = hairStylesMale[Random.Range(0, hairStylesMale.Length)];
            facialHairFilter.sharedMesh = facialHairStyles[Random.Range(0, facialHairStyles.Length)];
        }
        else
        {
            meshRenderer.sharedMesh = female[Random.Range(0, female.Length)];
            hairFilter.sharedMesh = hairStylesFemale[Random.Range(0, hairStylesFemale.Length)];
            facialHairFilter.sharedMesh = null;
        }
    }
}
public enum Gender
{
    M, F
}
