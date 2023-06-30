
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class gridplacement : MonoBehaviour
{
    [SerializeField] List<GameObject> blockPrefabs;

    [SerializeField] Transform OtherBlocksParent;

    [SerializeField] float zPos;

    // public List<Vector2> blockPlacements = new List<Vector2>();

    HashSet<List<int>> mapList = new HashSet<List<int>>();

    [SerializeField] private int itemID = 0;
    [SerializeField] private int rot = 0;

    [SerializeField] private ToggleGroup toggleGroup;

    [SerializeField] private bool disablePhysicsOnStart = true; 

    List<block> oldblockpos = new List<block>();

    bool inPlayMode;

    [SerializeField] private GameObject player;


    [Header("Camaras")]


    [SerializeField] private GameObject editorCam;

    [SerializeField] private GameObject playingCam;


    [SerializeField] private Transform msEffectParent;
    [SerializeField] private iTween.EaseType EaseType;
    [Header("UI")]

    [SerializeField] TMP_InputField MapNameInputField;

    [SerializeField] Transform MapsParent;

    [SerializeField] GameObject MapButtonPrefab;

    [SerializeField] tileMapHandler tileMapHandler;


    // public SimulationMode2D simulationMode;
    string currentMapName;



    private int oldItemId;

    private keyToggle keyToggle;

    private int removeLayer;

    [SerializeField] private TrailRenderer_Local trailRenderer;

    private Camera _cam;
    [SerializeField] mapLoader ML;

    void addBlockPrefab(GameObject b){
         GameObject msEffectBlock = Instantiate(b, msEffectParent.position, msEffectParent.rotation, msEffectParent);

        BoxCollider2D boxCollider = msEffectBlock.GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            Destroy(boxCollider);
        }
        Rigidbody2D rb = msEffectBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Destroy(rb);
        }
    }


    void Awake()
    {
        _cam = Camera.main;

    }

    void Start()
    {
        if(disablePhysicsOnStart){
            Physics2D.simulationMode = SimulationMode2D.Script;
            Debug.Log("Disabled physics");
        }
        
        keyToggle = toggleGroup.GetComponent<keyToggle>();
        ListMapsToLoad();
        changeCurrentMap(SaveSystem.getDeafultMapName());

        for (int i = msEffectParent.childCount; i < blockPrefabs.Count; i++)
        {
            addBlockPrefab(blockPrefabs[i]);
        }

        // initilize variables



    }






    void clearChildren(Transform parent){
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }



    // -- maps selection ui --
    void ListMapsToLoad(){
        clearChildren(MapsParent);
        string[] mapNames = SaveSystem.getAllSavedMapNames();
// MapButtonPrefab, MapsParent
        for (int i = 0; i < mapNames.Length; i++)
        {
            addButtonToMapList(MapButtonPrefab, MapsParent, mapNames[i]);
        }
    }

    void addButtonToMapList(GameObject buttonPrefab, Transform mapListParent, string mapName){
        GameObject newButton = Instantiate(buttonPrefab, mapListParent);
        changeTextOfButton(newButton, mapName);
        newButton.GetComponent<Button>().onClick.AddListener(() => changeCurrentMap(mapName));
        newButton.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => removeMap(mapName));
        
    }
    void changeTextOfButton(GameObject button, string newText){
        button.GetComponentInChildren<TMP_Text>().text = newText;
    }

    // change the current map

    void changeCurrentMap(string mapName){
        currentMapName = mapName;
        mapList = ML.importMapFromFile(mapName);
    }
    public void saveMap(){
        if(currentMapName is "" or null) return;
        Debug.Log("saving map to name: "+ currentMapName);
        exportMapAsString();
            // ListMapsToLoad(MapButtonPrefab, MapsParent);
    }

    // add a new map (mapName being the name for the new map). ////returns false if fails fx. if the map name already exists

    public void addMap(){
        if(MapNameInputField.text.Replace(" ", "-") == "") return;

        mapList = ML.clearMap();

        currentMapName = MapNameInputField.text.Replace(" ", "-");
        saveMap();
        ListMapsToLoad();
    }
    //clones a map !! not implemented yet DO NOT USE
    public void cloneMap(string NewMapName){

    }

    // removes the map with the mapNmae
    public void removeMap(string MapName){
        if(currentMapName == MapName){
            currentMapName = "";
        }
        SaveSystem.removeFile(MapName);
        ListMapsToLoad();
    }

    


    Vector2Int getMsPos(){
        Vector3 mousePosition = Input.mousePosition;
        if(_cam == null) _cam = Camera.main;
        Vector3 worldPosition = _cam.ScreenToWorldPoint(mousePosition);
        return new Vector2Int(
            Mathf.RoundToInt(worldPosition.x / 2.5f),
            Mathf.RoundToInt(worldPosition.y / 2.5f));
    }

    Vector2 blockToWorldPos(Vector2Int pos){
        return new Vector2(pos.x * 2.5f, pos.y * 2.5f);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            oldItemId = itemID;
            keyToggle.enableToggle(-1);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            print("you unshifted"); 
            keyToggle.enableToggle(oldItemId);
            
        }
        // blockType += Mathf.RoundToInt(Input.mouseScrollDelta.y);
        if(!inPlayMode){

            if(Input.GetMouseButtonUp(0)){
                removeLayer = -1;

                // reseting remove layer when not clicking
            }

            Vector2Int msPos = getMsPos();
            int x = msPos.x;
            int y = msPos.y;
            if(!EventSystem.current.IsPointerOverGameObject()){
                
                if(Input.GetMouseButton(0) ){
                    

                    if(itemID == -1){
                        removeBlock(x, y);
                        
                    }
                    else {
                        addBlock(x, y, itemID, rot);
                    }

                }
                if(!Input.GetKey(KeyCode.LeftControl)){
                    rot = (rot + Mathf.RoundToInt(Input.mouseScrollDelta.y)) % 4 ;
                }
            }
            

            if(itemID != -1){

                GameObject child = msEffectParent.GetChild(itemID).gameObject;
                // child.SetActive(false);
                iTween.RotateTo(child, iTween.Hash("z", rot * 90, "time", 0.1));
                iTween.MoveTo(child, iTween.Hash("x", x * 2.5f, "y", y * 2.5f, "time", 0.1, "easetype", EaseType));  
            }
        }
        
    }



    public string exportMapAsString(){
        String MapString = "";
        foreach (List<int> row in mapList)
        {
            foreach (int element in row)
            {
                // Do something with the element
                
                MapString += element;
                MapString += ' ';

            
            
            }
            MapString += ',';
        }
        
        SaveSystem.WriteString(currentMapName, MapString);
        // SaveSystem.WriteString(MapNameInputField.text.Replace(" ", "-"), MapString);

        return MapString;
    }



    void importMapAsString(string mapStr){
        foreach (string bstring in mapStr.Split(','))
        {
            if(bstring == "") continue;

            string[] elements = bstring.Split(' ');
            int[] numbers = new int[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                if(elements[i] == "") continue;
                numbers[i] = int.Parse(elements[i]);
            }
            addBlock(
                numbers[0],
                numbers[1],
                numbers[2],
                numbers[3]
            );
        }

    }

    void removeBlock(int x, int y){
        if(removeLayer != 0){
            for (int b = 0; b < OtherBlocksParent.transform.childCount; b++)
            {
                Transform block = OtherBlocksParent.transform.GetChild(b);

                if(block.position.x == x * 2.5f && block.position.y == y * 2.5f){
                    Destroy(block.gameObject);

                    // var itemToRemove = mapList.Where(r => r[0] == x && r[1] == y);
                    mapList.RemoveWhere(r => r[0] == x && r[1] == y && r[2] != 0);
                    exportMapAsString();
                    removeLayer = 1;
                    return;
                }


            }
        }
        if(removeLayer == 1) return;
        removeLayer = 0;

        tileMapHandler.changeBlock(x, y, false);
        mapList.RemoveWhere(r => r[0] == x && r[1] == y && r[2] == 0);
    }



    void addBlock(int x, int y, int blockType, int Rotation){
        Vector3 worldPosition = new Vector3(x * 2.5F, y * 2.5F, zPos);


        List<int> block = new List<int>() {
                                x, 
                                y,
                                blockType,
                                Rotation
                            };

        if(mapList.Any(x => x.SequenceEqual(block))) return;


        mapList.Add(block);


        // MapString = exportMapAsString();
        
        if(blockType != 0){
            worldPosition += Vector3.back * 2;
            Instantiate(blockPrefabs[blockType], worldPosition, Quaternion.Euler(0, 0, Rotation * 90), OtherBlocksParent);
            return;
        }
        
        tileMapHandler.changeBlock(x, y, true);

            // if(child.)
    }

    bool isBlockHere(Vector3 pos, List<int> bb){
        return pos.x == bb[0] && pos.y == bb[1];
    }

    

    public void updateSelectedItem(){

        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();

        // msEffectParent.GetChild(itemID).gameObject.SetActive(false);
        updateSelectedItem(toggle.transform.GetSiblingIndex());

        
    }

    public void updateSelectedItem(int NewitemID){
        if(itemID != -1){
            msEffectParent.GetChild(itemID).gameObject.SetActive(false);
        }
        itemID = NewitemID;
        if(itemID == -1) return;
        GameObject obj = msEffectParent.GetChild(itemID).gameObject;
        obj.SetActive(true);
        obj.transform.position = blockToWorldPos(getMsPos());



    }
    
    public void StartOrStopSim(){
        if(inPlayMode) StopSim();
        else StartSim();
    }

    void StartSim(){

        

        swichCam(true);
        oldblockpos.Clear();

        for (int b = 0; b < OtherBlocksParent.transform.childCount; b++)
        {
            GameObject block = OtherBlocksParent.transform.GetChild(b).gameObject;

            if(block.GetComponent<Rigidbody2D>() != null){
                oldblockpos.Add(new block(block.gameObject));

            }

        }

        oldblockpos.Add(new block(player));

        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        
        msEffectParent.GetChild(itemID).gameObject.SetActive(false);

        trailRenderer.enabled = true;


    }
    void StopSim(){
        trailRenderer.enabled = false;

        foreach (block b in oldblockpos)
        {
            b.resetTransform();
        }
        swichCam(false);
        Physics2D.simulationMode = SimulationMode2D.Script;
        
        msEffectParent.GetChild(itemID).gameObject.SetActive(true);

        

    }

    void swichCam(bool isPlaying){
        inPlayMode = isPlaying;
        editorCam.SetActive(!isPlaying);
        playingCam.SetActive(isPlaying);
    }
}








class block{

    private Vector3 pos;
    private Quaternion rot;
    private GameObject gameObject;
    private Rigidbody2D rb;
    public block(GameObject igameObject){
        pos = igameObject.transform.position;
        rot = igameObject.transform.rotation;
        rb = igameObject.GetComponent<Rigidbody2D>();
        gameObject = igameObject;
    }

    public void resetTransform(){
        gameObject.transform.position = pos;
        gameObject.transform.rotation = rot;
        rb.velocity = Vector2.zero;

    }
}