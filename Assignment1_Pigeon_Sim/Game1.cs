using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Diagnostics;

namespace Assignment1_Pigeon_Sim
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        
        private Matrix theWorld = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix theCamera; 
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);
        private PlotClient mapClient;
        private InputHandler inputHandlers;
        private Vector3 camPositionVector;
        private Vector3 camEyeVector;
        private Vector3 deltaVector;
        private Vector3 mouseInputDelta;
        private Camera camera;
        

        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            mapClient = new PlotClient(Content, 7, 7, 0.5f);
            mapClient.SetPlotList();
            mapClient.PrintPlotList();

            this.IsMouseVisible = true;


            camEyeVector = new Vector3(0,0,0);
            Debug.WriteLine("camEyeVector" + camEyeVector.X + " " + camEyeVector.Y + " " + camEyeVector.Z);
            camPositionVector = Vector3.Add(new Vector3(50, 0, 0), new Vector3(0, 1.6f, 0));
            deltaVector = new Vector3(0.01f, 0, 0);
            camera = new Camera(theCamera, camPositionVector, camEyeVector, deltaVector);
            //theCamera = Matrix.CreateLookAt(new Vector3(50, 0, 0), new Vector3(0, 0, 0), Vector3.Up);
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
           

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //int screenX = GraphicsDevice.Viewport.Width;
            //int screenY = GraphicsDevice.Viewport.Height;

            int screenX = Window.ClientBounds.Width;
            int screenY = Window.ClientBounds.Height;

            int centerX = (int)(screenX / 2);
            int centerY = (int)(screenY / 2);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            inputHandlers = new InputHandler(screenX, screenY);
            mouseInputDelta = inputHandlers.MouseHandler(screenX, screenY, 1.00f);

            // calculate pitch axis for rotating, therefore the orthogonal between the forward and up 
            // assuming righthandedness
            Vector3 pitchAxis = Vector3.Cross(deltaVector, Vector3.Up);
            pitchAxis.Normalize();

            deltaVector = camera.CameraUpdate(deltaVector, pitchAxis, mouseInputDelta.Y, mouseInputDelta);
            deltaVector = camera.CameraUpdate(deltaVector, Vector3.Up, -mouseInputDelta.X, mouseInputDelta);

            camPositionVector = camPositionVector * deltaVector; // i think this is correct scalar multiply
            camEyeVector = camPositionVector +  deltaVector; // this is correct add

            theCamera = Matrix.CreateLookAt(camPositionVector, camEyeVector, Vector3.Up);

            //Mouse.SetPosition((int)centerX, (int)centerY);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //buildingObj.ActorDraw(theWorld, theCamera, projection);

            // TODO: Add your drawing code here
            /*
            foreach (var plot in client.GetLandPlots())
            {
                plot.Value.ActorDraw(theWorld, theCamera, projection);
            }
            */

            for(int ii = 0; ii < mapClient.GetPlotList().Count; ii++)
            {
                mapClient.GetPlotList()[ii].ActorDraw(theWorld, theCamera, projection);
                
            }

            base.Draw(gameTime);
        }




    }


}
