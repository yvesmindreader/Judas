using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
namespace Judas
{
    class PlayGameScene
    {
        public PlayGameScene()
        {
            LoadBlocks();
            LoadPoints();
            SetTargetPoints();
            SetStage();
        }       

        int highScore;
        int target;
        int currentPoints;
        int stageNo;

        ObservableCollection<SingleBlock> judasBlocks;

        internal ObservableCollection<SingleBlock> JudasBlocks
        {
            get { return judasBlocks; }
            set { judasBlocks = value; }
        }
        public int HighScore
        {
            get { return highScore; }
            set { highScore = value; }
        }
        public int Target
        {
            get { return target; }
            set { target = value; }
        }
        public int CurrentPoints
        {
            get { return currentPoints; }
            set { currentPoints = value; }
        }
        public int StageNo
        {
            get { return stageNo; }
            set { stageNo = value; }
        }


        private void SetStage()
        {
            
        }

        private void SetTargetPoints()
        {
        }

        private void LoadPoints()
        {
        }

        public void LoadBlocks()
        {
           ObservableCollection<SingleBlock> blocks  = new ObservableCollection<SingleBlock>();

           long tick = DateTime.Now.Ticks;
           //Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
           Random ran = new Random();
           for (uint row = 0; row < CommonTypes.TOTALROWS; row++)
           {
               for (uint col = 0; col < CommonTypes.TOTALCOLUMNS; col++)
               {
                   SingleBlock newBlock = new SingleBlock();

                   newBlock.Rowpos = row + 1;
                   newBlock.Columnpos = col + 1;

                   newBlock.BlockColor = (CommonTypes.BlockColor) ran.Next((int)CommonTypes.BlockColor.BlockColorRed, (int)CommonTypes.BlockColor.BlockColorPurple + 1);
                   blocks.Add(newBlock);
               }
           }

           this.JudasBlocks = blocks;
        }
    }
}
