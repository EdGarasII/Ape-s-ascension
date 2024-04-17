using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Direction
{
    private GameObject playerObject;
    private Rigidbody2D rb;
    private PlayerMovement playerController;
    [SetUp]
    public void SetUp()
    {
        // Load the test scene
        SceneManager.LoadScene("game");

        // Find the player GameObject in the scene
        playerObject = GameObject.FindGameObjectWithTag("Player");
        rb = playerObject.GetComponent<Rigidbody2D>();
        //playerMovementScript = playerObject.GetComponent<PlayerMovement>();
        playerController = playerObject.AddComponent<PlayerMovement>();
    }
    [Test]
    public void Left()
    {
        playerController = playerObject.AddComponent<PlayerMovement>();
        // Arrange
        float moveSpeed = 7f; // Set the move speed
        playerController.rb = playerObject.AddComponent<Rigidbody2D>();

        playerController.DirX = -1;
        // Act
        playerController.Move();

        // Wait for one frame to simulate physics update
       

        // Assert
        Assert.AreEqual(new Vector2(-moveSpeed, 0), playerController.rb.velocity);
    }

}
