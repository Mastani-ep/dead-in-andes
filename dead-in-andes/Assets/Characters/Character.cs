using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    [FormerlySerializedAs("_name")] [SerializeField] public string characterName = "lol";
    [SerializeField] private bool alive = true;
    // [SerializeField] public Sprite characterSprite;
    
    [Header("Needs")]    
    [SerializeField] public int hungerMeter = 0;
    [SerializeField] public int fatigueMeter = 0;
    [SerializeField] public int sicknessMeter = 0;

    public void NextDayChanges()
    {
        if (alive)
        {
            hungerMeter += 1;
            fatigueMeter += 1;
            sicknessMeter += 1;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
