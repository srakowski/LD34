using Coldsteel;
using LD34.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    enum TurnState
    {
        INVALID = 0,
        PLAYER,
        WAITING_PLAYER,
        BOARD,
        WAITING_BOARD
    }

    class GameBoard : Behavior
    {
        private const int SLOT_SIZE = 48;
        private int _size;
        private GameBoardSlot[,] _slots;
        private Ship _ship = null;
        private List<LineRenderer> _lineRenderers;
        private GameBoardTextures _gbTextures;

        public HudConsole Hud { get { return _hud; } }
        private HudConsole _hud;
        public StarFieldRenderer StarFieldRenderer { get; set; }
        public TurnState TurnState { get { return _turnState; } }
        private TurnState _turnState = TurnState.PLAYER;

        private GameProgressManager _progress;

        public GameBoard(GameObject obj, HudConsole hud,
            Ship ship, int size, GameBoardTextures gbTextures,
             GameProgressManager progress, bool tutorial, bool finalPlanet)
            : base(obj)
        {
            this._size = size;
            this._gbTextures = gbTextures;
            this._ship = ship;
            this._hud = hud;
            this._progress = progress;

            GenerateGameBoard(ship, tutorial, finalPlanet);
            CreateGridLines();

            var go = new GameObject();
            this.GameObject.AddGameObject(go);
            go.Behaviors.Add(new GOBehv(go, progress));
        }

        private void GenerateGameBoard(Ship ship, bool tutorial, bool final)
        {
            if (tutorial)
            {
                GenerateTutorialBoard(ship);
                return;
            }

            _slots = new GameBoardSlot[_size, _size];
            for (var x = 0; x < _size; x++)
                for (var y = 0; y < _size; y++)
                    _slots[x, y] = new GameBoardSlot(new Point(x, y), BoardToInitialWorldPoint(x, y));

            Random r = new Random();
            var p = NextPosition(r);           
            PlaceGamePiece(p.X, p.Y, ship);


            if (!final)
            {
                p = NextPosition(r);
                var jg = new JumpGate(_gbTextures.JumpGate, _gbTextures._jump);
                this.GameObject.AddGameObject(jg);
                PlaceGamePiece(p.X, p.Y, jg);
            }
            else
            {
                p = NextPosition(r);
                var fp = new FinalPlanet(_gbTextures.PlanetTexture);
                this.GameObject.AddGameObject(fp);
                PlaceGamePiece(p.X, p.Y, fp);
            }

            var slotCount = _size * _size;
            var spawnObjs = r.Next(1, _size * 2);
            for (var i = 0; i < spawnObjs; i++)
            {
                p = NextPosition(r);
                var type = r.Next(5);
                switch (type)
                {
                case 0:
                    Asteroid a = new Asteroid(_gbTextures.AsteroidTexture, r, _gbTextures._expload);
                    PlaceGamePiece(p.X, p.Y, a);
                    this.GameObject.AddGameObject(a);
                    break;
                case 1:
                    PeaceKeepingShip b = new PeaceKeepingShip(_gbTextures.PeaceKeeperShip1, r, _gbTextures._expload);
                    PlaceGamePiece(p.X, p.Y, b);
                    this.GameObject.AddGameObject(b);
                    break;
                case 2:
                    TradeOutputStation c = new TradeOutputStation(_gbTextures.TradeStation, _gbTextures._convert);
                    PlaceGamePiece(p.X, p.Y, c);
                    this.GameObject.AddGameObject(c);
                    break;
                case 3:
                    Planet d = new Planet(_gbTextures.PlanetTexture, r);
                    PlaceGamePiece(p.X, p.Y, d);
                    this.GameObject.AddGameObject(d);
                    break;
                case 4:
                    Pirate e = new Pirate(_gbTextures.Pirate, r, _gbTextures._expload);
                    PlaceGamePiece(p.X, p.Y, e);
                    GameConsole.Log("PIRATE: x: " + p.X.ToString() + " y: " + p.Y.ToString());
                    this.GameObject.AddGameObject(e);
                    break;
                default: break;
                }                            
            }                      
        }

        private Point NextPosition(Random r)
        {
            var px = r.Next(0, _size);
            var py = r.Next(0, _size);
            while (_slots[px, py].occupant != null)
            {
                px = r.Next(0, _size);
                py = r.Next(0, _size);
            }
            return new Point(px, py);
        }

        private void GenerateTutorialBoard(Ship ship)
        {
            _size = 3;
            var slotCount = _size * _size;
            _slots = new GameBoardSlot[_size, _size];
            for (var x = 0; x < _size; x++)
                for (var y = 0; y < _size; y++)
                    _slots[x, y] = new GameBoardSlot(new Point(x, y), BoardToInitialWorldPoint(x, y));

            PlaceGamePiece(1, 1, ship);
            Random r = new Random();

            Asteroid a = new Asteroid(_gbTextures.AsteroidTexture, r, _gbTextures._expload);            
            this.GameObject.AddGameObject(a);
            PlaceGamePiece(2, 0, a);

            var pk = new PeaceKeepingShip(_gbTextures.PeaceKeeperShip1, r, _gbTextures._expload);
            this.GameObject.AddGameObject(pk);
            PlaceGamePiece(0, 1, pk);

            var to = new TradeOutputStation(_gbTextures.TradeStation, _gbTextures._convert);
            this.GameObject.AddGameObject(to);
            PlaceGamePiece(0, 0, to);
            
            var jg = new JumpGate(_gbTextures.JumpGate, _gbTextures._jump);
            this.GameObject.AddGameObject(jg);
            PlaceGamePiece(2, 2, jg);            
        }

        private void PlaceGamePiece(int x, int y, IGamePiece gp)
        {
            _slots[x, y].SetOccupent(gp, null, true);
            _slots[x, y].UpdateOccupantPos();
        }

        public void MovePieceTo(Point move, IGamePiece gp, Action completed)
        {
            if (move.X < 0 || move.X >= _size || move.Y < 0 || move.Y >= _size)
            {
                completed?.Invoke();
                return;
            }

            if (_slots[move.X, move.Y].occupant != null)
                if (!(gp is Ship) || !_slots[move.X, move.Y].occupant.IsTraversable)
                {
                    completed?.Invoke();
                    return;
                }

            _slots[move.X, move.Y].SetOccupent(gp, completed);
        }

        private Vector2 BoardToInitialWorldPoint(int x, int y)
        {
            var movex = x * SLOT_SIZE - (y * SLOT_SIZE);
            var movey = ((x + y) * SLOT_SIZE) - ((_size / 2) * (SLOT_SIZE * 2));
            var p = new Vector2(movex, movey);
            return p;
        }

        private void CreateGridLines()
        {
            var lcount = _size;
            _lineRenderers = new List<LineRenderer>();
            for (var l = 0; l <= lcount; l++)
            {
                var lineGO = new GameObject();
                var renderer = new LineRenderer(lineGO, this.ScreenManager.GraphicsDevice);
                renderer.AddVector(new Vector2(-SLOT_SIZE * (lcount - l), SLOT_SIZE * l));
                renderer.AddVector(new Vector2(SLOT_SIZE * l, -SLOT_SIZE * (lcount - l)));
                renderer.Color = new Color(100, 100, 100, 60);
                _lineRenderers.Add(renderer);
                lineGO.Renderer = renderer;
                this.GameObject.AddGameObject(lineGO);

                lineGO = new GameObject();
                renderer = new LineRenderer(lineGO, this.ScreenManager.GraphicsDevice);
                renderer.AddVector(new Vector2(SLOT_SIZE * (lcount - l), SLOT_SIZE * l));
                renderer.AddVector(new Vector2(-SLOT_SIZE * l, -SLOT_SIZE * (lcount - l)));
                renderer.Color = new Color(100, 100, 100, 60);
                _lineRenderers.Add(renderer);
                lineGO.Renderer = renderer;
                this.GameObject.AddGameObject(lineGO);
            }
        }

        #region Handling User Actions

        public void ActionLeft()
        {       
            var point = _ship.LeftPoint;
            if (point.X >= 0 && point.X < _size && point.Y >= 0 && point.Y < _size)
            {                
                var occupant = _slots[point.X, point.Y].occupant;
                if (occupant == null)
                {
                    _turnState = TurnState.WAITING_PLAYER;
                    _ship.MoveLeft(this, null);
                }
                else
                {
                    _turnState = TurnState.WAITING_PLAYER;
                    if (occupant.IsSelected)
                        occupant.Interact(this, _ship);
                    else
                        occupant.Select(_hud, this, _ship);
                }
            }
            else
            {
                _hud.Log("Uncharted space is cold and unforgiving...");
            }
        }

        public void ActionRight()
        {            
            var point = _ship.RightPoint;
            if (point.X >= 0 && point.X < _size && point.Y >= 0 && point.Y < _size)
            {
                _turnState = TurnState.WAITING_PLAYER;
                var occupant = _slots[point.X, point.Y].occupant;
                if (occupant == null)
                {
                    _turnState = TurnState.WAITING_PLAYER;
                    _ship.MoveRight(this, null);
                }
                else
                {
                    _turnState = TurnState.WAITING_PLAYER;
                    if (occupant.IsSelected)
                        occupant.Interact(this, _ship);
                    else
                        occupant.Select(_hud, this, _ship);
                }
            }
            else
            {                
                _hud.Log("Uncharted space is cold and unforgiving...");
            }
        }

        public void ActionBoth(Direction dir)
        {
            _ship.BeginRotate();
            _turnState = TurnState.WAITING_PLAYER;            
            RotateBoard(dir == Direction.LEFT);
        }

        #endregion

        private void RotateBoard(bool left)
        {
            if (left)
            {
                if (_ship.Bearing == Bearing.NORTH) _ship.Bearing = Bearing.WEST;
                else if (_ship.Bearing == Bearing.WEST) _ship.Bearing = Bearing.SOUTH;
                else if (_ship.Bearing == Bearing.SOUTH) _ship.Bearing = Bearing.EAST;
                else if (_ship.Bearing == Bearing.EAST) _ship.Bearing = Bearing.NORTH;
            }
            else
            {
                if (_ship.Bearing == Bearing.NORTH) _ship.Bearing = Bearing.EAST;
                else if (_ship.Bearing == Bearing.EAST) _ship.Bearing = Bearing.SOUTH;
                else if (_ship.Bearing == Bearing.SOUTH) _ship.Bearing = Bearing.WEST;
                else if (_ship.Bearing == Bearing.WEST) _ship.Bearing = Bearing.NORTH;
            }

            _hud.Log("You are now heading " + _ship.Bearing.ToString());

            this.Behaviors.Add(new RotateBoardBehavior(this.GameObject,
                _ship.Slot.Pos, _lineRenderers, _slots, StarFieldRenderer, left ? 3 : -3,
                new Action(() => { _ship.EndRotate(); })));
        }

        public override void Update(GameTime gameTime)
        {
            if (_turnState == TurnState.BOARD)
            {
                foreach (var slot in _slots)
                    slot.occupant?.BeginTurn(gameTime, this, _ship);

                _turnState = TurnState.WAITING_BOARD;
            }
            else if (_turnState == TurnState.WAITING_BOARD)
            {
                var over = true;
                foreach (var slot in _slots)
                    if (slot.occupant != null)
                        if (!slot.occupant.TurnOver && !(slot.occupant is Ship))
                        {
                            over = false;
                            break;
                        }

                if (over)
                    _turnState = TurnState.PLAYER;
            }
            else if (_turnState == TurnState.WAITING_PLAYER)
            {
                if (_ship.TurnOver)
                    _turnState = TurnState.BOARD;
            }
        }

        public void DestroyPeice(BaseGamePiece piece)
        {
            piece.Slot.Clear();
            this.GameObject.RemoveGameObject(piece);
        }

        public void EndLevel()
        {
            _hud.Log("Fear like limits are often just an illusion...");
            _progress.Level++;
            LoadingScreen.LoadScreen(this.ScreenManager, new GameStageScreen(new GameplayStage(_progress)));
        }
    }
}
