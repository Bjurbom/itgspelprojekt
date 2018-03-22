using Itgspelprojekt.Tiles;
using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Itgspelprojekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    enum Gamestate { ingame, battle };

    public class Game1 : Game
    {
       

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Map map;
        Player player;
        Creatures.Creatures creatures;
        Gamestate gamestate;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1280;
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
            
            map = new Map();

            camera = new Camera(graphics.GraphicsDevice.Viewport);

            player = new Player("bob", new Vector2(896, 896), 10, Content.Load<Texture2D>("knuc"));

            creatures = new Creatures.Creatures();
            creatures.ParseCreaturesFile(Content);

            gamestate = Gamestate.ingame;
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //map generator
            Tiless.Content = Content;
            map.Generate(level.map , 64);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /*/temporär movements för kamran
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y-= 10;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y+= 10;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X-=10;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X+=10;
            }
            //Kamran upptateras
            camera.Update(position); */
            if (gamestate == Gamestate.ingame)
            {
                player.Update();
                player.PlayerUpdate();

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    camera.Zoom += 0.1f;
                }
                foreach (Creature creature in creatures.creatures)
                {
                    creature.Update();
                }
                foreach (CollisionTiles item in map.CollisionTiles)
                {
                    if (player.hitboxUp.Intersects(item.Rectangle))
                    {
                        if (item.Id == 1)
                        {
                            player.goingUp = false;
                            
                        }
                        else
                        {
                            player.goingUp = true;
                        }
                    }


                    if (player.hitboxDown.Intersects(item.Rectangle))
                    {
                        if (item.Id == 1)
                        {
                            player.goingDown = false;
                        }
                        else
                        {
                            player.goingDown = true;
                        }
                    }

                    if (player.hitboxLeft.Intersects(item.Rectangle))
                    {
                        if (item.Id == 1)
                        {
                            player.goingLeft = false;
                        }
                        else
                        {
                            player.goingLeft = true;
                        }
                    }


                    if (player.hitboxRight.Intersects(item.Rectangle))
                    {
                        if (item.Id == 1)
                        {
                            player.goingRight = false;
                        }
                        else
                        {
                            player.goingRight = true;
                        }
                    }
                    if (player.hitbox.Intersects(item.Rectangle))
                    {
                        if (item.Id == 3)
                        {
                            camera.Zoom += 0.5f;
                            camera.Rotation += 0.5f;
                            if (camera.Zoom >= 20)
                            {
                                gamestate = Gamestate.battle;
                                item.Id = 2;
                            }
                        
                        }

                    }

                }
                camera.Update(player.position);
            }



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            if (gamestate == Gamestate.ingame)
            {
                map.Draw(spriteBatch);
                player.Draw(spriteBatch);
                foreach (Creature creature in creatures.creatures)
                {
                    creature.Draw(spriteBatch);
                }
                
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
