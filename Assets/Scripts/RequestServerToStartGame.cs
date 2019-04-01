using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RequestServerToStartGame : NetworkBehaviour
{
    [SyncVar]
    private int health = 100;
    public TextMeshProUGUI Health;
    public Button startGameButton;
    //public Slider countPlayersSlider;
    //public Slider countRoundsSlider;
    //public InputField passwordInputField;


    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(RequestServer);
    }

    [Command]
    void CmdHealth()
    {
        health -= 2;
    }

    void RequestServer()
    {
        CmdHealth();
        Health.SetText(health.ToString());
    }
}
