using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;
    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }
    // 1
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
   
        //spawn asteriod and get its initial pos
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        //wait for 0.1 sec and assert that the y pos is less
        yield return new WaitForSeconds(0.1f);
        Assert.Less(asteroid.transform.position.y, initialYPos);
        
    
    }
    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        //spawn a asteriod and set its pos to the ship
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        //wait for 0.1 and assert that the game is over is true
        yield return new WaitForSeconds(0.1f);

        Assert.True(game.isGameOver);
    }
    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        //game over bool set to true for test and starts new game
        game.isGameOver = true;
        game.NewGame();
        //Assert the game over bools false
        Assert.False(game.isGameOver);
        yield return null;
    }
    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        //spawn a test laser object and set get its initail pos
        GameObject laser = game.GetShip().SpawnLaser();
        float initialYPos = laser.transform.position.y;
        //wait 0.1 seconds and  check the value is greater eg moving up
        yield return new WaitForSeconds(0.1f);
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        //spawn a laser and asteriod and place at the same position to have a collision
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        //Must use the assertions class and not the Nunit one for isNull()
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }
    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        //spawn a laser and asteriod and place at the same position to have a collision
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        //Assert that the game score is the same as one on the collision
        Assert.AreEqual(game.score, 1);
    }
    [UnityTest]
    public IEnumerator GameScoreEqualZero()
    {
        game.isGameOver = true;
        game.NewGame();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(game.score, 0);
    }
    [UnityTest]
    public IEnumerator MoveShipRight()
    {
        GameObject ship = game.GetShip().gameObject;
        float initialXPos = ship.transform.position.x;
        yield return new WaitForSeconds(0.1f);
        game.GetShip().MoveRight();
        Assert.Greater(ship.transform.position.x, initialXPos);
    }
    [UnityTest]
    public IEnumerator MoveShipLeft()
    {
        GameObject ship = game.GetShip().gameObject;
        float initialXPos = ship.transform.position.x;
        yield return new WaitForSeconds(0.1f);
        game.GetShip().MoveLeft();
        Assert.Less(ship.transform.position.x, initialXPos);
    }
}