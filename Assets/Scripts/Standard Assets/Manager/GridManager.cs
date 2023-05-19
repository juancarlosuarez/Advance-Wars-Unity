using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager sharedInstance;
    [SerializeField] private int _width, _height;

    public List<Tile> tileList;

    [SerializeField] Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;
    [HideInInspector] public List<Tile> gridWalkable;
    private List<long> coordinates;
    private List<long> coordinatesUnits;

    private Tile tileSelector;

    public List<Tile> enemyBarrack;
    public List<Tile> cityPlayer1;
    public List<Tile> cityNeutral;

    public List<Tile> player1list;
    public List<Tile> player2List;
    
    [SerializeField] GameObject parent;

    [SerializeField] private ListTilesReference allTilesInTheGrid;
    [SerializeField] private ListTilesReference allTilesWalkables;
    void Awake()
    {
        sharedInstance = this;
        List<long> numberString = new List<long>();
        List<long> unitStringgg = new List<long>();

        numberString.Add(80000200022200); numberString.Add(20022002220000);
        unitStringgg.Add(11121111111111); unitStringgg.Add(11111111111111);
        numberString.Add(80000000022000); numberString.Add(80022002200000);
        unitStringgg.Add(21221114111141); unitStringgg.Add(11111411112121);
        numberString.Add(80466666666666); numberString.Add(66666666666700);
        unitStringgg.Add(11211111111111); unitStringgg.Add(11111111111121);
        numberString.Add(84406300060020); numberString.Add(82230630200000);
        unitStringgg.Add(12211311113113); unitStringgg.Add(11131141122211);
        numberString.Add(22006002063022); numberString.Add(21200600200020);
        unitStringgg.Add(11211311113111); unitStringgg.Add(11111131111111);
        numberString.Add(22226002260021); numberString.Add(11120600200020);
        unitStringgg.Add(11111111113111); unitStringgg.Add(11111131111111);
        numberString.Add(80226022260211); numberString.Add(11120602222055);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(55556002262221); numberString.Add(11222600005555);
        unitStringgg.Add(11111311111111); unitStringgg.Add(11111131111111);
        numberString.Add(55556552060221); numberString.Add(11222625555555);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(22556555060021); numberString.Add(11555655555555);
        unitStringgg.Add(11111111113111); unitStringgg.Add(11111111111111);
        numberString.Add(20226555562221); numberString.Add(15555655555555);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(80026255565555); numberString.Add(55552622255555);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(20026102565555); numberString.Add(55522622222555);
        unitStringgg.Add(11111411111111); unitStringgg.Add(11111111111111);
        numberString.Add(22206222260555); numberString.Add(55222630222555);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(22006222260025); numberString.Add(55220602000255);
        unitStringgg.Add(11111111112111); unitStringgg.Add(11111111121111);
        numberString.Add(22206022062222); numberString.Add(55520602202255);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(11206002060220); numberString.Add(55520600222225);
        unitStringgg.Add(11111411111111); unitStringgg.Add(11111141111111);
        numberString.Add(11106000060000); numberString.Add(85500600000025);
        unitStringgg.Add(11111411114112); unitStringgg.Add(11111121111111);
        numberString.Add(11116122060020); numberString.Add(05550600000002);
        unitStringgg.Add(11111111114114); unitStringgg.Add(11111141114111);
        numberString.Add(22116111266666); numberString.Add(66666666666600);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(82226111163000); numberString.Add(30550222300630);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(80226301160022); numberString.Add(21555112220630);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(80226302160221); numberString.Add(11555111112602);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(82226302263011); numberString.Add(11155011122602);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(82026022263021); numberString.Add(22255500220600);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);
        numberString.Add(20006002263002); numberString.Add(23055530230630);
        unitStringgg.Add(11211211111111); unitStringgg.Add(11111111111111);
        numberString.Add(22007666666666); numberString.Add(66666666666600);
        unitStringgg.Add(11221111111111); unitStringgg.Add(11111111111111);
        numberString.Add(12000000300030); numberString.Add(00305520300600);
        unitStringgg.Add(11122211111111); unitStringgg.Add(11111111112121);
        numberString.Add(11200002002000); numberString.Add(20025522000700);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111112121);
        numberString.Add(11222222222222); numberString.Add(22225522000002);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111112211);
        numberString.Add(11122211111122); numberString.Add(21155222000222);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111211111);
        numberString.Add(11111111111111); numberString.Add(11115522220222);
        unitStringgg.Add(11111111111111); unitStringgg.Add(11111111111111);

        coordinates = GetIntArray(numberString, 2);
        coordinatesUnits = GetIntArray(unitStringgg, 2);

    }
    /* 555500230202302 555536666666670 555506113211212 555526311110101 555506666400000 555504246040300             
555555526355530 555555526255540 555555536255555 555555526255555 555555526455555 554055526255555
550455546055555 554055506030555 550322236660555 557011322063555 552321111160555 554003131163555
550466666660555 552030300304555 555555555555555  Mapa que quiero generar.

    grass0 mountain1 forest2 ciudad3 cuartel4 agua5 carretera6 base7
*/
    private List<long> GetIntArray(List<long> num, int numList)
    {
        List<long> listOfInts = new List<long>();
        List<long> listTemporal = new List<long>();
        List<long> anotherListMoreTemporal = new List<long>();
        int count = 1;
        for (int i = 0; i < num.Count; i++)
        {
            while (num[i] > 0)
            {
                anotherListMoreTemporal.Add(num[i] % 10);
                num[i] = num[i] / 10;
            }
            anotherListMoreTemporal.Reverse();
            foreach (long c in anotherListMoreTemporal)
            {

                listTemporal.Add(c);
            }
            anotherListMoreTemporal = new List<long>();
            if (count == numList)
            {
                foreach (long c in listTemporal)
                {
                    listOfInts.Add(c);
                }
                listTemporal = new List<long>();
                count = 1;
            }
            else count++;
        }
        return listOfInts;
    }

    public void GenerateGrid()
    {

        int count = 0;
        _tiles = new Dictionary<Vector2, Tile>();
        List<Tile> gridComplete = new List<Tile>();
        var setNeighbours = new TileSearchNeighBour();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                
                var spawnedTile = Instantiate(tileList[(int)coordinates[count]], new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.Init(x, y, count);

                _tiles[new Vector2(x, y)] = spawnedTile;

                gridComplete.Add(spawnedTile);
                
                if (coordinatesUnits[count] == 2) enemyBarrack.Add(spawnedTile);
                if (coordinatesUnits[count] == 3) cityNeutral.Add(spawnedTile);
                if (coordinatesUnits[count] == 4) cityPlayer1.Add(spawnedTile);
                count++;

                if (x == 2 && y == 22) tileSelector = spawnedTile;

                if (x == 2 && y == 22 || x == 1 && y == 22 || x == 2 && y == 23 || x == 2 && y == 21 ||
                    x == 3 && y == 22)
                {
                    print("que cojones");
                    player1list.Add(spawnedTile);
                }
                if (x == 2 && y == 20 || x == 3 && y == 21 || x == 26 && y == 5 || x == 27 && y == 5 || x == 25 && y == 5 || x == 26 && y == 4 || x == 26 && y == 6) player2List.Add(spawnedTile);
                if (spawnedTile.isWalkable)
                {
                    gridWalkable.Add(spawnedTile);
                }
                spawnedTile.transform.SetParent(parent.transform);
            }
        }
        allTilesWalkables.reference = gridWalkable;
        allTilesInTheGrid.reference = gridComplete;
        //PathFinding.sharedInstance.SetList(allTilesWalkables.tilesListReference);
        foreach (var c in gridComplete)
        {
            setNeighbours.CalculateNeightbourTile(c);
        }
        //TileSearcherNeightbours.SetListGrid(gridComplete, gridWalkable);
        _cam.position = new Vector3(tileSelector.GetTilePosition().x, tileSelector.GetTilePosition().y, _cam.position.z);
        GameManager.sharedInstance.ChangeState(StateSpawnGamePlayElements.SpawnHeroes);
    }
    
    public Tile GetHeroSpawnTile(int count)
    {
        return player1list[count];

    }

    public Tile GetPlayer2Tile(int count)
    {
        return player2List[count];
    }
    public Tile GetBarrackAlly(int count)
    {
        return enemyBarrack[count];
    }
    public Tile GetCityNeutral(int count)
    {
        return cityNeutral[count];
    }
    public Tile GetCityPlayer1(int count)
    {
        return cityPlayer1[count];
    }
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
    
}