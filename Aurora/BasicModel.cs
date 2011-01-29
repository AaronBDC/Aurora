using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aurora
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class BasicModel : Microsoft.Xna.Framework.GameComponent
    {
        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;
        protected Vector3 location;
        protected String name;

        public BasicModel(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public BasicModel(Game game, Model m)
            :base(game)
        {
            model = m;
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
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        /**
         * Drawing the models of course
         * 
         * 
         **/
        public void Draw(Camera camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.EnableDefaultLighting();
                    be.Projection = camera.projection;
                    be.View = camera.view;
                    be.World = GetWorld(); //* mesh.ParentBone.Transform;
                }

                mesh.Draw();
            }
        }

        public virtual void SetLocation(Vector3 newLocation)
        {
            location = newLocation;
        }

        public virtual Vector3 GetLocation()
        {
            return location;
        }

        public virtual void SetDestination(Vector3 newLocation)
        {

        }

        public virtual void SetName(String newName)
        {
            name = newName;
        }

        /**
         * Pretty obvious as to what this does
         * 
         */
        public virtual Matrix GetWorld()
        {
            return world;
        }
    }
}