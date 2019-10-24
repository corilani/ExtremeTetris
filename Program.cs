using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ExtremeTetris
{
    public class Program : Game
    {
        #region Declarations
        #region Common
        /// <summary>
        /// true - if you want to use debug features, false - if not
        /// </summary>
        private const bool DEBUG = false;

        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        private GameTime _gameTime;

        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;

        private SpriteFont _debugFont;

        private Color fontColor;

        private Effect _blockColorize;

        #endregion Common

        #region Main
        #region Main Menu
        private Menu _mainMenu;

        private string[] _mainMenuMenus = new string[]
        {
            "New Game", "Settings", "Credits", "Quit"
        };
        #endregion Main Menu

        #region Settings Menu
        private Menu _settingsMenu;

        private string[] _settingsMenus = new string[]
        {
            "There is no settings, unfortunately", "Back"
        };
        #endregion Settings Menu

        #region Credits
        private Menu _creditsMenu;

        private string[] _creditsMenus = new string[]
        {
            "Title: Extreme Tetris", "Created by: Konstantin Kraynov", "Creating year: 2019", "Made just for fun", "Please, do not throw rocks at me"
        };
        #endregion Credits
        #endregion Main

        #region Gaming
        #region Gaming Tutorial

        #endregion Gaming Tutorial

        #region Gaming
        private Table _table;
        private Table _tableNextShape;
        private Shape _currentShape;
        private Shape _nextShape;
        private Score _currentScore;

        private Texture2D _currentBlockTex;
        private Texture2D _emptyBlockTex;
        private Texture2D _ghostBlockTex;

        private Vector2 _tableStart;
        private Vector2 _tableNextShapeStart;

        private GamePadState _currentGamePadState;
        private GamePadState _previousGamePadState;

        private double _elapsedTime = 0;
        private double _timeInterval = 1000;

        private int _topGlobalSideOffset = 5;
        private int _leftGlobalSideOffset = 5;
        private int _rightGlobalSideOffset = 5;

        private Vector2 _topTableOffset;

        private bool _ghost_mode = false;

        private double _YLimit = 1.0f;
        private double _XLimit = 0.5f;
        #endregion Gaming

        #region Gaming Pause Menu
        private Menu _gamingPauseMenu;

        private string[] _gamingPauseMenus = new string[2]
        {
            "Back", "Quit"
        };

        private Texture2D blackFill;
        #endregion Gaming Pause Menu
        #endregion Gaming

        #region Active Windows
        #region Main
        private bool _active_MainMenu = true;
        private bool _active_SettingsMenu = false;
        private bool _active_CreditsMenu = false;
        #endregion Main

        #region Gaming
        private bool _active_GamingTutorial = true;
        private bool _active_Gaming = false;
        private bool _active_GamingPauseMenu = false;
        #endregion Gaming
        #endregion Active Windows

        #region GamePad Keys
        private Buttons gp_up = Buttons.DPadUp;
        private Buttons gp_down = Buttons.DPadDown;
        private Buttons gp_left = Buttons.DPadLeft;
        private Buttons gp_right = Buttons.DPadRight;

        private Buttons gp_pause = Buttons.Start;
        private Buttons gp_select = Buttons.A;
        private Buttons gp_back = Buttons.B;

        private Buttons gp_dropdown = Buttons.A;
        private Buttons gp_rotateLeft = Buttons.LeftShoulder;
        private Buttons gp_rotateRight = Buttons.RightShoulder;
        private Buttons gp_moveLeft = Buttons.DPadLeft;
        private Buttons gp_moveRight = Buttons.DPadRight;
        private Buttons gp_moveDown = Buttons.DPadDown;
        #endregion GamePad Keys

        #region Keyboard Keys
        private Keys kb_up = Keys.Up;
        private Keys kb_down = Keys.Down;
        private Keys kb_left = Keys.Left;
        private Keys kb_right = Keys.Right;

        private Keys kb_wup = Keys.W;
        private Keys kb_sdown = Keys.S;
        private Keys kb_aleft = Keys.A;
        private Keys kb_dright = Keys.D;

        private Keys kb_pause = Keys.Escape;
        private Keys kb_select = Keys.Enter;
        private Keys kb_back = Keys.Escape;

        private Keys kb_dropdown = Keys.Space;
        private Keys kb_rotateLeft = Keys.Q;
        private Keys kb_rotateRight = Keys.E;
        #endregion Keyboard Keys
        #endregion Declarations

        public Program()
        {
            #region Common
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = (_topGlobalSideOffset + (30 * 20) + (1 * 19) + _topGlobalSideOffset + 45); //700
            //                                   (_leftSideOffset + (_currentBlockTex.width * _table.getCol()) + (_table.getSpacing() * (_table.getCol() - 1)) + (_table.getSpacing() * 2) + (_currentBlockTex.Width * (_tableNextShape.getCol() - 1)) + (_tableNextShape.getSpacing() * 1) + _rightGlobalSideOffset)
            _graphics.PreferredBackBufferWidth = (_leftGlobalSideOffset + (30 * 15) + (1 * 14) + (1 * 2) + (30 * 4) + (1 * 1) + _rightGlobalSideOffset);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            #endregion Common
        }

        #region Init
        protected override void Initialize()
        {
            #region Main
            #region Common

            #endregion Common

            #region Main Menu
            _mainMenu = new Menu(_mainMenuMenus);
            #endregion Main Menu

            #region Settings Menu
            _settingsMenu = new Menu(_settingsMenus);
            #endregion Settings Menu

            #region Credits Menu
            _creditsMenu = new Menu(_creditsMenus);
            #endregion Credits Menu
            #endregion Main

            #region Gaming
            #region Gaming Tutorial

            #endregion Gaming Tutorial

            #region Gaming
            _table = new Table();
            _table.createTable(15, 20, 1);
            _tableStart = new Vector2(_leftGlobalSideOffset, _topGlobalSideOffset);

            _tableNextShape = new Table();
            _tableNextShape.createTable(4, 4, _table.getSpacing());
            _tableNextShapeStart = new Vector2((_leftGlobalSideOffset + (_table.getCol() * 30) + (_table.getSpacing() * (_table.getCol() - 1)) + (_table.getSpacing() * 2)),
                                               (_topGlobalSideOffset));

            _currentScore = new Score();
            #endregion Gaming

            #region Gaming Pause Menu
            _gamingPauseMenu = new Menu(_gamingPauseMenus);
            #endregion Gaming Pause Menu
            #endregion Gaming

            base.Initialize();
        }

        /// <summary>
        /// Setups variables before new game started
        /// </summary>
        private void initialize_NewGame()
        {
            _currentShape = new Shape();
            _nextShape = new Shape();
        }
        #endregion Init

        protected override void LoadContent()
        {
            #region Common
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            _blockColorize = Content.Load<Effect>("blockColorize");
            #endregion Common

            #region Main
            #region Main Menu

            #endregion Main Menu

            #region Settings Menu

            #endregion Settings Menu
            #endregion Main

            #region Gaming
            #region Gaming Tutorial

            #endregion Gaming Tutorial

            #region Gaming
            _currentBlockTex = Content.Load<Texture2D>("block");
            _emptyBlockTex = Content.Load<Texture2D>("emptyBlock");
            _ghostBlockTex = Content.Load<Texture2D>("ghostBlock");

            _debugFont = Content.Load<SpriteFont>("Debug-font");

            _topTableOffset = new Vector2((_leftGlobalSideOffset + (_currentBlockTex.Width * _table.getCol()) + (_table.getSpacing() * (_table.getCol() - 1)) + _rightGlobalSideOffset),
                                              (_topGlobalSideOffset + (_currentBlockTex.Width * _table.getRow()) + (_table.getSpacing() * (_table.getRow() - 1)) + _topGlobalSideOffset));
            #endregion Gaming

            #region Gaming Pause Menu
            blackFill = Content.Load<Texture2D>("blackFill");
            #endregion Gaming Pause Menu
            #endregion Gaming
        }

        #region Update
        protected override void Update(GameTime gameTime)
        {
            _currentKeyboardState = Keyboard.GetState();
            _currentGamePadState = GamePad.GetState(0);

            _gameTime = gameTime;

            // Active window
            if (_active_MainMenu)
                update_MainMenu();
            else
            if (_active_SettingsMenu)
                update_SettingsMenu();
            else
            if (_active_CreditsMenu)
                update_CreditsMenu();
            else
            if (_active_Gaming)
            {
                if (_active_GamingTutorial)
                    update_GamingTutorial();
                else
                if (_active_GamingPauseMenu)
                    update_GamingPauseMenu();
                else
                    update_Gaming();
            }

            _previousGamePadState = _currentGamePadState;
            _previousKeyboardState = _currentKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// Updates Main Menu logic
        /// </summary>
        private void update_MainMenu()
        {
            #region Select Button
            if (_currentGamePadState.IsButtonDown(gp_select) && _previousGamePadState.IsButtonUp(gp_select) ||
               (_currentKeyboardState.IsKeyDown(kb_select) && _previousKeyboardState.IsKeyUp(kb_select)))
            {
                switch (_mainMenu.getMenus()[_mainMenu.getSelectedIndex()])
                {
                    case "New Game":
                        initialize_NewGame();
                        setActiveWindow(ref _active_Gaming);
                        break;
                    case "Settings":
                        setActiveWindow(ref _active_SettingsMenu);
                        break;
                    case "Credits":
                        setActiveWindow(ref _active_CreditsMenu);
                        break;
                    case "Quit":
                        Exit();
                        break;
                }
            }
            #endregion Select Button

            #region Down Button
            if ((-1f * _currentGamePadState.ThumbSticks.Left.Y >= _YLimit && -1f * _previousGamePadState.ThumbSticks.Left.Y < _YLimit) ||
                (_currentGamePadState.IsButtonDown(gp_down) && _previousGamePadState.IsButtonUp(gp_down)) ||
                (_currentKeyboardState.IsKeyDown(kb_down) && _previousKeyboardState.IsKeyUp(kb_down)))
            {
                _mainMenu.increaseMenu();
            }
            #endregion Down Button

            #region Up Button
            if ((_currentGamePadState.ThumbSticks.Left.Y >= _YLimit && _previousGamePadState.ThumbSticks.Left.Y < _YLimit) ||
                (_currentGamePadState.IsButtonDown(gp_up) && _previousGamePadState.IsButtonUp(gp_up)) ||
                (_currentKeyboardState.IsKeyDown(kb_up) && _previousKeyboardState.IsKeyUp(kb_up)))
            {
                _mainMenu.decreaseMenu();
            }
            #endregion Up Button
        }

        /// <summary>
        /// Updates Settings Menu logic
        /// </summary>
        private void update_SettingsMenu()
        {
            #region Select Button
            if (_currentGamePadState.IsButtonDown(gp_select) && _previousGamePadState.IsButtonUp(gp_select) ||
               (_currentKeyboardState.IsKeyDown(kb_select) && _previousKeyboardState.IsKeyUp(kb_select)))
            {
                switch (_settingsMenu.getMenus()[_settingsMenu.getSelectedIndex()])
                {
                    case "Back":
                        setActiveWindow(ref _active_MainMenu);
                        break;
                }
            }
            #endregion Select Button

            #region Back Button
            if ((_currentGamePadState.IsButtonDown(gp_back) && _previousGamePadState.IsButtonUp(gp_back)) ||
                (_currentKeyboardState.IsKeyDown(kb_back) && _previousKeyboardState.IsKeyUp(kb_back)))
            {
                setActiveWindow(ref _active_MainMenu);
            }
            #endregion Back Button

            #region Down Button
            if ((-1f * _currentGamePadState.ThumbSticks.Left.Y >= _YLimit && -1f * _previousGamePadState.ThumbSticks.Left.Y < _YLimit) ||
                (_currentGamePadState.IsButtonDown(gp_down) && _previousGamePadState.IsButtonUp(gp_down)) ||
                (_currentKeyboardState.IsKeyDown(kb_down) && _previousKeyboardState.IsKeyUp(kb_down)))
            {
                _settingsMenu.increaseMenu();
            }
            #endregion Down Button

            #region Up Button
            if ((_currentGamePadState.ThumbSticks.Left.Y >= _YLimit && _previousGamePadState.ThumbSticks.Left.Y < _YLimit) ||
                (_currentGamePadState.IsButtonDown(gp_up) && _previousGamePadState.IsButtonUp(gp_up)) ||
                (_currentKeyboardState.IsKeyDown(kb_up) && _previousKeyboardState.IsKeyUp(kb_up)))
            {
                _settingsMenu.decreaseMenu();
            }
            #endregion Up Button
        }

        /// <summary>
        /// Updates Credits Menu logic
        /// </summary>
        private void update_CreditsMenu()
        {
            #region Select Button
            if (_currentGamePadState.IsButtonDown(gp_back) && _previousGamePadState.IsButtonUp(gp_back) ||
                _currentGamePadState.IsButtonDown(gp_select) && _previousGamePadState.IsButtonUp(gp_select) ||
                (_currentKeyboardState.IsKeyDown(kb_back) && _previousKeyboardState.IsKeyUp(kb_back)) ||
                (_currentKeyboardState.IsKeyDown(kb_select) && _previousKeyboardState.IsKeyUp(kb_select)))
            {
                setActiveWindow(ref _active_MainMenu);
            }
            #endregion Select Button
        }

        /// <summary>
        /// Updates Gaming Tutorial logic
        /// </summary>
        private void update_GamingTutorial()
        {
            #region Select Button
            if (_currentGamePadState.IsButtonDown(gp_select) && _previousGamePadState.IsButtonUp(gp_select) ||
                (_currentKeyboardState.IsKeyDown(kb_select) && _previousKeyboardState.IsKeyUp(kb_select)))
            {
                _active_GamingTutorial = false;
            }
            #endregion Select Button
        }

        /// <summary>
        /// Updates Gaming logic
        /// </summary>
        private void update_Gaming()
        {
            #region Timer
            _elapsedTime += _gameTime.ElapsedGameTime.TotalMilliseconds;
            // If block even exists
            if (_currentShape.checkExisting())
            {
                // If elapsed time more or eq time interval
                if (_elapsedTime >= _timeInterval)
                {
                    // If moving down successful
                    if (_currentShape.moveShapeDown(_table))
                    { }
                    else
                    {
                        _currentShape.makeShapeTrue(_table);
                        _nextShape.changeTable(_table);
                        _currentShape = new Shape(_nextShape);
                        _nextShape.createNewShape(_tableNextShape);
                    }
                    _elapsedTime -= _timeInterval;
                }
            }
            else
            {
                _nextShape.createNewShape(_tableNextShape);
                _nextShape.changeTable(_table);
                _currentShape = new Shape(_nextShape);
                _nextShape.createNewShape(_tableNextShape);
            }
            #endregion Timer

            #region Left Button
            if ((-1f * _currentGamePadState.ThumbSticks.Left.X >= _XLimit && -1f * _previousGamePadState.ThumbSticks.Left.X < _XLimit) ||
                (_currentGamePadState.IsButtonDown(gp_left) && _previousGamePadState.IsButtonUp(gp_left)) ||
                (_currentKeyboardState.IsKeyDown(kb_aleft) && _previousKeyboardState.IsKeyUp(kb_aleft)))
            {
                _currentShape.moveShapeLeft(_table);
            }
            #endregion Left Button

            #region Right Button
            if ((_currentGamePadState.ThumbSticks.Left.X >= _XLimit && _previousGamePadState.ThumbSticks.Left.X < _XLimit) ||
                (_currentGamePadState.IsButtonDown(gp_right) && _previousGamePadState.IsButtonUp(gp_right)) ||
                (_currentKeyboardState.IsKeyDown(kb_dright) && _previousKeyboardState.IsKeyUp(kb_dright)))
            {
                _currentShape.moveShapeRight(_table);
            }
            #endregion Right Button

            #region Down Button
            if ((-1f * _currentGamePadState.ThumbSticks.Left.Y >= _YLimit && -1f * _previousGamePadState.ThumbSticks.Left.Y < _YLimit) ||
                (_currentGamePadState.IsButtonDown(gp_moveDown) && _previousGamePadState.IsButtonUp(gp_moveDown)) ||
                (_currentKeyboardState.IsKeyDown(kb_sdown) && _previousKeyboardState.IsKeyUp(kb_sdown)))
            {
                _currentShape.moveShapeDown(_table);

                _elapsedTime += _gameTime.ElapsedGameTime.TotalMilliseconds;
                // If elapsed time more or eq time interval
                if (_elapsedTime >= _timeInterval)
                {
                    _elapsedTime -= _timeInterval;
                }
            }
            #endregion Down Button

            #region DropDown Button
            if ((_currentGamePadState.IsButtonDown(gp_dropdown) && _previousGamePadState.IsButtonUp(gp_dropdown)) ||
                (_currentKeyboardState.IsKeyDown(kb_dropdown) && _previousKeyboardState.IsKeyUp(kb_dropdown)))
            {
                // If successfully moved to the bottom
                if (_currentShape.tryFindBottom(_table) != 0)
                {
                    _currentShape.makeShapeTrue(_table);
                    _nextShape.changeTable(_table);
                    _currentShape = new Shape(_nextShape);
                    _nextShape.createNewShape(_tableNextShape);

                    _elapsedTime += _gameTime.ElapsedGameTime.TotalMilliseconds;
                    // If elapsed time more or eq time interval
                    if (_elapsedTime >= _timeInterval)
                    {
                        _elapsedTime -= _timeInterval;
                    }
                }
            }
            #endregion DropDown Button

            #region Rotate Left
            if ((_currentGamePadState.IsButtonDown(gp_rotateLeft) && _previousGamePadState.IsButtonUp(gp_rotateLeft)) ||
                (_currentKeyboardState.IsKeyDown(kb_rotateLeft) && _previousKeyboardState.IsKeyUp(kb_rotateLeft)))
            {
                // If block even exists
                if (_currentShape.checkExisting())
                {
                    _currentShape.rotateLeft(_table);
                }
            }
            #endregion Rotate Left

            #region Rotate Right
            if ((_currentGamePadState.IsButtonDown(gp_rotateRight) && _previousGamePadState.IsButtonUp(gp_rotateRight)) ||
                (_currentKeyboardState.IsKeyDown(kb_rotateRight) && _previousKeyboardState.IsKeyUp(kb_rotateRight)))
            {
                // If block even exists
                if (_currentShape.checkExisting())
                {
                    _currentShape.rotateRight(_table);
                }
            }
            #endregion Rotate Rigth

            #region Pause Button
            if ((_currentGamePadState.IsButtonDown(gp_pause) && _previousGamePadState.IsButtonUp(gp_pause)) ||
                (_currentKeyboardState.IsKeyDown(kb_pause) && _previousKeyboardState.IsKeyUp(kb_pause)))
            {
                setActiveWindow(ref _active_Gaming, ref _active_GamingPauseMenu);
            }
            #endregion Pause Button

            #region Ghost Mode
            if ((_currentGamePadState.IsButtonUp(Buttons.Y) && _previousGamePadState.IsButtonDown(Buttons.Y)) ||
                (_currentKeyboardState.IsKeyUp(Keys.G) && _previousKeyboardState.IsKeyDown(Keys.G)))
            {
                _ghost_mode = !_ghost_mode;
            }
            #endregion Ghost Mode

            #region Lines Check
            short count = 0;
            for (int y = _table.getRow() - 1; y >= 0; y--)
            {
                int numCollected = 0;
                for (int x = 0; x < _table.getCol(); x++)
                {
                    if (_table.tableContains[y, x] == true)
                        numCollected++;
                }
                // If every cell in row is filled
                if (numCollected == _table.getCol())
                {
                    // Clead every cell in row
                    for (int x = 0; x < _table.getCol(); x++)
                    {
                        _table.tableContains[y, x] = false;
                    }
                    // Move upside cells to the bottom
                    if (y != 0)
                        for (int yy = y; yy > 0; yy--)
                            for (int xx = 0; xx < _table.getCol(); xx++)
                                _table.tableContains[yy, xx] = _table.tableContains[yy - 1, xx];
                    count++;
                }
            }
            if (count != 0)
                _currentScore.addScore(count);
            #endregion Lines Check

            /*
            if (DEBUG)
            {
                #region Plus Button
                if ((_currentGamePadState.IsButtonDown(Buttons.RightTrigger) && _previousGamePadState.IsButtonUp(Buttons.RightTrigger)) ||
                    (_currentKeyboardState.IsKeyDown(Keys.OemPlus) && _previousKeyboardState.IsKeyUp(Keys.OemPlus) &&
                    (_currentKeyboardState.IsKeyDown(Keys.LeftShift) || _currentKeyboardState.IsKeyDown(Keys.RightShift))))
                {
                    if (_timeInterval > 250 && _timeInterval <= 2000)
                    {
                        _timeInterval /= 2;
                    }
                }
                #endregion Plus Button

                #region Minus Button
                if ((_currentGamePadState.IsButtonDown(Buttons.LeftTrigger) && _previousGamePadState.IsButtonUp(Buttons.LeftTrigger)) ||
                    (_currentKeyboardState.IsKeyDown(Keys.OemMinus) && _previousKeyboardState.IsKeyUp(Keys.OemMinus) &&
                    (_currentKeyboardState.IsKeyDown(Keys.LeftShift) || _currentKeyboardState.IsKeyDown(Keys.RightShift))))
                {
                    if (_timeInterval >= 250 && _timeInterval < 2000)
                    {
                        _timeInterval *= 2;
                    }
                }
                #endregion Minus Button

                #region Backspace Button
                if (_currentKeyboardState.IsKeyDown(Keys.Back))
                {
                    _table.clear();
                }
                #endregion Backspace Button

                #region Enter Button
                if (_currentKeyboardState.IsKeyDown(Keys.Enter) && _previousKeyboardState.IsKeyUp(Keys.Enter))
                {
                    _currentShape.createNewShape(_table);
                    _timeInterval = 1000;
                }
                #endregion Enter Button

                #region 0 Numpad Button
                if (_currentKeyboardState.IsKeyDown(Keys.NumPad0) && _previousKeyboardState.IsKeyUp(Keys.NumPad0))
                {
                    _timeInterval = 10000000;
                }
                #endregion 0 Numpad Button

                #region 1 Numpad Button
                if (_currentKeyboardState.IsKeyDown(Keys.NumPad1) && _previousKeyboardState.IsKeyUp(Keys.NumPad1))
                {
                    _timeInterval = 1000;
                }
                #endregion 1 Numpad Button
            }
            */
        }

        /// <summary>
        /// Updates Gaming Pause Menu logic
        /// </summary>
        private void update_GamingPauseMenu()
        {
            #region Timer
            _elapsedTime += _gameTime.ElapsedGameTime.TotalMilliseconds;
            // If elapsed time more or eq time interval
            if (_elapsedTime >= _timeInterval)
            {
                _elapsedTime -= _timeInterval;
            }
            #endregion Timer

            #region Select Button
            if (_currentGamePadState.IsButtonDown(gp_select) && _previousGamePadState.IsButtonUp(gp_select) ||
                (_currentKeyboardState.IsKeyDown(kb_select) && _previousKeyboardState.IsKeyUp(kb_select)))
            {
                switch (_gamingPauseMenu.getMenus()[_gamingPauseMenu.getSelectedIndex()])
                {
                    case "Back":
                        setActiveWindow(ref _active_Gaming);
                        break;
                    case "Quit":
                        clearGame();
                        setActiveWindow(ref _active_MainMenu);
                        break;
                }
            }
            #endregion Select Button

            #region Down Button
            if ((-1f * _currentGamePadState.ThumbSticks.Left.Y >= _YLimit && -1f * _previousGamePadState.ThumbSticks.Left.Y < _YLimit) ||
                (_currentGamePadState.IsButtonDown(gp_down) && _previousGamePadState.IsButtonUp(gp_down)) ||
                (_currentKeyboardState.IsKeyDown(kb_down) && _previousKeyboardState.IsKeyUp(kb_down)))
            {
                _gamingPauseMenu.increaseMenu();
            }
            #endregion Down Button

            #region Up Button
            if ((_currentGamePadState.ThumbSticks.Left.Y >= _YLimit && _previousGamePadState.ThumbSticks.Left.Y < _YLimit) ||
                (_currentGamePadState.IsButtonDown(gp_up) && _previousGamePadState.IsButtonUp(gp_up)) ||
                (_currentKeyboardState.IsKeyDown(kb_up) && _previousKeyboardState.IsKeyUp(kb_up)))
            {
                _gamingPauseMenu.decreaseMenu();
            }
            #endregion Up Button
        }
        #endregion Update

        #region Draw
        protected override void Draw(GameTime gameTime)
        {
            // Active window
            if (_active_MainMenu)
                draw_MainMenu(true);
            else
            if (_active_SettingsMenu)
                draw_SettingsMenu(true);
            else
            if (_active_CreditsMenu)
                draw_CreditsMenu(true);
            else
            if (_active_Gaming)
            {
                draw_Gaming(true);
                if (_active_GamingTutorial)
                    draw_GamingTutorial(false);
                if (_active_GamingPauseMenu)
                    draw_GamingPauseMenu(false);
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws Main Menu
        /// </summary>
        private void draw_MainMenu(bool clear)
        {
            if (clear)
                GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            // Drawing dim
            _spriteBatch.Draw(blackFill, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black * 0.8f);

            // Drawing Main Menus
            for (int n = 0; n < _mainMenu.getNumberOfMenus(); n++)
            {
                if (_mainMenu.getSelectedIndex() == n)
                    fontColor = Color.Gray;
                _spriteBatch.DrawString(_debugFont,
                                        _mainMenuMenus[n],
                                        new Vector2((_graphics.PreferredBackBufferWidth / 2) - ((int)_debugFont.MeasureString(_mainMenuMenus[n]).Length() / 2),
                                                    (_graphics.PreferredBackBufferHeight / 2) - (((_mainMenu.getNumberOfMenus() + 1) / 2) * (50)) + (n * 50)),
                                        fontColor);
                    fontColor = Color.White;
            }

            if (DEBUG)
            {
                var debug_version = "Debug build: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _spriteBatch.DrawString(_debugFont,
                                        debug_version,
                                        new Vector2(_graphics.PreferredBackBufferWidth - _debugFont.MeasureString(debug_version).Length(),
                                                    _graphics.PreferredBackBufferHeight - 15),
                                        Color.White);
            }
            else
            {
                var release_version = "Release build: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _spriteBatch.DrawString(_debugFont,
                                        release_version,
                                        new Vector2(_graphics.PreferredBackBufferWidth - _debugFont.MeasureString(release_version).Length(),
                                                    _graphics.PreferredBackBufferHeight - 15),
                                        Color.White);
            }

            _spriteBatch.End();
        }

        /// <summary>
        /// Draws Settings Menu
        /// </summary>
        private void draw_SettingsMenu(bool clear)
        {
            if (clear)
                GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            // Drawing dim
            _spriteBatch.Draw(blackFill, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black * 0.8f);

            // Drawing Settings Menus
            for (int n = 0; n < _settingsMenu.getNumberOfMenus(); n++)
            {
                if (_settingsMenu.getSelectedIndex() == n)
                    fontColor = Color.Gray;
                _spriteBatch.DrawString(_debugFont,
                                        _settingsMenus[n],
                                        new Vector2((_graphics.PreferredBackBufferWidth / 2) - ((int)_debugFont.MeasureString(_settingsMenus[n]).Length() / 2),
                                                    (_graphics.PreferredBackBufferHeight / 2) - (((_settingsMenu.getNumberOfMenus() + 1) / 2) * (50)) + (n * 50)),
                                        fontColor);
                fontColor = Color.White;
            }

            if (DEBUG)
            {
                var debug_version = "Debug build: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _spriteBatch.DrawString(_debugFont,
                                        debug_version,
                                        new Vector2(_graphics.PreferredBackBufferWidth - _debugFont.MeasureString(debug_version).Length(),
                                                    _graphics.PreferredBackBufferHeight - 15),
                                        Color.White);
            }
            else
            {
                var release_version = "Release build: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _spriteBatch.DrawString(_debugFont,
                                        release_version,
                                        new Vector2(_graphics.PreferredBackBufferWidth - _debugFont.MeasureString(release_version).Length(),
                                                    _graphics.PreferredBackBufferHeight - 15),
                                        Color.White);
            }

            _spriteBatch.End();
        }

        /// <summary>
        /// Draws Credits Menu
        /// </summary>
        /// <param name="clear"></param>
        private void draw_CreditsMenu(bool clear)
        {
            if (clear)
                GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            // Drawing dim
            _spriteBatch.Draw(blackFill, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black * 0.8f);

            // Drawing Settings Menus
            _spriteBatch.DrawString(_debugFont,
                                    _creditsMenus[0],
                                    new Vector2((_graphics.PreferredBackBufferWidth / 2) - ((int)_debugFont.MeasureString(_creditsMenus[0]).Length() / 2),
                                                (100)),
                                    fontColor);
            for (int n = 1; n < _creditsMenu.getNumberOfMenus(); n++)
            {
                _spriteBatch.DrawString(_debugFont,
                                        _creditsMenus[n],
                                        new Vector2((_graphics.PreferredBackBufferWidth / 2) - ((int)_debugFont.MeasureString(_creditsMenus[n]).Length() / 2),
                                                    (_graphics.PreferredBackBufferHeight / 2) - (((_creditsMenu.getNumberOfMenus() + 1) / 2) * (50)) + (n * 50)),
                                        fontColor);
                fontColor = Color.White;
            }

            if (DEBUG)
            {
                var debug_version = "Debug build: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _spriteBatch.DrawString(_debugFont,
                                        debug_version,
                                        new Vector2(_graphics.PreferredBackBufferWidth - _debugFont.MeasureString(debug_version).Length(),
                                                    _graphics.PreferredBackBufferHeight - 15),
                                        Color.White);
            }
            else
            {
                var release_version = "Release build: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _spriteBatch.DrawString(_debugFont,
                                        release_version,
                                        new Vector2(_graphics.PreferredBackBufferWidth - _debugFont.MeasureString(release_version).Length(),
                                                    _graphics.PreferredBackBufferHeight - 15),
                                        Color.White);
            }

            _spriteBatch.End();
        }

        /// <summary>
        /// Draws Tutorial before Gaming
        /// </summary>
        /// <param name="clear"></param>
        private void draw_GamingTutorial(bool clear)
        {
            if (clear)
                GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            // Drawing dim
            _spriteBatch.Draw(blackFill, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black * 0.8f);

            // Drawing tutorial
            _spriteBatch.DrawString(_debugFont,
                                    "Q/LB - Rotate Left",
                                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - _debugFont.MeasureString("Q/LB - Rotate Left").Length() / 2, 150),
                                    Color.White);
            _spriteBatch.DrawString(_debugFont,
                                    "E/RB - Rotate Right",
                                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - _debugFont.MeasureString("E/RB - Rotate Right").Length() / 2, 200),
                                    Color.White);
            _spriteBatch.DrawString(_debugFont,
                                    "A/Left Pad/Left Stick to the left - Move Left",
                                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - _debugFont.MeasureString("A/Left Pad/Left Stick to the left - Move Left").Length() / 2, 250),
                                    Color.White);
            _spriteBatch.DrawString(_debugFont,
                                    "D/Right Pad/Left Stick to the right - Move Right",
                                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - _debugFont.MeasureString("D/Right Pad/Left Stick to the right - Move Right").Length() / 2, 300),
                                    Color.White);
            _spriteBatch.DrawString(_debugFont,
                                    "S/Down Pad/Left Stick to the down - Move Down",
                                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - _debugFont.MeasureString("S/Down Pad/Left Stick to the down - Move Down").Length() / 2, 350),
                                    Color.White);
            _spriteBatch.DrawString(_debugFont,
                                    "Space/A Button/Up Pad - Drop Down",
                                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - _debugFont.MeasureString("Space/A Button - Drop Down").Length() / 2, 400),
                                    Color.White);
            _spriteBatch.DrawString(_debugFont,
                                    "Escape/Start - Pause",
                                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - _debugFont.MeasureString("Escape/Start - Pause").Length() / 2, 450),
                                    Color.White);

            _spriteBatch.End();
        }

        /// <summary>
        /// Draws Gaming
        /// </summary>
        private void draw_Gaming(bool clear)
        {
            if (clear)
                GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            // Drawing table
            for (int y = 0; y < _table.getRow(); y++)
                for (int x = 0; x < _table.getCol(); x++)
                    if (_table.tableContains[y, x] == true)
                        _spriteBatch.Draw(_currentBlockTex,
                            _tableStart + new Vector2(_currentBlockTex.Width * x, _currentBlockTex.Height * y) + new Vector2(_table.getSpacing() * x, _table.getSpacing() * y),
                            Color.White);
                    else
                        _spriteBatch.Draw(_emptyBlockTex,
                            _tableStart + new Vector2(_emptyBlockTex.Width * x, _emptyBlockTex.Height * y) + new Vector2(_table.getSpacing() * x, _table.getSpacing() * y),
                            Color.White);

            // Drawing next shape table
            for (int y = 0; y < _tableNextShape.getRow(); y++)
                for (int x = 0; x < _tableNextShape.getCol(); x++)
                    _spriteBatch.Draw(_emptyBlockTex,
                        _tableNextShapeStart + new Vector2(_emptyBlockTex.Width * x, _emptyBlockTex.Height * y) + new Vector2(_tableNextShape.getSpacing() * x, _tableNextShape.getSpacing() * y),
                        Color.White);

            // Drawing Ghost shape
            if (_ghost_mode)
            {
                if (_currentShape.checkExisting())
                {
                    var offset = _currentShape.tryFindBottomGhost(_table);
                    for (int y = (int)_currentShape._startBlockPos.Y + offset; y <= (int)_currentShape._endBlockPos.Y + offset; y++)
                        for (int x = (int)_currentShape._startBlockPos.X; x <= (int)_currentShape._endBlockPos.X; x++)
                        {
                            if (_currentShape.currentShape[(_currentShape.shapeDimension - 1) - ((int)_currentShape._endBlockPos.Y + offset - y),
                                (_currentShape.shapeDimension - 1) - ((int)_currentShape._endBlockPos.X - x)] == true)
                                _spriteBatch.Draw(_ghostBlockTex,
                                    _tableStart + new Vector2(_currentBlockTex.Width * x, _currentBlockTex.Height * y) + new Vector2(_table.getSpacing() * x, _table.getSpacing() * y),
                                    Color.White);
                        }
                }
            }
            _spriteBatch.End();

            // Drawing current shape
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            _blockColorize.Parameters["externalColorR"].SetValue(_currentShape.getBlockColorR());
            _blockColorize.Parameters["externalColorG"].SetValue(_currentShape.getBlockColorG());
            _blockColorize.Parameters["externalColorB"].SetValue(_currentShape.getBlockColorB());
            _blockColorize.CurrentTechnique.Passes[0].Apply();
            if (_currentShape.checkExisting())
                for (int y = (int)_currentShape._startBlockPos.Y; y <= (int)_currentShape._endBlockPos.Y; y++)
                    for (int x = (int)_currentShape._startBlockPos.X; x <= (int)_currentShape._endBlockPos.X; x++)
                    {
                        if (_currentShape.currentShape[(_currentShape.shapeDimension - 1) - ((int)_currentShape._endBlockPos.Y - y),
                            (_currentShape.shapeDimension - 1) - ((int)_currentShape._endBlockPos.X - x)] == true)
                            _spriteBatch.Draw(_currentBlockTex,
                                _tableStart + new Vector2(_currentBlockTex.Width * x, _currentBlockTex.Height * y) + new Vector2(_table.getSpacing() * x, _table.getSpacing() * y),
                                Color.White);
                    }
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            // Drawing next shape
            if (_nextShape.checkExisting())
                for (int y = (int)_nextShape._startBlockPos.Y; y <= (int)_nextShape._endBlockPos.Y; y++)
                    for (int x = (int)_nextShape._startBlockPos.X; x <= (int)_nextShape._endBlockPos.X; x++)
                    {
                        if (_nextShape.currentShape[(_nextShape.shapeDimension - 1) - ((int)_nextShape._endBlockPos.Y - y),
                            (_nextShape.shapeDimension - 1) - ((int)_nextShape._endBlockPos.X - x)] == true)
                            _spriteBatch.Draw(_currentBlockTex,
                                _tableNextShapeStart + new Vector2(_currentBlockTex.Width * x, _currentBlockTex.Height * y) + new Vector2(_tableNextShape.getSpacing() * x, _tableNextShape.getSpacing() * y),
                                Color.White);
                    }

            _spriteBatch.DrawString(_debugFont, "Score: " + _currentScore.getScore(), new Vector2(_tableNextShapeStart.X, _tableNextShapeStart.Y + _tableNextShape.getRow() * _emptyBlockTex.Width + _tableNextShape.getSpacing() * 4), Color.White);
            _spriteBatch.End();
        }

        /// <summary>
        /// Draws Gaming Pause Menu
        /// </summary>
        private void draw_GamingPauseMenu(bool clear)
        {
            if (clear)
                GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            // Drawing dim
            _spriteBatch.Draw(blackFill, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black * 0.8f);

            // Drawing "Paused" string
            _spriteBatch.DrawString(_debugFont,
                                    "Paused",
                                    new Vector2((_graphics.PreferredBackBufferWidth / 2) - ((int)_debugFont.MeasureString("Paused").Length() / 2),
                                                (200)),
                                    fontColor);

            // Drawing Settings Menus
            for (int n = 0; n < _gamingPauseMenu.getNumberOfMenus(); n++)
            {
                if (_gamingPauseMenu.getSelectedIndex() == n)
                    fontColor = Color.Gray;
                _spriteBatch.DrawString(_debugFont,
                                        _gamingPauseMenus[n],
                                        new Vector2((_graphics.PreferredBackBufferWidth / 2) - ((int)_debugFont.MeasureString(_gamingPauseMenus[n]).Length() / 2),
                                                    (_graphics.PreferredBackBufferHeight / 2) - (((_gamingPauseMenu.getNumberOfMenus() + 1) / 2) * (50)) + (n * 50)),
                                        fontColor);
                fontColor = Color.White;
            }

            _spriteBatch.End();
        }
        #endregion Draw

        /// <summary>
        /// Changes actual windows by one value
        /// </summary>
        /// <param name="value"></param>
        private void setActiveWindow(ref bool value)
        {
            // Disable all
            disableActiveWindows();

            // Enable value
            value = true;
        }

        /// <summary>
        /// Changes actual windows by two values
        /// </summary>
        /// <param name="value"></param>
        private void setActiveWindow(ref bool value, ref bool value2)
        {
            // Disable all
            disableActiveWindows();

            // Enable value
            value = true;
            value2 = true;
        }

        /// <summary>
        /// Disables all actual windows
        /// </summary>
        private void disableActiveWindows()
        {
            // Disable all
            _active_MainMenu = false;
            _active_SettingsMenu = false;
            _active_CreditsMenu = false;
            _active_Gaming = false;
            _active_GamingPauseMenu = false;
        }

        /// <summary>
        /// Cleares all the tables, shapes, etc
        /// </summary>
        private void clearGame()
        {
            _table.clear();
            _tableNextShape.clear();
            _currentScore.clear();
            // _currentShape will be destructed by himself
            // _nextShape will be destructed by himself

        }
    }
}
