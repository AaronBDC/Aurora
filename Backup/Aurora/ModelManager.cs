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

        Block[] package;

        //keyboard states
        KeyboardState currentKeyState;
        KeyboardState prevKeyState;

        bool packageInPlay;

        public ModelManager(Game game)
            : base(game)
        {
            packageInPlay = false;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            package = new Block[4];

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            currentKeyState = Keyboard.GetState();
            Vector3 tempLocation;

            //creating Package if none are in play
            if ( packageInPlay == false)
            {
                CreatePackage();

                packageInPlay = true;
            }
            
            //Checking Keyboard States
            if (Keyboard.GetState().IsKeyDown(Keys.R) && prevKeyState.IsKeyUp(Keys.R))
            {
                RotatePackageRight();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E) && prevKeyState.IsKeyUp(Keys.E))
            {
                RotatePackageLeft();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && prevKeyState.IsKeyUp(Keys.Enter))
            {
                packageInPlay = false;

                for (int i = 0; i < 4; i++)
                {
                    //package[i].ToDestination();
                    models.Add(package[i]);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                for (int i = 0; i < 4; i++)
                {
                    tempLocation = package[i].GetLocation();
                    tempLocation.Y -= 1.0f;
                    package[i].SetLocation(tempLocation);

                    foreach (BasicModel bm in models)
                    {
                        
                        if (package[i].CollidesWith(bm.model, bm.GetWorld()))
                        {
                            //resetting initial values
                            /*tempLocation = package[i].GetLocation();
                            tempLocation.Y += 13.0f;
                            package[i].SetLocation(tempLocation);*/

                            package[i].SetState(Block.BlockState.BLOCKY);
                        }
                    }
                    
                }
                //if the down key is selecting updating the y position of the model
                //location.Y = location.Y - 13.0f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                for (int i = 0; i < 4; i++)
                {
                    tempLocation = package[i].GetLocation();
                    tempLocation.Y += 13.0f;

                    package[i].SetLocation(tempLocation);
                }
                //if the down key is selecting updating the y position of the model
                //location.Y = location.Y + 13.0f;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                for (int i = 0; i < 4; i++)
                {
                    tempLocation = package[i].GetLocation();
                    tempLocation.X -= 13.0f;

                    package[i].SetLocation(tempLocation);
                }
                //if the left key is selected updating the x position of the model
                //location.X = location.X - 13.0f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                for (int i = 0; i < 4; i++)
                {
                    tempLocation = package[i].GetLocation();
                    tempLocation.X += 13.0f;

                    package[i].SetLocation(tempLocation);
                }
                //if the right key is selected updating the x position of the model
                //location.X = location.X + 13.0f;
            }


            if (packageInPlay == true)
            {
                //updating the package on the screen
                for (int i = 0; i < 4; i++)
                {
                    package[i].Update(gameTime);
                }
            }

           /* //updating the models on the screen
            for (int i = 0; i < models.Count; ++i)
            {
                models[i].Update(gameTime);
            }
            */

            
            //showing block location for debugging purposes....bitches!!!(oh yes 3 exclamation marks)
            /*System.Console.WriteLine("Block 1" + package[0].GetLocation().ToString() + "Block 2" + package[1].GetLocation().ToString() +
                                        "Block 3" + package[2].GetLocation().ToString() +"Block 4" + package[3].GetLocation().ToString() );*/

            //remembering the this key state for the next update
            prevKeyState = currentKeyState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 4; i++)
            {
                package[i].Draw(((Game1)Game).camera);
            }

            //Drawing the package
            /*foreach (BasicModel bm in package)
            {
                bm.Draw(((Game1)Game).camera);
            }
            */
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
            //models.Add(new Block(Game, Game.Content.Load<Model>(@"Models\spaceship")));
            //models.Add(new Block(Game, Game.Content.Load<Model>(@"Models\box")));
            //models.Add(new Block(Game, Game.Content.Load<Model>(@"Models\redbox")));
            
            //models.Add(new
            //models.Add(new Block(Game, Game.Content.Load < Model>(@"Models\Box\green_box")));
            //models.Add(new Block(Game, Game.Content.Load<Model>(@"Models\monkey")));


            //Debri
            //models.Add(new Debris(Game, Game.Content.Load<Model>(@"Models\Box\Intercooler")));


            //Other stuff unthought of yet

            base.LoadContent();
        }

        public void CreatePackage()
        {
            Vector3 previewPosition = new Vector3(30, 30 , 0);

            for (int i = 0; i < 4; i++)
            {
                if (((Game1)Game).rand.Next(2) == 1)
                {
                    package[i] = new Block(Game, Game.Content.Load<Model>(@"Models\blue_box"));
                    //package.Add(new Block(Game, Game.Content.Load<Model>(@"Models\blue_box")));

                }
                else
                {
                    package[i] = new Block(Game, Game.Content.Load<Model>(@"Models\rboxnew"));
                    //package.Add(new Block(Game, Game.Content.Load<Model>(@"Models\rboxnew")));
                }

            }

            ///Putting the elements where they belong....I thnk
            //current blocks are about 13x13.

            //Placing blocks in the preview position
            /*package.ElementAt(package.Count() - 1).SetLocation(new Vector3(-300.0f, 160.0f, -50.0f));
            package.ElementAt(package.Count() - 1).SetDestination(new Vector3(-13.0f, 13.0f, -50.0f));

            package.ElementAt(package.Count() - 2).SetLocation(new Vector3(-274.0f, 160.0f, -50.0f));
            package.ElementAt(package.Count() - 2).SetDestination(new Vector3(13.0f, 13.0f, -50.0f));

            package.ElementAt(package.Count() - 3).SetLocation(new Vector3(-300.0f, 134.0f, -50.0f));
            package.ElementAt(package.Count() - 3).SetDestination(new Vector3(-13.0f, -13.0f, -50.0f));

            package.ElementAt(package.Count() - 4).SetLocation(new Vector3(-274.0f, 134.0f, -50.0f));
            package.ElementAt(package.Count() - 4).SetDestination(new Vector3(13.0f, -13.0f, -50.0f));
            */

            //block 1
            package[0].SetLocation(new Vector3(-300.0f, 160.0f, -50.0f));
            package[0].SetDestination(new Vector3(-13.0f, 13.0f, -50.0f));

            //block 2
            package[1].SetLocation(new Vector3(-274.0f, 160.0f, -50.0f));
            package[1].SetDestination(new Vector3(13.0f, 13.0f, -50.0f));

            //block 3
            package[2].SetLocation(new Vector3(-300.0f, 134.0f, -50.0f));
            package[2].SetDestination(new Vector3(-13.0f, -13.0f, -50.0f));

            //block 4
            package[3].SetLocation(new Vector3(-274.0f, 134.0f, -50.0f));
            package[3].SetDestination(new Vector3(13.0f, -13.0f, -50.0f));
        }

        void RotatePackageRight()
        {

          /* 
           * block switching breakdown of sorts
            1 = 3;
            3 = 4;
            4 = 2;
            2 = 1;
            */

            //Switching first block with third block
            Vector3 tempBlockLocation1 = package.ElementAt(package.Count() - 1).GetLocation();
            Vector3 tempBlockLocation2 = package.ElementAt(package.Count() - 3).GetLocation();
            package.ElementAt(package.Count() - 1).SetLocation(tempBlockLocation2);

            //Switching third block with fourth block
            tempBlockLocation2 = package.ElementAt(package.Count() - 4).GetLocation();
            package.ElementAt(package.Count() - 3).SetLocation(tempBlockLocation2);
            
            //Switching fourth block with second block
            tempBlockLocation2 = package.ElementAt(package.Count() - 2).GetLocation();
            package.ElementAt(package.Count() - 4).SetLocation(tempBlockLocation2);

            //Switching second block with first block
            //tempBlockLocation = models.ElementAt(models.Count() - 1).GetLocation();
            package.ElementAt(package.Count() - 2).SetLocation(tempBlockLocation1);
                
        }

        void RotatePackageLeft()
        {

            /* 
             * block switching breakdown of sorts
              1 = 2;
              2 = 4;
              4 = 3;
              3 = 1;
              */

            //Switching first block with second block
            Vector3 tempBlockLocation1 = package.ElementAt(package.Count() - 1).GetLocation();
            Vector3 tempBlockLocation2 = package.ElementAt(package.Count() - 2).GetLocation();
            package.ElementAt(package.Count() - 1).SetLocation(tempBlockLocation2);

            //Switching second block with fourth block
            tempBlockLocation2 = package.ElementAt(package.Count() - 4).GetLocation();
            package.ElementAt(package.Count() - 2).SetLocation(tempBlockLocation2);

            //Switching fourth block with third block
            tempBlockLocation2 = package.ElementAt(package.Count() - 3).GetLocation();
            package.ElementAt(package.Count() - 4).SetLocation(tempBlockLocation2);

            //Switching third block with first block
            //tempBlockLocation = models.ElementAt(models.Count() - 1).GetLocation();
            package.ElementAt(package.Count() - 3).SetLocation(tempBlockLocation1);
        }
        void SetPackage()
        {

        }


    }
}
