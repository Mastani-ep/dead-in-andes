using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    [FormerlySerializedAs("_name")] [SerializeField] public string characterName = "lol";
    [SerializeField] private bool alive = true;
    public SpriteRenderer characterImage;
    // [SerializeField] public Sprite characterSprite;
    
    [Header("Needs")]    
    [SerializeField] public int hungerMeter = 0;
    [SerializeField] public int fatigueMeter = 0;
    [SerializeField] public int sicknessMeter = 0;
    [SerializeField] public int injuryMeter = 0;
    [SerializeField] public int depressionMeter = 0;
    
    [Header("Skills")]
    [SerializeField] public int gathering;
    [SerializeField] public int hunting;
    [SerializeField] public int fishing;
    [SerializeField] public int cooking;
    [SerializeField] public int communication;
    [SerializeField] public int knowledge;
    [SerializeField] public int craftsmanship;
    [SerializeField] public int exploration;

    public void NextDayChanges()
    {
        if (alive)
        {
            hungerMeter += 15;
            fatigueMeter += 25;
            sicknessMeter += 10;

            if (hungerMeter >= 100)
            {
                alive = false;
            }
        }
    }
    
    
    // [SerializeField] private string _sex;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!characterImage)
        {
            characterImage = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
