using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_Pigeon_Sim
{
    // base clase for map generation smallest unit is a "Block"

    public class Block
    {

        // the abstracted grid position
        private int positionX;
        private int positionY;
        private int coordX;
        private int coordY;

        // enum of building types
        public enum buildType {NullBlock, RoadHorizontal, RoadVertical, RoadCorner, Building, RoadT, RoadCross};
        private buildType blockType;

        private String modelPath;
        private String texturePath;

        public Block()
        {
            this.blockType = buildType.NullBlock;
        }

        public Block(int gridX, int gridY, buildType gridType, string modelFile, string textureFile)
        {
            this.positionX = gridX;
            this.positionY = gridY;
            this.blockType = gridType;
            this.modelPath = modelFile;
            this.texturePath = textureFile;

        }

        public void SetPositionX(int inputX)
        {
            this.positionX = inputX;
        }

        public int GetPositionX()
        {
            return this.positionX;
        }

        public void SetPositionY(int inputY)
        {
            this.positionY = inputY;
        }

        public int GetPositionY()
        {
            return this.positionY;
        }

        public void SetBlockType(buildType inputType)
        {
            this.blockType = inputType;
        }

        public buildType GetBlockType()
        {
            return this.blockType;
        }

        public void SetCoordX(int inputX)
        {
            this.coordX = inputX;
        }

        public int GetCoordX()
        {
            return this.coordX;
        }

        public void SetCoordY(int inputY)
        {
            this.coordY = inputY;
        }

        public int GetCoordY()
        {
            return this.coordY;
        }

        public void SetModelPath(string modelFile)
        {
            this.modelPath = modelFile;
        }

        public string GetModelPath()
        {
            return this.modelPath;
        }

        public void SetTexturePath(string textureFile)
        {
            this.texturePath = textureFile;
        }

        public string GetTexturePath()
        {
            return this.texturePath;
        }


    }
}
