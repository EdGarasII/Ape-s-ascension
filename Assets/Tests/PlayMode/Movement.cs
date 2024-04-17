using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class GameControllerTests
{
    [Test]
    public void DieTest()
    {
        // Create a new GameObject with a SpriteRenderer component attached
        GameObject gameControllerObject = new GameObject();
        SpriteRenderer spriteRenderer = gameControllerObject.AddComponent<SpriteRenderer>();
        GameController gameController = gameControllerObject.AddComponent<GameController>();

        // Set up initial position
        gameController.startPoz = Vector2.zero;

        // Set up coroutine delay
        float delay = 0.5f;

        // Call Die method
        gameController.Die();

        // Ensure coroutine is started with correct delay
        Assert.That(spriteRenderer.enabled, Is.False.After((int)0.1f));
        Assert.That(gameController.transform.position, Is.EqualTo(Vector2.zero).After((int)delay));
        Assert.That(spriteRenderer.enabled, Is.True.After((int)(delay + 0.1f)));
    }

    [Test]
    public void PauseGameTest()
    {
        GameObject gameControllerObject = new GameObject();
        GameController gameController = gameControllerObject.AddComponent<GameController>();

        GameObject pauseMenuObject = new GameObject();
        gameController.pauseMenuScreen = pauseMenuObject;

        // Call PauseGame method
        gameController.pauseGame();

        // Ensure time scale is set to 0 and pause menu is activated
        Assert.That(Time.timeScale, Is.EqualTo(0));
        Assert.That(gameController.pauseMenuScreen.activeSelf, Is.True);
    }

    [Test]
    public void ResumeGameTest()
    {
        GameObject gameControllerObject = new GameObject();
        GameController gameController = gameControllerObject.AddComponent<GameController>();

        GameObject pauseMenuObject = new GameObject();
        gameController.pauseMenuScreen = pauseMenuObject;

        // Call ResumeGame method
        gameController.ResumeGame();

        // Ensure time scale is set to 1 and pause menu is deactivated
        Assert.That(Time.timeScale, Is.EqualTo(1));
        Assert.That(gameController.pauseMenuScreen.activeSelf, Is.False);
    }

    
}
public class AnimationTests
{
    private GameObject playerObject;
    [SetUp]
    public void SetUp()
    {
        // Load the test scene
        SceneManager.LoadScene("game");

        // Find the player GameObject in the scene
        playerObject = GameObject.FindGameObjectWithTag("Player");
        

    }
    [Test]
    public void IdleStateTest()
    {
        // Arrange
        MovementAnimationTestObject movementObject = new MovementAnimationTestObject();

        // Act
        movementObject.UpdateMovementState(0, 0);

        // Assert
        Assert.AreEqual(MovementAnimationTestObject.MovementState.Idle, movementObject.CurrentState);
    }

    [Test]
    public void RunStateTest()
    {
        // Arrange
        MovementAnimationTestObject movementObject = new MovementAnimationTestObject();

        // Act
        movementObject.UpdateMovementState(1f, 0f);

        // Assert
        Assert.AreEqual(MovementAnimationTestObject.MovementState.Run, movementObject.CurrentState);
    }

    [Test]
    public void JumpStateTest()
    {
        // Arrange
        MovementAnimationTestObject movementObject = new MovementAnimationTestObject();

        // Act
        movementObject.UpdateMovementState(0f, 5f);

        // Assert
        Assert.AreEqual(MovementAnimationTestObject.MovementState.Jump, movementObject.CurrentState);
    }

    [Test]
    public void FallStateTest()
    {
        // Arrange
        MovementAnimationTestObject movementObject = new MovementAnimationTestObject();

        // Act
        movementObject.UpdateMovementState(0f, -5f);

        // Assert
        Assert.AreEqual(MovementAnimationTestObject.MovementState.Fall, movementObject.CurrentState);
    }

}
public class MovementAnimationTestObject
{
    public enum MovementState
    {
        Idle,
        Run,
        Jump,
        Fall
    }

    public MovementState CurrentState { get; private set; }

    public void UpdateMovementState(float dirX, float velocityY)
    {
        if (dirX > 0f)
        {
            CurrentState = MovementState.Run;
        }
        else if (dirX < 0f)
        {
            CurrentState = MovementState.Run;
        }
        else if (velocityY > .1f)
        {
            CurrentState = MovementState.Jump;
        }
        else if (velocityY < -.1f)
        {
            CurrentState = MovementState.Fall;
        }
        else
        {
            CurrentState = MovementState.Idle;
        }
    }
    public class MovementTests
    {
        private GameObject playerObject;
        private Rigidbody2D rb;
        private PlayerMovement playerController;

        [SetUp]
        public void SetUp()
        {
            // Load the test scene
            SceneManager.LoadScene("game");

            //Find the player GameObject in the scene
            playerObject = GameObject.FindGameObjectWithTag("Player");
            rb = playerObject.GetComponent<Rigidbody2D>();            
            playerController = playerObject.AddComponent<PlayerMovement>();
        }
        [UnityTest]
        public IEnumerator MoveLeft()
        {
            //playerObject = GameObject.FindGameObjectWithTag("Player");
            //rb = playerObject.GetComponent<Rigidbody2D>();
            //playerController = playerObject.AddComponent<PlayerMovement>();
            // Arrange
            float moveSpeed = 7f; // Set the move speed
            playerController.rb = playerObject.AddComponent<Rigidbody2D>();

            playerController.DirX = -1;
            // Act
            playerController.Move();

            // Wait for one frame to simulate physics update
            yield return null;

            // Assert
            Assert.AreEqual(new Vector2(-moveSpeed, rb.velocity.x), playerController.rb.velocity);
        }
        [UnityTest]
        public IEnumerator MoveRight()
        {
            //playerObject = GameObject.FindGameObjectWithTag("Player");
            //rb = playerObject.GetComponent<Rigidbody2D>();
            //playerController = playerObject.AddComponent<PlayerMovement>();
            // Arrange
            float moveSpeed = 7f; // Set the move speed
            playerController.rb = playerObject.AddComponent<Rigidbody2D>();
            playerController.DirX = 1;
            // Act
            playerController.Move();

            // Wait for one frame to simulate physics update
            yield return null;

            // Assert
            Assert.AreEqual(new Vector2(moveSpeed, rb.velocity.x), playerController.rb.velocity);
        }
        [UnityTest]
        public IEnumerator Jump_Test()
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            rb = playerObject.GetComponent<Rigidbody2D>();
            playerController = playerObject.AddComponent<PlayerMovement>();
            // Arrange
            //float jumpSpeed = 14f; // Set the jump speed
                                   




            // Act
            playerController.Jump();
            yield return null;
            // Assert
            Assert.AreEqual(new Vector2(rb.velocity.y, 0), playerController.rb.velocity);
        }
    }
}
