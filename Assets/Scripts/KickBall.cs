// *---- Purpose of this file: ----*
// Manage the kicking of the object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class KickBall : MonoBehaviour
{

    public GameObject Target;
    public GameObject Projectile;

    public AudioManager AM;

    public LevelManager levelManager;

    public GameObject Ball;
    public GameObject Bomb;
    public GameObject Explosion;

    public Rigidbody rb;

    public Collider goal;

    public float widthZone;
    public float minHeight;
    public float playerWidth;
    public float playerHeight;

    public float minSpeed = 10f;
    public float maxSpeed = 30f;
    public float maxSpin = 10f;

    public string projectileTag = "";

    public bool wasReset = true;

    public Vector3 startPosition = new Vector3(0, 0.5f, 0);

    private float maxHeight;
    private float vz;
    private float spin;

    private float g = -9.81f;

    private bool shoot = false;
    private bool inFlight = false;

    private bool fuseSoundPlayed = false;
    private bool bombTriggered = false;

    private float timeToActivate = 0;
    private float timeTillExplosion = 2.0f;

    private float _enterStartTime;

    public int randomGenerator = 0;


    // Set the object to the kick-off position and set the target area in which
    // the ball can be fired at.
    void Start()
    {
        Projectile.transform.position = startPosition;

        maxHeight = 1.4f * playerHeight;

        Ball.SetActive(true);
        Bomb.SetActive(false);
        Explosion.SetActive(false);


    }

    // The Update function is used to manage the behaviour of the projectile
    // object during flight. It applies the spin force to the object during
    // flight and if the object is labeled as a bomb, a fuse sound is played.

    void Update()
    {
        if (inFlight && Ball.transform.position.y < 15.0f)
        {
            if (projectileTag == "bomb" && !fuseSoundPlayed)
            {
                levelManager.sound.PlayOneShot(levelManager.fuse, 0.5f * AM.masterVolume);
                fuseSoundPlayed = true;
            }
            rb.AddForce(spin, 0f, 0f);
        } else
        {
            spin = 0;
        }
    }

    // The target position is set by taking a random target position within
    // a set area around the player. This area can be enlarged to make the
    // game harder.

    void setTargetPosition()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        float randomWidth = 0;
        if (randomHeight > playerHeight)
        {
            randomWidth = Random.Range(-widthZone / 2f, widthZone / 2f);
        } else {
            randomWidth = Random.Range(playerWidth / 2f, widthZone / 2f);
            var randomLR = Random.Range(0, 2);
            if (randomLR >= 1)
                randomWidth *= -1;
        }
        Target.transform.position = new Vector3(randomWidth, randomHeight, 15);
    }

    // Once the target position is set, the velocity is calculated through some
    // 3D algebra. With a random generated horizontal shot speed and spin value,
    // the algorithm determines what vertical and horizontal velocity the object
    // should be shot at to reach the predetermined target.

    void setVelocityToTarget()
    {

        spin = Random.Range(-maxSpin, maxSpin);

        vz = Random.Range(minSpeed, maxSpeed);

        var x1 = Projectile.transform.position.x;
        var y1 = Projectile.transform.position.y;
        var z1 = Projectile.transform.position.z;

        var x2 = Target.transform.position.x;
        var y2 = Target.transform.position.y;
        var z2 = Target.transform.position.z;

        var vy_numerator = (g * (z1 * z1) - 2 * g * z1 * z2 + g * (z2 * z2) + 2 * y1 * (vz * vz) - 2 * y2 * (vz * vz));
        var vy_demonimator = 2 * vz * (z1 - z2);
        var vy = vy_numerator / vy_demonimator;

        var vx_numerator = spin * (z1 * z1) - 2 * spin * z1 * z2 + spin * (z2 * z2) + 2 * x1 * (vz * vz) - 2 * x2 * (vz * vz);
        var vx_denominator = 2 * vz * (z1 - z2);
        var vx = vx_numerator / vx_denominator;

        rb.velocity = new Vector3(vx, vy, vz);
    }

    // Once object has reached the player or the goal, it has to be reset to
    // its starting position so that the next shot can be taken. This is what
    // the resetProjectile() function does. It also decides randomly whether
    // the next object will be either a ball or a bomb.

    public void resetProjectile()
    {
        if (startPosition.y > 0)
        {
            Projectile.transform.position = startPosition;
        } else
        {
            // If the ball y-coordinate is placed lower than 0, a secret
            // random kick-location mode is activated for in the higher
            // levels.
            Projectile.transform.position = new Vector3(Random.Range(-startPosition.x, startPosition.x), 0.5f, Random.Range(-startPosition.z, startPosition.z));
        }

        rb.useGravity = true;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        inFlight = false;
        wasReset = true;
        bombTriggered = false;

        fuseSoundPlayed = false;

        randomGenerator = Mathf.RoundToInt(Random.Range(0, 3));

        if (randomGenerator <= 1)
        {
            if (levelManager.ballsFired < levelManager.ballsToFire)
            {
                projectileTag = "ball";
                Ball.SetActive(true);
                Bomb.SetActive(false);
                Explosion.SetActive(false);
            } else
            {
                randomGenerator = 2;
            }
        }
        if (randomGenerator >= 2)
        {
            if (levelManager.bombsFired < levelManager.bombsToFire)
            {
                projectileTag = "bomb";
                Bomb.SetActive(true);
                Ball.SetActive(false);
                Explosion.SetActive(false);
            } else
            {
                projectileTag = "ball";
                Ball.SetActive(true);
                Bomb.SetActive(false);
                Explosion.SetActive(false);
            }
        }
    }

    // The shootProjectile() function sets everything up so that the ball can
    // be 'kicked' to the player.

    public void shootProjectile()
    {
        setTargetPosition();
        setVelocityToTarget();
        inFlight = true;
        levelManager.sound.PlayOneShot(levelManager.kickSound, 3f * AM.masterVolume);
    }

    // If the player accidentally stops a bomb, this codeblock sets the bomb
    // off and lets the player know they made a mistake.

    private void OnCollisionEnter(Collision collision)
    {
        if (projectileTag == "bomb" && collision.gameObject.tag == "Player" && !bombTriggered)
        {
            levelManager.sound.PlayOneShot(levelManager.explosion, 2f * AM.masterVolume);
            bombTriggered = true;
            Bomb.SetActive(false);
            Explosion.SetActive(true);
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.useGravity = false;
            levelManager.bombsTriggered++;
        }

        if (projectileTag == "ball" && collision.gameObject.tag == "Player")
        {
            levelManager.sound.PlayOneShot(levelManager.ballStopped, 2.5f * AM.masterVolume);
        }
    }
}
