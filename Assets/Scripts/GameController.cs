using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Range(2, 4)]
    public int players;
    public Transform[] SpawnPoints;
    public PlayerController playerPrefab;
    public Color[] playerColors;
    public List<PlayerController>  listOfPlayers = new List<PlayerController>();

	void Start () {
        listOfPlayers.Clear();

        foreach (PlayerController player in listOfPlayers)
        {
            player.AssignColor(playerColors[player.playerNumber]);
        }


    }

    private void ClearSceneOfPlayers()
    {
        PlayerController[] tempList = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in tempList)
        {
            Destroy(player.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void ResetScene()
    {
        listOfPlayers.Clear();

        PlayerController newPlayer = Instantiate(playerPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
