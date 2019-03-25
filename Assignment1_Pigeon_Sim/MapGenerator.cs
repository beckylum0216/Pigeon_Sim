using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Assignment1_Pigeon_Sim
{
    class MapGenerator
    {
        int sizeX = 0;
        int sizeY = 0;
        Block[,] gridMap;
        

        public MapGenerator(int inputX, int inputY)
        {
            sizeX = inputX;
            sizeY = inputY;
            gridMap = new Block[sizeX, sizeY];
        }

        // generates the map based on a typical chinese chequerboard pattern
        public void SetMap()
        {

            // sets the initial "block consisting of building and adjacent road
            for(int ii = 0; ii < sizeX; ii++)
            {
                for(int jj = 0; jj < sizeY; jj++)
                {
                    if(ii % 2 == 1 && jj % 2 == 1)
                    {
                        string modelPath = "Models/city_residential_03";
                        string texturePath = "Maya/sourceimages/city_residential_03_dif";
                        Block tempBlock = new Block(ii, jj, Block.buildType.Building, modelPath, texturePath);
                        gridMap[ii, jj] = tempBlock;

                        if((ii - 1) >= 0)
                        {
                            string modelFile = "Models/city_road_02";
                            string textureFile = "Maya/sourceimages/city_road_02_dif";
                            Block roadWest = new Block(ii - 1, jj, Block.buildType.RoadVertical, modelFile, textureFile);
                            gridMap[ii - 1, jj] = roadWest;
                        }
                        
                        if((jj - 1) >= 0)
                        {
                            string modelFile = "Models/city_road_02";
                            string textureFile = "Maya/sourceimages/city_road_02_dif";
                            Block roadNorth = new Block(ii, jj - 1, Block.buildType.RoadHorizontal, modelFile, textureFile);
                            gridMap[ii, jj - 1] = roadNorth;
                        }

                        if((ii + 1) < sizeX)
                        {
                            string modelFile = "Models/city_road_02";
                            string textureFile = "Maya/sourceimages/city_road_02_dif";
                            Block roadEast = new Block(ii + 1, jj, Block.buildType.RoadVertical, modelFile, textureFile);
                            gridMap[ii + 1, jj] = roadEast;
                        }

                        if((jj + 1) < sizeY)
                        {
                            string modelFile = "Models/city_road_02";
                            string textureFile = "Maya/sourceimages/city_road_02_dif";
                            Block roadSouth = new Block(ii, jj + 1, Block.buildType.RoadHorizontal, modelFile, textureFile);
                            gridMap[ii, jj + 1] = roadSouth;
                        }
                        
                    }
                }
            }

            // searches for empty grid spaces and populates it with a suitable asset
            for (int ii = 0; ii < sizeX; ii++)
            {
                for (int jj = 0; jj < sizeY; jj++)
                {
                    if (gridMap[ii, jj] == null)
                    {
                        int junctions = FindNeighbours(ii, jj, gridMap);

                        Debug.WriteLine("Junctions: " + junctions);

                        if (junctions == 2)
                        {
                            string modelFile = "Models/city_road_02";
                            string textureFile = "Maya/sourceimages/city_road_02_dif";
                            gridMap[ii, jj] = new Block(ii, jj, Block.buildType.RoadCorner, modelFile, textureFile);
                        }
                        else if(junctions == 3)
                        {
                            string modelFile = "Models/city_road_02";
                            string textureFile = "Maya/sourceimages/city_road_02_dif";
                            gridMap[ii, jj] = new Block(ii, jj, Block.buildType.RoadT, modelFile, textureFile);
                        }
                        else
                        {
                            string modelFile = "Models/city_road_02";
                            string textureFile = "Maya/sourceimages/city_road_02_dif";
                            gridMap[ii, jj] = new Block(ii, jj, Block.buildType.RoadCross, modelFile, textureFile);
                        }
                    }
                }
            }
        }

        // this functions helps the program to find the right junction for each empty space
        // does not take into account orientation
        private int FindNeighbours(int gridX, int gridY, Block[,] inputGrid)
        {
            int junction = 0;

            if((gridX - 1) >= 0)
            {
                if (inputGrid[gridX - 1, gridY].GetBlockType() == Block.buildType.RoadHorizontal)
                {
                    junction += 1;
                }
            }
            
            if((gridY - 1) >= 0)
            {
                if (inputGrid[gridX, gridY - 1].GetBlockType() == Block.buildType.RoadVertical)
                {
                    junction += 1;
                }
            }

            if((gridX + 1) < sizeX)
            {
                if(inputGrid[gridX + 1, gridY].GetBlockType() == Block.buildType.RoadHorizontal)
                {
                    junction += 1;
                }
            }
            
            if((gridY + 1) < sizeY)
            {
                if (inputGrid[gridX, gridY + 1].GetBlockType() == Block.buildType.RoadVertical)
                {
                    junction += 1;
                }
            }
            
            return junction;
           
        }

        public void SetCoords()
        {
            for(int ii = 0; ii < sizeX; ii++)
            {
                for(int jj = 0; jj < sizeY; jj++)
                {
                    int tempX = gridMap[ii, jj].GetPositionX() * 3;
                    int tempY = gridMap[ii, jj].GetPositionY() * 3;

                    gridMap[ii, jj].SetCoordX(tempX);
                    gridMap[ii, jj].SetCoordY(tempY);

                }
            }
        }

        public Block[,] GetGridMap()
        {
            return gridMap;
        }
        
        // output function for debugging
        public void PrintGrid()
        {
            for(int ii = 0; ii < sizeX; ii++)
            {
                for(int jj = 0; jj < sizeY; jj++)
                {
                    Debug.Write(gridMap[jj, ii].GetBlockType() + " ");
                }

                Debug.Write("\n");
            }
        }

        public void PrintCoords()
        {
            for (int ii = 0; ii < sizeX; ii++)
            {
                for (int jj = 0; jj < sizeY; jj++)
                {
                    Debug.WriteLine("Coord X: " + gridMap[ii,jj].GetCoordX() + " Y: "+ gridMap[ii,jj].GetCoordY());
                }
            }
        }



    }

    
}
