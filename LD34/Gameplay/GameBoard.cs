using Coldsteel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class GameBoard : Behavior
    {
        private const int SLOT_SIZE = 48;
        private int _size;
        private GameBoardSlot[,] _slots;
        private Ship _ship = null;
        private Selector _selector = null;

        private List<LineRenderer> _lineRenderers;
        private GameBoardTextures _gbTextures;
        private HudConsole _hud;

        public StarFieldRenderer StarFieldRenderer { get; set; }

        public GameBoard(GameObject obj, HudConsole hud,
            Ship ship, int size, GameBoardTextures gbTextures)
            : base(obj)
        {
            this._size = size;
            this._gbTextures = gbTextures;
            this._ship = ship;
            this._hud = hud;

            GenerateGameBoard(size, ship);
            CreateGridLines(size);
            CreateSelector(ship.Transform.Position);                      
        }

        private void CreateSelector(Vector2 playerPos)
        {
            this._selector = new Selector();
            this._selector.Renderer = new SpriteRenderer(this._selector, _gbTextures.SelecterTexture)
            {
                Color = new Color(60, 255, 60, 100)
            };
            this._selector.Transform.Position = playerPos;
            this.GameObject.AddGameObject(this._selector);
        }

        public void ActionLeft()
        {
            _selector.SelectLeft(_ship.Transform.Position, 
                new Action(Select),
                new Action(MoveShipLeft));            
        }

        private void Select()
        {            
        }

        private void MoveShipLeft()
        {
            _ship.MoveLeft();
            _selector.ResetSelection();
        }

        public void ActionRight()
        {
            _selector.SelectRight(_ship.Transform.Position, 
                new Action(Select),
                new Action(MoveShipRight));
        }

        private void MoveShipRight()
        {
            _ship.MoveRight();
            _selector.ResetSelection();
        }

        public void ActionBoth(Direction dir)
        {
            RotateBoard(dir == Direction.LEFT);            
        }

        private void GenerateGameBoard(int size, Ship ship)
        {
            var slotCount = size * size;

            _slots = new GameBoardSlot[size, size];
            Random r = new Random();
            var px = r.Next(0, size);
            var py = r.Next(0, size);
            MoveGamePieceTo(px, py, ship);
            slotCount--;

            px = r.Next(0, size);
            py = r.Next(0, size);
            Asteroid a = new Asteroid(_gbTextures.AsteroidTexture);            
            MoveGamePieceTo(px, py, a);
            this.GameObject.AddGameObject(a);
        }

        private void MoveGamePieceTo(int x, int y, IGamePiece gp)
        {
            _slots[x, y].occupant = gp;
            Point p = BoardToWorldPoint(x, y);
            gp.MoveTo(p.X, p.Y, true);


            //gp.MoveTo((x * SLOT_SIZE) - (y * SLOT_SIZE),
            //    -((_size / 2) * SLOT_SIZE) + (y * SLOT_SIZE), true);
        }

        private Point BoardToWorldPoint(int x, int y)
        {
            var movex = x * SLOT_SIZE - (y * SLOT_SIZE);
            var movey = ((x + y) * SLOT_SIZE) - ((_size / 2) * (SLOT_SIZE * 2));
            var p = new Point(movex, movey);
            return p;
        }

        private void CreateGridLines(int size)
        {
            var lcount = size;
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
                _ship.Transform.Position, _lineRenderers, _slots, StarFieldRenderer, left ? 3 : -3));
        }

        //public void SpawnAsteroid()
        //{
        //    var asteroid = new Asteroid(_asteroidTexture, _ship.Transform.Position + new Microsoft.Xna.Framework.Vector2(-SLOT_SIZE, -SLOT_SIZE));
        //    this.GameObject.AddGameObject(asteroid);
        //}
    }
}
