using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Aurora
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Package : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //Blocks in Package
        Block topLeft;
        Block topRight;
        Block bottomLeft;
        Block bottomRight;

        //blocks that have been set on the playing field while a package is still in play
        List<BasicModel> setBlocks;

        float blockSize;
        float moveUnit;

        public Package(Game game, float bSize)
            : base(game)
        {
            setBlocks = new List<BasicModel>();
            blockSize = bSize;
            moveUnit = blockSize * 2;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            
            //Setting the Names of the blocks for debugging purposes
            

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //Updating the blocks
            topRight.Update(gameTime);
            bottomRight.Update(gameTime);
            topLeft.Update(gameTime);
            bottomLeft.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                //clearing the set block list of the reset button is pressed
                //in this case the reset button is J, because J is just a magnificent letter
                setBlocks.Clear();
            }

            //once all the blocks have been placed on the field from the package, 
            //clearing the list for the next round of block setting fun
            if (setBlocks.Count == 4)
                setBlocks.Clear();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            //Drawing the blocks in the package
            topRight.Draw(((Game1)Game).camera);
            bottomRight.Draw(((Game1)Game).camera);
            topLeft.Draw(((Game1)Game).camera);
            bottomLeft.Draw(((Game1)Game).camera);
            
            base.Draw(gameTime);
        }

        /// <summary>
        /// sets the initial position of the package based off of an initial position 
        /// </summary>
        /// <param name="position"></param>
        public void setInitialPosition(Vector3 position)
        {
            //setting the position of top left block
            topLeft.SetLocation(new Vector3(position.X - blockSize, position.Y + blockSize, position.Z));

            //setting the position of top right block
            topRight.SetLocation(new Vector3(position.X + blockSize, position.Y + blockSize, position.Z));

            //setting the position of bottom left block
            bottomLeft.SetLocation(new Vector3(position.X - blockSize, position.Y - blockSize, position.Z));

            //setting the position of bottom right block
            bottomRight.SetLocation(new Vector3(position.X + blockSize, position.Y - blockSize, position.Z));
        }

        public void MoveLeft(List<BasicModel> models)
        {
            //Vector3 tempLocation;

            //Top Left
            if (topLeft.GetState() != Block.BlockState.BLOCKY)
            {
                //invoking helper function to move blocks and check for collisions
                horizontalHelper(ref topLeft, models, "left");
            }

            //Top Right Check
            if (topRight.GetState() != Block.BlockState.BLOCKY)
            {
                //invoking helper function to move blocks and check for collisions
                horizontalHelper(ref topRight, models, "left");
            }

            //Bottom Left Check
            if (bottomLeft.GetState() != Block.BlockState.BLOCKY)
            {
                horizontalHelper(ref bottomLeft, models, "left");
            }

            //Bottom Right Check
            if (bottomRight.GetState() != Block.BlockState.BLOCKY)
            {
                horizontalHelper(ref bottomRight, models, "left");
            }       
        }

        public void MoveRight(List<BasicModel> models)
        {
            //Top Right Check
            if (topRight.GetState() != Block.BlockState.BLOCKY)
            {
                //invoking helper function to move blocks and check for collisions
                horizontalHelper(ref topRight, models, "right");
            }

            //Top Left
            if (topLeft.GetState() != Block.BlockState.BLOCKY)
            {
                //invoking helper function to move blocks and check for collisions
                horizontalHelper(ref topLeft, models, "right");
            }
            //Bottom Right Check
            if (bottomRight.GetState() != Block.BlockState.BLOCKY)
            {
                horizontalHelper(ref bottomRight, models, "right");
            }   


            //Bottom Left Check
            if (bottomLeft.GetState() != Block.BlockState.BLOCKY)
            {
                horizontalHelper(ref bottomLeft, models, "right");
            }

    
        }

        /// <summary>
        /// Helper function for moving left and right
        /// </summary>
        private void horizontalHelper(ref Block block, List<BasicModel> m, string direction)
        {

            Vector3 tempLocation;
            Block tempBlock;
            bool isCollision;

            if (direction == "left")
            {
                tempLocation = block.GetLocation();
                tempLocation.X -= moveUnit;
                tempBlock = new Block(Game, tempLocation);
            }
            else
            {
                //moving the block down and setting its location
                tempLocation = block.GetLocation();
                tempLocation.X += moveUnit;
                tempBlock = new Block(Game, tempLocation);
            }

            //Checking for Collisions within the blocks on the field
            isCollision = CheckForCollisions(m, ref tempBlock);

            //if there hasn't been a collision with the blocks already on the field
            //checking for collisions within the setBlocks(if applicable)
            if ( !isCollision  && setBlocks.Count > 0)
                isCollision = CheckForCollisions(setBlocks, ref tempBlock);

            if (isCollision == true)
            {
                //adding the block to the setblock list
                setBlocks.Add(block);
                block.SetState(Block.BlockState.BLOCKY);

                if (block == bottomLeft && bottomRight.GetState() != Block.BlockState.BLOCKY)
                {
                    setBlocks.Add(bottomRight);
                    bottomRight.SetState(Block.BlockState.BLOCKY);
                }

                else if (block == topLeft && topRight.GetState() != Block.BlockState.BLOCKY)
                {
                    setBlocks.Add(topRight);
                    topRight.SetState(Block.BlockState.BLOCKY);
                }

                else if (block == bottomRight && bottomLeft.GetState() != Block.BlockState.BLOCKY)
                {
                    setBlocks.Add(bottomLeft);
                    bottomLeft.SetState(Block.BlockState.BLOCKY);
                }

                else if (block == topRight && topLeft.GetState() != Block.BlockState.BLOCKY)
                {
                    setBlocks.Add(topLeft);
                    topLeft.SetState(Block.BlockState.BLOCKY);
                }
            }
            //if there is no collision then moving the block up/down
            else if (isCollision == false && block.GetState() != Block.BlockState.BLOCKY)
            {
                block.SetLocation(tempLocation);
            }
        }

        public void MoveUp(List<BasicModel> models)
        {
            //Top Left
            if (topLeft.GetState() != Block.BlockState.BLOCKY)
            {
                //invoking helper function to move blocks and check for collisions
                VerticalHelper(ref topLeft, models, "up");
            }

            //Top Right Check
            if (topRight.GetState() != Block.BlockState.BLOCKY)
            {
                //invoking helper function to move blocks and check for collisions
                VerticalHelper(ref topRight, models, "up");
            }

            //Bottom Left Check
            if (bottomLeft.GetState() != Block.BlockState.BLOCKY)
            {
                VerticalHelper(ref bottomLeft, models, "up");
            }

            //Bottom Right Check
            if (bottomRight.GetState() != Block.BlockState.BLOCKY)
            {
                VerticalHelper(ref bottomRight, models, "up");
            }
        }

        public void MoveDown(List<BasicModel> models)
        {
            //Bottom Left Check
            //Since this is the first check, the adjacent block needs to be checked as well in order to make
            //sure that there is no
            if (bottomLeft.GetState() != Block.BlockState.BLOCKY)
            {


                VerticalHelper(ref bottomLeft, models, "down");
            }

            //Top Left
            if (topLeft.GetState() != Block.BlockState.BLOCKY)
            {
                //invoking helper function to move blocks and check for collisions
                VerticalHelper(ref topLeft, models, "down");
            }

            //Bottom Right Check
            if (bottomRight.GetState() != Block.BlockState.BLOCKY)
            {
                VerticalHelper(ref bottomRight, models, "down");
            }

            //Top Right Check
            if (topRight.GetState() != Block.BlockState.BLOCKY)
            {
                //invoking helper function to move blocks and check for collisions
                VerticalHelper(ref topRight, models, "down");
            }
            
        }

        /// <summary>
        /// Up/Down helper function
        /// </summary>
        /// <param name="block">block that will be moved</param>
        /// <param name="m">list of models on the field</param>
        /// <param name="direction">direction of movement</param>
        private void VerticalHelper(ref Block block, List<BasicModel> m, string direction)
        {
            Vector3 tempLocation;
            Block tempBlock;
            bool isCollision;

            if (direction == "up")
            {
                tempLocation = block.GetLocation();
                tempLocation.Y += moveUnit;
                tempBlock = new Block(Game, tempLocation);
            }
            else
            {
                //moving the block down and setting its location
                tempLocation = block.GetLocation();
                tempLocation.Y -= moveUnit;
                tempBlock = new Block(Game, tempLocation);
            }


            //Checking for Collisions within the blocks on the field
            isCollision = CheckForCollisions(m, ref tempBlock);

            //if there hasn't been a collision with the blocks already on the field
            //checking for collisions within the setBlocks(if applicable)
            if ( !isCollision  && setBlocks.Count > 0)
                isCollision = CheckForCollisions(setBlocks, ref tempBlock);

            if (isCollision == true)
            {
                //adding the block to the setblock list
                setBlocks.Add(block);
                block.SetState(Block.BlockState.BLOCKY);

                if (block == topRight && bottomRight.GetState() != Block.BlockState.BLOCKY)
                {
                    //setting the blocks state to BLOCKy and then moving the appropriate units
                    setBlocks.Add(bottomRight);
                    bottomRight.SetState(Block.BlockState.BLOCKY);
                }

                else if (block == topLeft && bottomLeft.GetState() != Block.BlockState.BLOCKY)
                {
                    //setting the blocks state to BLOCKy and then moving the appropriate units
                    setBlocks.Add(bottomLeft);
                    bottomLeft.SetState(Block.BlockState.BLOCKY);
                }
                else if (block == bottomRight && topRight.GetState() != Block.BlockState.BLOCKY)
                {
                    setBlocks.Add(topRight);
                    topRight.SetState(Block.BlockState.BLOCKY);
                }

                else if (block == bottomLeft && topLeft.GetState() != Block.BlockState.BLOCKY)
                {
                    setBlocks.Add(topLeft);
                    topLeft.SetState(Block.BlockState.BLOCKY);
                }
            }
            //if there is no collision then moving the block up/down
            else if (isCollision == false && block.GetState() != Block.BlockState.BLOCKY)
            {
                block.SetLocation(tempLocation);
            }
        }

        public void CreatePackage()
        {
            //Top Left Block
            CreatePackageHelper(ref topLeft);

            //Top Right Block
            CreatePackageHelper(ref topRight);

            //Bottom Left Block
            CreatePackageHelper(ref bottomLeft);

            //Bottom Right block
            CreatePackageHelper(ref bottomRight);

            topLeft.SetName("TopLeft");
            topRight.SetName("TopRight");
            bottomLeft.SetName("BottomLeft");
            bottomRight.SetName("BottomRight");

        }
        private void CreatePackageHelper(ref Block block)
        {
            if (((Game1)Game).rand.Next(2) == 1)
            {
                block = new Block(Game, Game.Content.Load<Model>(@"Models\blue_box"));
            }
            else
            {
                block = new Block(Game, Game.Content.Load<Model>(@"Models\rboxnew"));
            }
        }

        public void RotatePackageRight()
        {
            //Switching first block with third block
            Vector3 tempBlockLocation1 = topLeft.GetLocation();
            Vector3 tempBlockLocation2 = bottomLeft.GetLocation();
            topLeft.SetLocation(tempBlockLocation2);

            //Switching third block with fourth block
            tempBlockLocation2 = bottomRight.GetLocation();
            bottomLeft.SetLocation(tempBlockLocation2);

            //Switching fourth block with second block
            tempBlockLocation2 = topRight.GetLocation();
            bottomRight.SetLocation(tempBlockLocation2);

            //Switching second block with first block
            topRight.SetLocation(tempBlockLocation1);

        }

        public void RotatePackageLeft()
        {
            //Switching first block with second block
            Vector3 tempBlockLocation1 = topLeft.GetLocation();
            Vector3 tempBlockLocation2 = topRight.GetLocation();
            topLeft.SetLocation(tempBlockLocation2);

            //Switching second block with fourth block
            tempBlockLocation2 = bottomRight.GetLocation();
            topRight.SetLocation(tempBlockLocation2);

            //Switching fourth block with third block
            tempBlockLocation2 = bottomLeft.GetLocation();
            bottomRight.SetLocation(tempBlockLocation2);

            //Switching third block with first block
            bottomLeft.SetLocation(tempBlockLocation1);
        }

        public BasicModel GetBlock(int block)
        {
            if (block == 0)
                return topLeft;
            else if (block == 1)
                return topRight;
            else if (block == 2)
                return bottomLeft;
            else if (block == 3)
                return bottomRight;

            //if the number is over 3 then return nothing
            return null;
        }

        private bool CheckForCollisions(List<BasicModel>models, ref Block block)
        {
            //creating and setting the isCollision flag to automatically be false
            //Innocent until proven guilty
            //bool isCollision = false;

            //checking the blocks that have already been set in case there is a possible collision
            foreach (Block b in models)
            {
                if (b.CollidesWith(block.GetLocation().X, block.GetLocation().Y))
                {
                    //stop drop and roll ya dig
                    //block.SetState(Block.BlockState.BLOCKY);                    

                    //collision has been found to notating that and breaking from the loop
                    return true;
                }
            }

            //returning false on all other occasions
            return false;
        }
    }
}
