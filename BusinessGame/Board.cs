using BusinessGame.Config;
using BusinessGame.Contracts;
using BusinessGame.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessGame
{
    public class Board
    {
        private char[] _pathSequence;       
        public List<Property> _boardCell;
        
        public Board(char[] pathSequence)
        {
            this._pathSequence = pathSequence;           
            BuildBoardCell();
        }

        private void BuildBoardCell()
        {
            _boardCell = new List<Property>();

            foreach (var path in _pathSequence)
            {
                switch (path)
                {
                    case 'E':
                        _boardCell.Add(new Property() { Type = PropertyType.None, Name = PropertyType.None.ToString(), Penalty = 0 });
                        break;
                    case 'J':
                        _boardCell.Add(new Property() { Type = PropertyType.Jail, Name = PropertyType.Jail.ToString(), Penalty = 150 });
                        break;
                    case 'T':
                        _boardCell.Add(new Property() { Type = PropertyType.Treasure, Name = PropertyType.Treasure.ToString(), Penalty = 200 });
                        break;
                    case 'H':
                        _boardCell.Add(new Hotel() { Type = PropertyType.Hotel, Name = PropertyType.Hotel.ToString(), Penalty = 50, Worth = 200, IsOwned = false });
                        break;

                }
                
            }
        }

        public Property GetBoardCell(int v)
        {
            return _boardCell[v % _boardCell.Count];
        }        
    }
}
