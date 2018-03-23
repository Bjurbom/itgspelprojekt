using Itgspelprojekt.Tiles;
using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Itgspelprojekt.battle;

namespace Itgspelprojekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    enum Gamestate { ingame, battle };

    public class Game1 : Game
    {

        animationForBattle battleAnimation, battleMenuAnimation;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Map map;
        Player player;
        Creatures.Creatures creatures;
        Gamestate gamestate;
        Texture2D fadeIn, battle, menuBattle, healthMenuBattle;

        KeyboardState lastUpdate;

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

            fadeIn = Content.Load<Texture2D>("blackspace");
            battle = Content.Load<Texture2D>("backroundForBattle");
            menuBattle = Content.Load<Texture2D>("battleMenu");
            healthMenuBattle = Content.Load<Texture2D>("healthMenu");
            battleAnimation = new animationForBattle(battle, new Vector2(0, 0), new Vector2(0, 0));
            battleMenuAnimation = new animationForBattle(menuBattle, new Vector2(1200, 400), new Vector2(0,0));
            

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
                player.PlayerUpdate();
                player.Update();

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    camera.Zoom += 0.1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.T))
                {
                    camera.Zoom -= 0.1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    camera.Rotation += 0.1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.G))
                {
                    camera.Rotation -= 0.1f;
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
                            player.PlayerStop();
                            if (camera.Zoom >= 30)
                            {
                                gamestate = Gamestate.battle;
                                item.Id = 2;
                            }
                        
                        }

                    }

                camera.Update(player.position);
                }
            }
            else if (gamestate == Gamestate.battle)
            {
                camera.Update(new Vector2(battle.Width / 2, battle.Height / 2));

                battleAnimation.Update(gameTime);
                battleMenuAnimation.Update(gameTime);
          
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
                foreach (Creature creature in creatures.creatures)
                {
                    creature.Draw(spriteBatch);
                }
                player.Draw(spriteBatch);

                
            }
            if (gamestate == Gamestate.battle)
            {
                GraphicsDevice.Clear(Color.Black);

                // återstälelr kamra inställningarna
                camera.Zoom = 1;
                camera.Rotation = 0;

                battleAnimation.Draw(spriteBatch);
                battleMenuAnimation.Draw(spriteBatch);


            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
