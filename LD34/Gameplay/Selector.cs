using Coldsteel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class Selector : GameObject
    {
        public int _selected;

        public Selector()
        {
            _selected = 0;
            this.RigidBody = new RigidBody(this);
        }

        public void SelectLeft(Vector2 shipPos, Action sel, Action onAct)
        {
            if (_selected >= 0)
            {
                _selected--;
                MoveCursor(shipPos);
                sel.Invoke();
            }
            else
            {
                onAct.Invoke();
            }
        }

        public void SelectRight(Vector2 shipPos, Action sel, Action onAct)
        {
            if (_selected <= 0)
            {
                _selected++;
                MoveCursor(shipPos);
                sel.Invoke();
            }
            else
            {
                onAct.Invoke();
            }
        }

        private void MoveCursor(Vector2 shipPos)
        {
            switch (_selected)
            {
            case -1:
                MoveTo(shipPos + new Vector2(-48, -48));
                break;
            case 1:
                MoveTo(shipPos + new Vector2(48, -48));
                break;
            default:
                MoveTo(shipPos);
                break;
            }
        }

        public void MoveTo(Vector2 pos)
        {
            this.Behaviors.Add(new MoveToBehavior(this, pos, 0.8f, false));
        }

        public void ResetSelection()
        {
            this._selected = 0;
        }
    }
}
