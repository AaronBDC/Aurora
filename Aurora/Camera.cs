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
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {   
        //Camera Matrices
        public Matrix view {get; protected set;}
        public Matrix projection { get; protected set; }

        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;

        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            //view = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 20.0f), Vector3.Zero, Vector3.Up); //Matrix.CreateLookAt(pos, target, up);
            cameraPosition = pos;
            cameraDirection = target - pos;
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLookAt();


            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                (float)Game.Window.ClientBounds.Width /
                (float)Game.Window.ClientBounds.Height,
                1, 10000);
                
            //Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), 1.333f, 1.0f, 10000.0f);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Camera Movement code
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {

                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                
                
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                //Move foward
                cameraPosition += cameraDirection;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Z) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                cameraPosition -= cameraDirection;
            }
            base.Update(gameTime);
        }

        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
    }
}