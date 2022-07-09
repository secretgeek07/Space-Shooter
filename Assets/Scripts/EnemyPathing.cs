using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints;
    
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[index].transform.position;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (index <=waypoints.Count - 1)
        {
            var target = waypoints[index].transform.position;
            var movement = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, movement);
            if (transform.position == target)
            {
                index=index+1;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
