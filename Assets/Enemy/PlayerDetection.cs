using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    Enemy enemy;
    Player player => enemy.player;
     [SerializeField] TMP_Text detectionText;
    [SerializeField] float detectionDistance = 10;
    float detectionProgress = 0;
    float detectionTime = 2f; // How many seconds to detect the player 

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        detectionText.enabled = false;
    }

    public void ResetDetectionProgress()
    {
        detectionProgress = 0;
        detectionText.enabled = false;
    }

    public void CheckDistanceFromPlayer()
    {
        //get distance from the player
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // if distance is less than detection distance start detecting the player
        if(distance <= detectionDistance) 
        {
            detectionText.enabled = true;                                                   // enable the '!' on top of the enemy model
            detectionProgress += Time.deltaTime *  (1 / detectionTime);                     // increase the detection progress
            detectionText.color = Color.Lerp(Color.white,Color.red, detectionProgress);    // lerp the '!' color based on the progress
            if(detectionProgress >= 1)                                                      // if detection progress reaches 1
                enemy.ChangeState(Enemy.EEnemyState.Chasing);                               // detect player and change state to Chase
        }
        // if enemy is further than the detection distance and the detection progress 
        //     was greater than 0 reduce the detection progress
        else if (detectionProgress > 0)      
        {
            detectionProgress -= Time.deltaTime *  (1 / detectionTime);
            detectionText.color = Color.Lerp(Color.white,Color.red, detectionProgress);  // lerp the color back to white
        }
        else
            detectionText.enabled = false;  // disable the detection text '!'

    }
}
