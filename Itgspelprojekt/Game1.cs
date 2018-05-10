using Itgspelprojekt.Tiles;
using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Itgspelprojekt.Abstrac_battle.battle;
using System.Collections.Generic;

namespace Itgspelprojekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    public enum Gamestate { ingame, battle,exit,meny,settings };



    public class Game1 : Game
    {

        animationForBattle battleAnimation, battleMenuAnimation, battleHealthbars;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Map map;
        Pathfinder pathfinder;
        Player player;
        Creatures.Creatures creatures;
        Texture2D fadeIn, battle, menuBattle, healthMenuBattle;
        SpriteFont nameInBattle;
        SpriteFont developerFont;
        List<UI> UIList;
        Vector2 selectorPosition;
        NormalBattle normalBattle;
        Meny meny; 

        string errorMessage;


        public static Gamestate gamestate;
        public static Creature battleOpponent;



        controlForUI mainBattleMenu;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1280;

            IsMouseVisible = true;
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
            pathfinder = new Pathfinder();
            camera = new Camera(graphics.GraphicsDevice.Viewport);
            

            //för UI
            UIList = new List<UI>();
            selectorPosition = new Vector2(730, 550);


            //laddar in textures / text
            fadeIn = Content.Load<Texture2D>("blackspace");
            battle = Content.Load<Texture2D>("backroundForBattle");
            menuBattle = Content.Load<Texture2D>("battleMenu");
            healthMenuBattle = Content.Load<Texture2D>("healthMenu");
            nameInBattle = Content.Load<SpriteFont>("textForName");
            developerFont = Content.Load<SpriteFont>("Ffont");


            //battle animationer
            battleAnimation = new animationForBattle(battle, new Vector2(0, 0), new Vector2(0, 0));
            battleMenuAnimation = new animationForBattle(menuBattle, new Vector2(1200, 1200), new Vector2(-1, -1));
            battleHealthbars = new animationForBattle(healthMenuBattle, new Vector2(1200, 0), new Vector2(-1, 60));



            creatures = new Creatures.Creatures();
            errorMessage = creatures.ParseCreaturesFile(Content);
            creatures.creatures[0].MoveTo(pathfinder.Pathfind(creatures.creatures[0].position, new Vector2(128, 832), 0));
            creatures.creatures[1].MoveTo(pathfinder.Pathfind(creatures.creatures[1].position, new Vector2(2496, 256), 0));
            creatures.creatures[2].MoveTo(pathfinder.Pathfind(creatures.creatures[2].position, new Vector2(2432, 256), 0));
            creatures.creatures[3].MoveTo(pathfinder.Pathfind(creatures.creatures[3].position, new Vector2(2368, 256), 0));
            errorMessage += pathfinder.errorMessage;
            pathfinder.errorMessage = string.Empty;

            player = new Player("bob", new Vector2(896, 896), 10, Content.Load<Texture2D>("knuc"), creatures.creatures);

            //battle UI Main
            UIList.Add(new UI(new Vector2(170, 90), nameInBattle, player.Name));
            UIList.Add(new UI(new Vector2(750, 550), nameInBattle, "Attack"));
            UIList.Add(new UI(new Vector2(750, 600), nameInBattle, "Stats"));
            UIList.Add(new UI(new Vector2(850, 550), nameInBattle, "Inventory"));
            UIList.Add(new UI(new Vector2(850, 600), nameInBattle, "Run"));

            mainBattleMenu = new controlForUI(nameInBattle, new Vector2(740, 550), 2, 2);


            gamestate = Gamestate.meny;

            normalBattle = new NormalBattle(battle, menuBattle, healthMenuBattle, mainBattleMenu, UIList, player, nameInBattle,spriteBatch);

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
            map.Generate(level.map, 64);
            meny = new Meny(Content);
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

            if (gamestate == Gamestate.exit)
            {
                Exit();
            }
            meny.update(gameTime);
            // in game
            if (gamestate == Gamestate.ingame)
            {
                //gör mussen synlig
                IsMouseVisible = true;

                player.PlayerUpdate();
                player.Update();
                CameraControls(); //camera controls

                foreach (Creature creature in creatures.creatures)
                {
                    creature.Update();
                    if (creature.canDoBattle && creature.Hitbox.Intersects(player.Hitbox))
                    {


                        normalBattle = new NormalBattle(battle, menuBattle, healthMenuBattle, mainBattleMenu, UIList, player, nameInBattle, spriteBatch);
                        gamestate = Gamestate.battle;
                            battleOpponent = creature;
                        
                       
                    }
                }
                // återställer kamran
               // CameraReset();

                //hitdetection
                foreach (CollisionTiles item in map.CollisionTiles)
                {
                    player.PlayerHitdetection(item, camera);

                    camera.Update(player.position);
                }

            }

            //battle
            else if (gamestate == Gamestate.battle)
            {
                normalBattle.Update(camera, gameTime);
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
                normalBattle.Draw(spriteBatch);
                battleOpponent.DrawInBattle(spriteBatch, new Rectangle(1085, 155, 200, 200));
                player.DrawInBattle(spriteBatch, new Rectangle(235, 460, 200, 200));
            }
            spriteBatch.End();

            
            spriteBatch.Begin(); // No camera transform in this spriteBatch.

            if (gamestate == Gamestate.ingame)
                spriteBatch.DrawString(developerFont, player.position.ToString() + "   " + errorMessage, new Vector2(0, 0), Color.Black); // errorMessage = String.Empty if no error has occured.
            else if (gamestate == Gamestate.battle)
                spriteBatch.DrawString(developerFont, errorMessage, new Vector2(0, 0), Color.Black); // errorMessage = String.Empty if no error has occured.

            spriteBatch.End();

            if(gamestate == Gamestate.meny)
            {
                spriteBatch.Begin();
                meny.Draw(spriteBatch);
                spriteBatch.End();
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void CameraControls()
        {
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
        }
        private void CameraReset()
        {
            camera.Zoom = 1;
            camera.Rotation = 0;
        }

    }
}
