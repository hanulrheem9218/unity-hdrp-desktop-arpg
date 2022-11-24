using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("MAX COUNT:50")]
    [SerializeField] int objectCounts;
    [SerializeField] int spawnTime;
    [SerializeField] string modelName;
    [SerializeField] GameObject spawnModel;
    [SerializeField] GameObject parentManager;
    [SerializeField] Transform spawnLocation;
    [SerializeField] int maxRandomDistance;
    [SerializeField] enemyDataAI NPC_DATA;
    [SerializeField] GameObject damageObjectPreview;
    [SerializeField] int maxValueDiff;
    [SerializeField] int minValueDiff;

    //  private Transform[] objectTypes;
    [SerializeField] List<Transform> objectSpawnLocations = new List<Transform>();
    [SerializeField] List<Transform> objectTypes = new List<Transform>();
    void Start()
    {
        Invoke(nameof(objectTypeSpawnEnemyCounts), spawnTime);

        //Test
        spawnLocation.transform.position += new Vector3(2, 0, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Spawn Function that creates Linear Only 

    //spawn Function that dosent care about any ground detection field.
    private bool objectTypeSpawnEnemyCounts()
    {
        parentManager = this.gameObject;
        //   spawnLocation = parentManager.transform.GetChild(0).transform;

        // Random X and Z position thats all you need for specific position valeus.
        for (int i = 0; i <= (objectCounts - 1); i++)
        {
            // new x and z values

            Transform spawnLocationName = Instantiate(spawnLocation, parentManager.transform);
            spawnLocationName.name = "SpawnPosition" + i;
            objectSpawnLocations.Add(spawnLocationName.transform);
           // Debug.Log(objectSpawnLocations.Count.ToString());
            // adding Object Models
            //GameObject changePositionName = Instantiate()
        }
        for (int i = 0; i <= (objectCounts - 1); i++)
        {

            int spawnX = Random.Range(-maxRandomDistance, maxRandomDistance);
            int spawnZ = Random.Range(-maxRandomDistance, maxRandomDistance);
            objectSpawnLocations[i].transform.position = Vector3.zero;
            objectSpawnLocations[i].transform.position = new Vector3(spawnX + parentManager.transform.position.x, 0f, spawnZ + parentManager.transform.position.z);
        }
        for (int i = 0; i <= (objectCounts - 1); i++)
        {
            // AI models duplicates only detects empty assets
            if (objectSpawnLocations[i].transform.childCount <= 0)
            {
                Transform spawnModelName = Instantiate(spawnModel.transform, objectSpawnLocations[i].transform);
                spawnModelName.name = modelName + i;
                objectTypes.Add(spawnModelName.transform);
                enemyAImodelA spawnModelA = spawnModelName.transform.GetComponent<enemyAImodelA>();
             //   spawnModelA.playerDetectionRange = (int)NPC_DATA.DATA_playerDetectionRange;//(int)Random.Range((minValueDiff - NPC_DATA.DATA_playerDetectionRange),(maxValueDiff + NPC_DATA.DATA_playerDetectionRange));
                spawnModelA.coreDetectionRangeAI = (int)NPC_DATA.DATA_coreDetectionRangeAI;//(int)Random.Range((minValueDiff - NPC_DATA.DATA_coreDetectionRangeAI), (maxValueDiff + NPC_DATA.DATA_coreDetectionRangeAI));
                spawnModelA.enemyHealth = (int)NPC_DATA.DATA_enemyHealth;
                spawnModelA.damageStatusObject = NPC_DATA.DATA_damageStatusObject;
                spawnModelA.coreMeleeRangeAI = (float)NPC_DATA.DATA_coreMeleeRangeAI;
                spawnModelA.coreTurnSpeedAI = (int)NPC_DATA.DATA_coreTurnSpeedAI;// (int)Random.Range(NPC_DATA.DATA_coreTurnSpeedAI, (maxValueDiff + NPC_DATA.DATA_coreTurnSpeedAI));
                spawnModelA.coreMoveSpeedAI = (int)NPC_DATA.DATA_coreMoveSpeedAI;// (int)Random.Range((NPC_DATA.DATA_coreMoveSpeedAI- minValueDiff), (maxValueDiff + NPC_DATA.DATA_coreMoveSpeedAI));
                spawnModelA.coreShootRangeAI = (float)NPC_DATA.DATA_coreShootRangeAI;//(int)Random.Range((minValueDiff - NPC_DATA.DATA_coreShootRangeAI), (maxValueDiff + NPC_DATA.DATA_coreShootRangeAI));
                spawnModelA.coreMeleeRangeAI = (int)NPC_DATA.DATA_coreMeleeRangeAI;
                spawnModelA.backRadius = 10;

                // frontai, backai temproary
                spawnModelA.coreBackAI = objectSpawnLocations[i].transform;
                spawnModelA.coreFrontAI = objectSpawnLocations[i].transform;
            }
        }

        return true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(parentManager.transform.position, new Vector3(maxRandomDistance * 2, maxRandomDistance * 2, maxRandomDistance * 2));
    }
}
