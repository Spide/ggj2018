using UnityEngine;

public class BallPowerUp : PowerUp
{

	public  override void pickupBy(Player player){
		player.enableForceField ();

	}

	public override void pickupBy(Ball ball){

		GameObject newBall = GameObject.Instantiate (ball.gameObject);
		Ball newBallB = newBall.GetComponent<Ball> ();
		newBallB.isBonus = true;
		newBallB.resetBall ();
		newBall.GetComponent<SpriteRenderer> ().color = Color.green;

		Debug.Log (newBallB);
	}
}

