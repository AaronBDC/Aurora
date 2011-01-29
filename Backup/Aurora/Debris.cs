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
    public class Debris : BasicModel
    {
        public Debris(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Debris(Game game, Model m)
            : base(game, m)
        {

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
        public bool CollidesWith(Model otherModel, Matrix otherworld)
        {
            //Loop through each ModelMesh in both objects and compare
            //all bouding spheres for collisions
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

        public override Matrix GetWorld()
        {
            return (world);
        }
    }
}