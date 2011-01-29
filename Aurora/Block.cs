using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace Aurora
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Block : BasicModel
    {
        //Creating a Block State of Mind
        public enum BlockState { BLOCKY, MOVING, PACKAGE, CONNECTED, DISAPPEARING };

        //State Blockery
        private BlockState state;
        private BlockState prevState;


        //location
        //public Vector3 location;

        //**Begin Movement Variables**//

        //when the block is to be placed on the field this is its destination
        public Vector3 destination { get; protected set; }

        //steps used for movement calculation
        float xSteps;
        float ySteps;

        //represents the total amount of game updates required to complete a movement
        int moveUpdates;

        //**End Movement Variables**//
        
        public Block(Game game)
            : base(game)
        {
            state = BlockState.PACKAGE;
            
        }
        public Block(Game game, Vector3 loc)
            : base(game)
        {
            location = loc;
        }

        public Block(Game game, Model m)
            :base(game)
        {
            model = m;
            state = BlockState.PACKAGE;
            location = new Vector3(0.0f, 0.0f, -50.0f);
        }

        public Block(Game game, Model m, Vector3 loc)
            : base(game)
        {
            model = m;
            state = BlockState.PACKAGE;
            location = loc;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            location = new Vector3(0.0f, 0.0f, -50.0f);

            base.Initialize();
        }

        public override void SetDestination(Vector3 newDestination)
        {
            // Setting the new destination of the block
            destination = newDestination;
        }

        //Getters and Setters
        public void SetState(BlockState newState)
        {
            prevState = state;
            state = newState;
        }

        public BlockState GetState()
        {
            return state;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            if (state == BlockState.PACKAGE)
            {
                /*
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    //if the down key is selecting updating the y position of the model
                    location.Y = location.Y - 13.0f;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    //if the down key is selecting updating the y position of the model
                    location.Y = location.Y + 13.0f;
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    //if the left key is selected updating the x position of the model
                    location.X = location.X - 13.0f;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    //if the right key is selected updating the x position of the model
                    location.X = location.X + 13.0f;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    //if the right key is selected updating the x position of the model
                    location.Z = location.Z - 13.0f;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    //if the right key is selected updating the x position of the model
                    location.Z = location.Z + 13.0f;
                }
*/
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    //state = BlockState.MOVING;
                }
                
            }

            if (state == BlockState.CONNECTED)
            {

            }
            if (state == BlockState.DISAPPEARING)
            {

            }
            if (state == BlockState.MOVING)
            {
                //moving the block one unit every update.  Lets see how this works out
                //First Calculating the stuff necessary for automoving blocks(hmm I may use that )
                if (prevState != state)
                {
                    if (Math.Abs(destination.X) > Math.Abs(destination.Y))
                    {
                        //set moveUpdates
                        moveUpdates = Math.Abs((int)destination.X);
                    }

                    else
                    {
                        moveUpdates = Math.Abs((int)destination.Y);
                    }

                    xSteps = (location.X - destination.X) / moveUpdates;
                    ySteps = (location.Y - destination.Y) / moveUpdates;

                }

                if (moveUpdates > 0)
                {
                    location.X = location.X - xSteps;
                    location.Y = location.Y - ySteps;

                    moveUpdates--;
                }
                else
                {
                    state = BlockState.BLOCKY;
                }
                //ToDestination();
            }
            

            base.Update(gameTime);
        }

        public void ToDestination()
        {
            //moving the block one unit every update.  Lets see how this works out
            //First Calculating the stuff necessary for automoving blocks(hmm I may use that )
            if (prevState != state)
            {
                if (Math.Abs(destination.X) > Math.Abs(destination.Y))
                {
                    //set moveUpdates
                    moveUpdates = Math.Abs((int)destination.X);
                }

                else
                {
                    moveUpdates = Math.Abs((int)destination.Y);
                }

                xSteps = (location.X - destination.X) / moveUpdates;
                ySteps = (location.Y - destination.Y) / moveUpdates;

            }

            if (moveUpdates > 0)
            {
                location.X = location.X - xSteps;
                location.Y = location.Y - ySteps;

                moveUpdates--;
            }
            else
            {
                state = BlockState.BLOCKY;
            }
        }
    
        public bool CollidesWith(Model otherModel, Matrix otherworld)
        {
            //Loop through each ModelMesh in both objects and compare
            //all bounding spheres for collisions
            foreach (ModelMesh myModelMeshes in model.Meshes)
            {
                foreach (ModelMesh hisModelMeshes in otherModel.Meshes)
                {
                    if (myModelMeshes.BoundingSphere.Transform(
                          GetWorld()).Intersects(hisModelMeshes.BoundingSphere.Transform(otherworld)))
                        return true;
                }
            }
            return false;
        }
        public bool CollidesWith(Block otherBlock)
        {
            if (otherBlock.GetLocation() == location)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CollidesWith(float locX, float locY)
        {
            if (location.X == locX && location.Y == locY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Matrix GetWorld()
        {
            return (world * Matrix.CreateTranslation(location));
        }

    }
}