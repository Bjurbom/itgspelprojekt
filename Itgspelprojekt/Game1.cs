using Itgspelprojekt.Tiles;
using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Itgspelprojekt.battle;
using System.Collections.Generic;

namespace Itgspelprojekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    public enum Gamestate { ingame, battle };



    public class Game1 : Game
    {

        animationForBattle battleAnimation, battleMenuAnimation, battleHealthbars;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Map map;
        Player player;
        Creatures.Creatures creatures;
        Texture2D fadeIn, battle, menuBattle, healthMenuBattle;
        KeyboardState lastUpdate;
        SpriteFont nameInBattle;
        List<UI> UIList;
        Vector2 selectorPosition;

        public static Gamestate gamestate;


        controlForUI mainBattleMenu;


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

            //för UI
            UIList = new List<UI>();
            selectorPosition = new Vector2(730, 550);


            //laddar in textures / text
            fadeIn = Content.Load<Texture2D>("blackspace");
            battle = Content.Load<Texture2D>("backroundForBattle");
            menuBattle = Content.Load<Texture2D>("battleMenu");
            healthMenuBattle = Content.Load<Texture2D>("healthMenu");
            nameInBattle = Content.Load<SpriteFont>("textForName");


            //battle animationer
            battleAnimation = new animationForBattle(battle, new Vector2(0, 0), new Vector2(0,0));
            battleMenuAnimation = new animationForBattle(menuBattle, new Vector2(1200,1200), new Vector2(-1,-1));
            battleHealthbars = new animationForBattle(healthMenuBattle, new Vector2(1200, 0), new Vector2(-1, 60));

            //battle UI Main
            UIList.Add(new UI(new Vector2(170, 90), nameInBattle, player.name));
            UIList.Add(new UI(new Vector2(750, 550), nameInBattle, "Attack"));
            UIList.Add(new UI(new Vector2(750, 600), nameInBattle, "Stats"));
            UIList.Add(new UI(new Vector2(850, 550), nameInBattle, "Inventory"));
            UIList.Add(new UI(new Vector2(850, 600), nameInBattle, "Run"));

            mainBattleMenu = new controlForUI(nameInBattle,new Vector2(740, 550), 2, 2);

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


            // in game
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

                //hitdetection
                foreach (CollisionTiles item in map.CollisionTiles)
                {

                    player.PlayerHitdetection(item,camera);

                    camera.Update(player.position);
                }

                
            }

            //battle
            else if (gamestate == Gamestate.battle)
            {
                camera.Update(new Vector2(battle.Width / 2, battle.Height / 2));
                camera.Zoom = 1;
                camera.Rotation = 0;

                battleAnimation.Update(gameTime);
                battleMenuAnimation.Update(gameTime);
                battleHealthbars.Update(gameTime);

                if (battleHealthbars.InPosition == true)
                {
                    mainBattleMenu.Update(gameTime);
                }
                
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 2 && mainBattleMenu.SelectorPositionY == 2)
                {
                    gamestate = Gamestate.ingame;
                }
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
                battleHealthbars.Draw(spriteBatch);


                if (battleMenuAnimation.InPosition == true)
                {
                    foreach (UI textItem in UIList)
                    {
                        textItem.Draw(spriteBatch);
                    }

                    mainBattleMenu.Draw(spriteBatch);
                }
                
                
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
