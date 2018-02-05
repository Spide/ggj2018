using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public float defaultCameraSize;

	public bool focus;
	public float timeToFocus;
	public Vector3 deadPosition;
	public Vector3 defaultPosition;

    public Transform PauseMenu;

	public float timeToEnd = 120f;

	public static GameManager instance;

	private List<Player> players;

	private Dictionary<Player, Ball> playersDead;

	private List<Transform> spawnPoints;

	public List<PowerUp> powerUps;
	private List<Transform> powerUpPoints;

	public List<Sprite> runes;
	private List<PickupPoint> pickupPoints;


	private PickupPoint actualPickupPoint;

	private bool powerupAvailable = false;
	private float nextPowerUpTimeer = 5;

	public Text EndGameText;

	// Use this for initialization
	void Awake()
	{
		players = new List<Player>();

		playersDead = new Dictionary<Player, Ball>();

		spawnPoints = new List<Transform>();

		powerUpPoints = new List<Transform>();

		pickupPoints = new List<PickupPoint>();

		instance = this;
	}

	void Start()
	{
		foreach (PickupPoint point in pickupPoints)
		{
			point.disablePoint();
		}

		chooseNextPickupPoint();

		//activateNewPowerUp (powerUps[Random.Range(0, powerUps.Count-1)]);
	}


	public void EndGame(string winner, string reason)
	{
		string finaLText = "Winner is: " + winner + "" + " \n" + reason;
		EndGameText.enabled = true;
		EndGameText.text = finaLText;
		print(finaLText);
		StartCoroutine("FinishGame");
	}

	private IEnumerator FinishGame()
	{
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("intro");
	}


	public void chooseNextPickupPoint()
	{
		if (actualPickupPoint == null)
		{
			actualPickupPoint = pickupPoints[Random.Range(0, pickupPoints.Count - 1)];
		}
		else if (pickupPoints.Count >= 1)
		{
			PickupPoint farest = pickupPoints[0];

			foreach (PickupPoint point in pickupPoints)
			{
				if (Vector2.Distance(actualPickupPoint.transform.position, farest.transform.position) <
				    Vector2.Distance(point.transform.position, actualPickupPoint.transform.position))
				{
					farest = point;
				}
			}

			actualPickupPoint = farest;
		}
		else
		{
			// win state
			FindObjectOfType<Migician>().EndGameEnable();
			return;
		}

		//debug
		//FindObjectOfType<Migician>().EndGameEnable();

		actualPickupPoint.transform.Find("rune").GetComponent<SpriteRenderer>().sprite = runes[runes.Count - 1];

		runes.RemoveAt(runes.Count - 1);

		actualPickupPoint.activatePoint();
	}

	public void powerupPicked(PowerUp powerUp)
	{
		powerupAvailable = false;
		nextPowerUpTimeer = 5;
	}

	public void activateNewPowerUp(PowerUp powerUp)
	{
		powerupAvailable = true;
		GameObject pow = Instantiate(powerUp.gameObject);
		pow.transform.position = powerUpPoints[Random.Range(0, powerUpPoints.Count - 1)].position;
	}

	public void addSpawnPoint(Transform point)
	{
		spawnPoints.Add(point);
	}


	public void addPickupPoint(PickupPoint point)
	{
		pickupPoints.Add(point);
	}

	public void addPowerUpSpawnPoint(Transform point)
	{
		powerUpPoints.Add(point);
	}

	public void finishedPickupPoint(PickupPoint point)
	{
		pickupPoints.Remove(point);
		point.disablePoint();
		chooseNextPickupPoint();
	}

	private void timeIsUp()
	{
		EndGame("Demon", "Bear is slow!");
	}


	// Update is called once per frame
	void Update()
	{
		if (focus)
		{
			timeToFocus -= Time.deltaTime;
			if (timeToFocus <= 0)
			{
				foreach (Player p in playersDead.Keys)
				{
					p.respawn(spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position);
					playersDead[p].resetBall();
				}

				playersDead.Clear();

				Time.timeScale = 1f;

				focus = false;
			}
		}

		if (!powerupAvailable)
		{
			if (nextPowerUpTimeer <= 0)
			{
				activateNewPowerUp(powerUps[Random.Range(0, powerUps.Count - 1)]);
			}

			nextPowerUpTimeer -= Time.deltaTime;
		}


		timeToEnd -= Time.deltaTime;

		if (timeToEnd <= 0)
		{
			timeIsUp();
		}

		if (Input.GetButtonDown("Cancel"))
		{
            pause();

        }
	}

    public void pause()
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1f;
            PauseMenu.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            PauseMenu.gameObject.SetActive(true);
        }
    }

    public void toMenu()
    {
        SceneManager.LoadScene("intro");
    }


    public void onDead(Player player, Ball byBall)
	{
		//defaultCameraSize = Camera.main.orthographicSize;
		//defaultPosition = Camera.main.gameObject.transform.position;
		//deadPosition = player.transform.position;
		if (!playersDead.ContainsKey(player))
		{
			playersDead.Add(player, byBall);

			//Camera.main.orthographicSize = defaultCameraSize - 0.2f;
			Time.timeScale = 0.2f;

			focus = true;
			timeToFocus = 0.2f;
		}
	}
}