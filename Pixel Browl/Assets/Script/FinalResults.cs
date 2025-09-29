using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalResults : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI result;


    [SerializeField] AudioSource source;

    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip sad;

    int total;
    [SerializeField]SpriteRenderer background;
    [SerializeField] Image emoji;
    [SerializeField] Sprite[] emojies;


    void Start()
    {
        
        total = GameManager.totPlayers;
        result.text = "Your Arrived " + total.ToString();

        if(total > 4)
        {
            background.color = Color.red;
            emoji.sprite = emojies[0];
            source.clip = sad;
            
        }
        else
        {
            background.color= Color.blue;
            emoji.sprite = emojies[1];
            source.clip = victory;
        }
        source.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
