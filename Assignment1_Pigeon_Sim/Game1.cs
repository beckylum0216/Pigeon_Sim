using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

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
        private PlotClient client;
        private InputHandler inputHandlers;

        private Vector3 camPositionVector;
        private Vector3 camRotateVector;
        private Vector3 mouseInputDelta;
        private Vector3 camRotateDelta;
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

            client = new PlotClient(Content, 7, 7, 0.5f);
            client.SetPlotList();
            client.PrintPlotList();

            this.IsMouseVisible = true;
            camRotateVector = new Vector3(0, 0, 0);
            camPositionVector = new Vector3(50, 0, 0);
            camRotateDelta = camPositionVector;
            camera = new Camera(camPositionVector);


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
            int screenX = GraphicsDevice.Viewport.Width;
            int screenY = GraphicsDevice.Viewport.Height;
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            inputHandlers = new InputHandler(screenX, screenY);
            mouseInputDelta = inputHandlers.MouseHandler(screenX, screenY, 1.00f);
            mouseInputDelta.Normalize();

            camRotateVector += camera.CameraUpdate(camPositionVector, mouseInputDelta) * deltaTime * 60;

            camRotateDelta = camera.GetDeltaVector();

            camPositionVector = Vector3.Cross(camPositionVector, camRotateDelta);
            
            theCamera = Matrix.CreateLookAt(camPositionVector, new Vector3(0,0,0) , Vector3.UnitY);
            
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

            for(int ii = 0; ii < client.GetPlotList().Count; ii++)
            {
                client.GetPlotList()[ii].ActorDraw(theWorld, theCamera, projection);
                
            }

            base.Draw(gameTime);
        }
    }
}
