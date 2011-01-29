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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Aurora
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<BasicModel> models = new List<BasicModel>();
        //List<Debris> debris = new List<Debris>();
        //List<Block> package = new List<Block>();

        //Block[] package;

        Package package;

        //keyboard states
        KeyboardState currentKeyState;
        KeyboardState prevKeyState;

        float blockSize;

        //flag that signifies if a package is still in play on the field
        private bool packageInPlay;

        //counters
        private int totalBlocks;

        public ModelManager(Game game)
            : base(game)
        {
            packageInPlay = false;
            
            //this depends on the models that are being used for the blocks. Should be 1 unit but that will be worked out
            blockSize = 13.0f;

            package = new Package((Game1)Game, blockSize);

            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            //package = new Block[4];

            totalBlocks = 0;
            package.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            currentKeyState = Keyboard.GetState();
            //Vector3 tempLocation;

            //creating Package if none are in play
            if ( packageInPlay == false)
            {
                package.CreatePackage();
                package.setInitialPosition(new Vector3(-350.0f, 215.0f, 0.0f));

                packageInPlay = true;
            }
            
            //Checking Keyboard States
            currentKeyState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.R) && prevKeyState.IsKeyUp(Keys.R))
            {
                package.RotatePackageRight();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E) && prevKeyState.IsKeyUp(Keys.E))
            {
                package.RotatePackageLeft();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && prevKeyState.IsKeyUp(Keys.Enter))
            {
                packageInPlay = false;

                if (totalBlocks == 0)
                {
                    //need to move to the center of the level    
                }

                for (int i = 0; i < 4; i++)
                {
                    models.Add(package.GetBlock(i));

                    //incrementing block count
                    totalBlocks += 4;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (prevKeyState != Keyboard.GetState())
                {
                    package.MoveDown(models);

                    prevKeyState = Keyboard.GetState();
                }
                else
                {
                    //there should be some sort of logic here, just can't think of it right now
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (prevKeyState != Keyboard.GetState())
                {
                    package.MoveUp(models);

                    prevKeyState = Keyboard.GetState();
                }

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (prevKeyState != Keyboard.GetState())
                {
                    package.MoveLeft(models);

                    prevKeyState = Keyboard.GetState();
                }

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (prevKeyState != Keyboard.GetState())
                {
                    package.MoveRight(models);

                    prevKeyState = Keyboard.GetState();
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                //resetting the blocks on the field for obvious debugging purposes.
                models.Clear();
                totalBlocks = 0;
            }



            if (packageInPlay == true)
            {
                //updating the package on the screen
                    package.Update(gameTime);
                
            }

            
            //showing block location for debugging purposes....bitches!!!(oh yes 3 exclamation marks)
            /*System.Console.WriteLine("Block 1" + package[0].GetLocation().ToString() + "Block 2" + package[1].GetLocation().ToString() +
                                        "Block 3" + package[2].GetLocation().ToString() +"Block 4" + package[3].GetLocation().ToString() );*/

            //remembering the this key state for the next update
            prevKeyState = currentKeyState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            package.Draw(gameTime);

            //Loop through and draw each model
            foreach (BasicModel bm in models)
            {
                bm.Draw(((Game1)Game).camera);
            }

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {

            //Blocks

            //Debri
            //models.Add(new Debris(Game, Game.Content.Load<Model>(@"Models\Box\Intercooler")));

            //Other stuff unthought of yet

            base.LoadContent();
        }

        public void CreatePackage()
        {
            package.CreatePackage();
            package.setInitialPosition(new Vector3(-300.0f, 300.0f, 50.0f));
        }

        void RotatePackageRight()
        {
            package.RotatePackageRight();
        }

        void RotatePackageLeft()
        {
            package.RotatePackageLeft();
        }

        void SetPackage()
        {

        }
    }
}
