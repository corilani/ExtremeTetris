using Microsoft.Xna.Framework;
using System;
using System.Security.Cryptography;

namespace ExtremeTetris
{
    class Shape
    {
        public Vector2 _defaultBlockPos = new Vector2(-100, -100);
        public Vector2 _startBlockPos;
        public Vector2 _endBlockPos;

        private const int numShape2 = 3;
        private const int numShape3 = 38;
        private const int numShape4 = 1;

        private bool[,,] shape2x2 = new bool[numShape2, 2, 2]
        {
            // 0
            {{false, true},
             {false, true}},
            // 1
            {{false, true},
             {true,  true}},
            // 2
            {{true,  true},
             {true,  true}}
        };
        private bool[,,] shape3x3 = new bool[numShape3, 3, 3]
        {
            // 0
            {{false, true,  false},
             {true,  true,  true},
             {false, false, false}},
            // 1
            {{false, true,  false},
             {false, true,  false},
             {false, true,  false}},
            // 2
            {{false, true,  true},
             {true,  true,  false},
             {false, false, false}},
            // 3
            {{true,  true,  false},
             {false, true,  true},
             {false, false, false}},
            // 4
            {{true,  false, false},
             {true,  true,  true},
             {false, false, false}},
            // 5
            {{false, false, true},
             {true,  true,  true},
             {false, false, false}},
            // 6
            {{true,  false, false},
             {true,  false, false},
             {true,  true,  true}},
            // 7
            {{true,  false, false},
             {true,  true,  false},
             {true,  true,  true}},
            // 8
            {{true,  false, false},
             {true,  true,  false},
             {false, true,  true}},
            // 9
            {{false, true,  false},
             {true,  true,  true},
             {false, true,  false}},
            // 10
            {{false, true,  true},
             {true,  true,  true},
             {false, true,  false}},
            // 11
            {{true,  false, true},
             {true,  true,  true},
             {false, true,  false}},
            // 12
            {{false, true,  true},
             {true,  true,  true},
             {true,  true,  false}},
            // 13
            {{true,  false, true},
             {true,  true,  true},
             {true,  true,  true}},
            // 14
            {{true,  false, true},
             {true,  true,  true},
             {true,  false, true}},
            // 15
            {{false, false, true},
             {true,  true,  true},
             {true,  false, true}},
            // 16
            {{true,  false, true},
             {true,  true,  true},
             {false, false, true}},
            // 17
            {{false, false, true},
             {true,  true,  true},
             {false, true,  true}},
            // 18
            {{false, true,  true},
             {true,  true,  true},
             {false, false, true}},
            // 19
            {{true,  false, true},
             {true,  true,  true},
             {true,  true,  false}},
            // 20
            {{true,  false, true},
             {true,  true,  true},
             {false, true,  true}},
            // 21
            {{true,  true,  true},
             {true,  true,  true},
             {true,  true,  false}},
            // 22
            {{true,  true,  true},
             {true,  true,  true},
             {false, true,  true}},
            // 23
            {{true,  true,  false},
             {false, true,  false},
             {false, true,  true}},
            // 24
            {{false, true,  true},
             {false, true,  false},
             {true,  true,  false}},
            // 25
            {{true,  true,  true},
             {true,  true,  true},
             {false, true,  false}},
            // 26
            {{false, true,  false},
             {false, true,  false},
             {true,  true,  true}},
            // 27
            {{true,  false, false},
             {true,  true,  true},
             {false, true,  true}},
            // 28
            {{true,  true,  false},
             {false, true,  true},
             {false, true,  true}},
            // 29
            {{true,  true,  false},
             {true,  true,  false},
             {true,  true,  true}},
            // 30
            {{false, true,  true},
             {false, true,  true},
             {true,  true,  true}},
            // 31
            {{true,  true,  false},
             {true,  true,  false},
             {true,  true,  false}},
            // 32
            {{true,  false, true},
             {true,  false, true},
             {true,  true,  true}},
            // 33
            {{true,  false, true},
             {true,  true,  true},
             {false, false, false}},
            // 34
            {{false, false, true},
             {true,  false, true},
             {true,  true,  true}},
            // 35
            {{true,  false, false},
             {true,  false, true},
             {true,  true,  true}},
            // 36
            {{false, true,  true },
             {true,  true,  false},
             {false, true,  false}},
            // 37
            {{true,  true,  false},
             {false, true,  true},
             {false, true,  false}}
        };
        private bool[,,] shape4x4 = new bool[numShape4, 4, 4]
        {
            // 0
            {{false, false, true, false},
             {false, false, true, false},
             {false, false, true, false},
             {false, false, true, false}}
        };

        public int shapeDimension;

        public int currentShapeIndex;
        public bool[,] currentShape;

        private static RNGCryptoServiceProvider rngCsp;

        private static int seed;

        /// <summary>
        /// Constructor
        /// </summary>
        public Shape()
        {
            _startBlockPos = _defaultBlockPos;
            _endBlockPos = _defaultBlockPos;

            rngCsp = new RNGCryptoServiceProvider();

            seed = numShape2 + numShape3 + numShape4;
        }

        /// <summary>
        /// Constructor for shape duplicating 
        /// </summary>
        /// <param name="dublicateShape"></param>
        public Shape(Shape dublicateShape)
        {
            currentShape = dublicateShape.currentShape;
            currentShapeIndex = dublicateShape.currentShapeIndex;
            shapeDimension = dublicateShape.shapeDimension;
            _startBlockPos = dublicateShape._startBlockPos;
            _endBlockPos = dublicateShape._endBlockPos;
        }

        private int NextRandomInt(int minValue, int maxExclusiveValue)
        {
            long diff = (long)maxExclusiveValue - minValue;
            long upperBound = uint.MaxValue / diff * diff;
            uint ui;
            do
            {
                ui = GenerateRandomUInt();
            } while (ui >= upperBound);
            return (int)(minValue + (ui % diff));
        }

        private uint GenerateRandomUInt()
        {
            var randomBytes = GenerateRandomBytes(sizeof(uint));
            return System.BitConverter.ToUInt32(randomBytes, 0);
        }

        private byte[] GenerateRandomBytes(int bytesNumber)
        {
            byte[] buffer = new byte[bytesNumber];
            rngCsp.GetBytes(buffer);
            return buffer;
        }

        /// <summary>
        /// Creates new shape with selected shape index
        /// </summary>
        /// <param name="table"></param>
        /// <param name="index"></param>
        public void CreateNewShape(Table table, int index)
        {
            currentShapeIndex = index;
            Console.WriteLine(currentShapeIndex);
            // 2x2
            if (currentShapeIndex < numShape2)
            {
                Console.WriteLine("->" + currentShapeIndex);
                shapeDimension = 2;
                currentShape = new bool[shapeDimension, shapeDimension];
                for (int x = 0; x < shapeDimension; x++)
                    for (int y = 0; y < shapeDimension; y++)
                    {
                        currentShape[x, y] = shape2x2[currentShapeIndex, x, y];
                    }
                _startBlockPos = new Vector2(table.GetCenter() - 1, 0);
                _endBlockPos = new Vector2(table.GetCenter() + 0, shapeDimension - 1);
            }
            else
            // 3x3
            if (currentShapeIndex < numShape2 + numShape3)
            {
                Console.WriteLine("->" + (currentShapeIndex - numShape2));
                shapeDimension = 3;
                currentShape = new bool[shapeDimension, shapeDimension];
                for (int x = 0; x < shapeDimension; x++)
                    for (int y = 0; y < shapeDimension; y++)
                    {
                        currentShape[x, y] = shape3x3[currentShapeIndex - numShape2, x, y];
                    }
                _startBlockPos = new Vector2(table.GetCenter() - 1, 0);
                _endBlockPos = new Vector2(table.GetCenter() + 1, shapeDimension - 1);
            }
            else
            // 4x4
            {
                Console.WriteLine("->" + (currentShapeIndex - (numShape2 + numShape3)));
                shapeDimension = 4;
                currentShape = new bool[shapeDimension, shapeDimension];
                for (int x = 0; x < shapeDimension; x++)
                    for (int y = 0; y < shapeDimension; y++)
                    {
                        currentShape[x, y] = shape4x4[currentShapeIndex - (numShape2 + numShape3), x, y];
                    }
                _startBlockPos = new Vector2(table.GetCenter() - 2, 0);
                _endBlockPos = new Vector2(table.GetCenter() + 1, shapeDimension - 1);
            }
        }

        /// <summary>
        /// Creates new random shape
        /// </summary>
        /// <param name="table"></param>
        public void CreateNewShape(Table table)
        {
            currentShapeIndex = NextRandomInt(0, seed);
            Console.WriteLine(currentShapeIndex);
            // 2x2
            if (currentShapeIndex < numShape2)
            {
                Console.WriteLine("->" + currentShapeIndex);
                shapeDimension = 2;
                currentShape = new bool[shapeDimension, shapeDimension];
                for (int x = 0; x < shapeDimension; x++)
                    for (int y = 0; y < shapeDimension; y++)
                    {
                        currentShape[x, y] = shape2x2[currentShapeIndex, x, y];
                    }
                _startBlockPos = new Vector2(table.GetCenter() - 1, 0);
                _endBlockPos = new Vector2(table.GetCenter() + 0, shapeDimension - 1);
            }
            else
            // 3x3
            if (currentShapeIndex < numShape2 + numShape3)
            {
                Console.WriteLine("->" + (currentShapeIndex - numShape2));
                shapeDimension = 3;
                currentShape = new bool[shapeDimension, shapeDimension];
                for (int x = 0; x < shapeDimension; x++)
                    for (int y = 0; y < shapeDimension; y++)
                    {
                        currentShape[x, y] = shape3x3[currentShapeIndex - numShape2, x, y];
                    }
                _startBlockPos = new Vector2(table.GetCenter() - 1, 0);
                _endBlockPos = new Vector2(table.GetCenter() + 1, shapeDimension - 1);
            }
            else
            // 4x4
            {
                Console.WriteLine("->" + (currentShapeIndex - (numShape2 + numShape3)));
                shapeDimension = 4;
                currentShape = new bool[shapeDimension, shapeDimension];
                for (int x = 0; x < shapeDimension; x++)
                    for (int y = 0; y < shapeDimension; y++)
                    {
                        currentShape[x, y] = shape4x4[currentShapeIndex - (numShape2 + numShape3), x, y];
                    }
                _startBlockPos = new Vector2(table.GetCenter() - 2, 0);
                _endBlockPos = new Vector2(table.GetCenter() + 1, shapeDimension - 1);
            }
        }

        /// <summary>
        /// Checks if the shape positions (and block) are existing
        /// </summary>
        /// <returns>true - if exists, false - if not</returns>
        public bool CheckExisting()
        {
            if (_startBlockPos.X != _defaultBlockPos.X && _startBlockPos.Y != _defaultBlockPos.Y &&
                _endBlockPos.X != _defaultBlockPos.X && _endBlockPos.Y != _defaultBlockPos.Y)
                return true;
            return false;
        }

        /// <summary>
        /// Checks shape collision at left
        /// </summary>
        /// <param name="table"></param>
        /// <returns>true - if left is clear, false - if not</returns>
        private bool CheckLeftCollision(Table table)
        {
            for (int y = 0; y < shapeDimension; y++)
                for (int x = 0; x < shapeDimension; x++)
                    if (currentShape[y, x] == false)
                        continue;
                    else
                        if ((_startBlockPos.X + x) <= 0)
                        return false;
                    else
                        if (table.tableContains[(int)_startBlockPos.Y + y,
                                               (int)_startBlockPos.X + x - 1] == true)
                        return false;
                    else
                        break;
            return true;
        }

        /// <summary>
        /// Checks shape collision at right
        /// </summary>
        /// <param name="table"></param>
        /// <returns>true - if left is clear, false - if not</returns>
        private bool CheckRigthCollision(Table table)
        {
            for (int y = 0; y < shapeDimension; y++)
                for (int x = shapeDimension - 1; x >= 0; x--)
                    if (currentShape[y, x] == false)
                        continue;
                    else
                        if ((_endBlockPos.X - ((shapeDimension - 1) - x)) >= table.GetCol() - 1)
                        return false;
                    else
                        if (table.tableContains[(int)_startBlockPos.Y + y,
                                               (int)_endBlockPos.X - ((shapeDimension - 1) - x) + 1])
                        return false;
                    else
                        break;
            return true;
        }

        /// <summary>
        /// Checks shape collision at bottom
        /// </summary>
        /// <param name="table"></param>
        /// <returns>true - if left is clear, false - if not</returns>
        private bool CheckBottomCollision(Table table)
        {
            for (int x = shapeDimension - 1; x >= 0; x--)
                for (int y = shapeDimension - 1; y >= 0; y--)
                    if (currentShape[y, x] == false)
                        continue;
                    else
                        if (((_endBlockPos.Y) - ((shapeDimension - 1) - y)) >= table.GetRow() - 1)
                        return false;
                    else
                        if (table.tableContains[(int)_endBlockPos.Y - ((shapeDimension - 1) - y) + 1,
                                                (int)_endBlockPos.X - ((shapeDimension - 1) - x)] == true)
                        return false;
                    else
                        break;
            return true;
        }

        /// <summary>
        /// Checks shape collision at bottom with pos offset on y
        /// </summary>
        /// <param name="table"></param>
        /// <returns>true - if left is clear, false - if not</returns>
        private bool CheckBottomCollision(Table table, int yoffset)
        {
            for (int x = shapeDimension - 1; x >= 0; x--)
                for (int y = shapeDimension - 1; y >= 0; y--)
                    if (currentShape[y, x] == false)
                        continue;
                    else
                        if (((_endBlockPos.Y + yoffset) - ((shapeDimension - 1) - y)) >= table.GetRow() - 1)
                        return false;
                    else
                        if (table.tableContains[((int)_endBlockPos.Y + yoffset) - ((shapeDimension - 1) - y) + 1,
                                                (int)_endBlockPos.X - ((shapeDimension - 1) - x)] == true)
                        return false;
                    else
                        break;
            return true;
        }

        /// <summary>
        /// Checks all side shape collisions
        /// </summary>
        /// <param name="table"></param>
        /// <param name="checkShape"></param>
        /// <returns></returns>
        private bool CheckAllCollision(Table table, bool[,] checkShape)
        {
            for (int y = 0; y < shapeDimension; y++)
                for (int x = 0; x < shapeDimension; x++)
                    if (checkShape[y, x] == true)
                        if ((_startBlockPos.X + x) < 0 ||
                            (_endBlockPos.X - ((shapeDimension - 1) - x) >= table.GetCol()) ||
                            (_endBlockPos.Y - ((shapeDimension - 1) - y) >= table.GetRow()))
                            return false;
                        else
                            if (table.tableContains[(int)_startBlockPos.Y + y,
                                                    (int)_startBlockPos.X + x] == true)
                            return false;
                        else
                            continue;
            return true;
        }

        /// <summary>
        /// Finds last bottom free position
        /// </summary>
        /// <param name="table"></param>
        /// <returns>true - if bottom exists and shape placed to it, false - if you are at bottom</returns>
        public int TryFindBottom(Table table)
        {
            // Counter for number of movings
            int count = 0;
            while (CheckBottomCollision(table))
            {
                MoveShapeDown(table);
                count++;
            }
            return count;
        }

        /// <summary>
        /// Finds last bottom position for ghost shape
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int TryFindBottomGhost(Table table)
        {
            // Counter for number of movings
            int count = 0;
            while (CheckBottomCollision(table, count))
            {
                count++;
            }
            if (count == 0)
                return count;
            else
                return count;
        }

        /// <summary>
        /// Rotates current shape counterclockwise
        /// </summary>
        public void RotateLeft(Table table)
        {
            bool[,] newShape = new bool[shapeDimension, shapeDimension];
            for (int y = 0; y < shapeDimension; y++)
                for (int x = 0; x < shapeDimension; x++)
                    newShape[y, x] = currentShape[x, shapeDimension - y - 1];
            if (CheckAllCollision(table, newShape))
                SetNewShape(newShape);
        }

        /// <summary>
        /// Rotates current shape clockwise
        /// </summary>
        public void RotateRight(Table table)
        {
            bool[,] newShape = new bool[shapeDimension, shapeDimension];
            for (int y = 0; y < shapeDimension; y++)
                for (int x = 0; x < shapeDimension; x++)
                    newShape[y, x] = currentShape[shapeDimension - x - 1, y];
            if (CheckAllCollision(table, newShape))
                SetNewShape(newShape);
        }

        /// <summary>
        /// Make current shape saved at tableContains
        /// </summary>
        /// <param name="table"></param>
        public void MakeShapeTrue(Table table)
        {
            for (int x = (int)_startBlockPos.X; x <= (int)_endBlockPos.X; x++)
                for (int y = (int)_startBlockPos.Y; y <= (int)_endBlockPos.Y; y++)
                    if (y < table.GetRow() &&
                        y >= 0 &&
                        x < table.GetCol() &&
                        x >= 0 &&
                        currentShape[(shapeDimension - 1) - ((int)_endBlockPos.Y - y), (shapeDimension - 1) - ((int)_endBlockPos.X - x)] == true)
                        table.tableContains[y, x] = true;
        }

        /// <summary>
        /// Sets shape by sent array of bools
        /// </summary>
        /// <param name="newShape"></param>
        private void SetNewShape(bool[,] newShape)
        {
            for (int y = 0; y < shapeDimension; y++)
                for (int x = 0; x < shapeDimension; x++)
                    currentShape[y, x] = newShape[y, x];
        }

        /// <summary>
        /// Moves current shape left at one block
        /// </summary>
        public bool MoveShapeLeft(Table table)
        {
            if (CheckExisting())
                if (CheckLeftCollision(table))
                {
                    _startBlockPos.X -= 1;
                    _endBlockPos.X -= 1;
                    return true;
                }
            return false;
        }

        /// <summary>
        /// Moves current shape right at one block
        /// </summary>
        public bool MoveShapeRight(Table table)
        {
            if (CheckExisting())
                if (CheckRigthCollision(table))
                {
                    _startBlockPos.X += 1;
                    _endBlockPos.X += 1;
                    return true;
                }
            return false;
        }

        /// <summary>
        /// Moves current shape down at one block
        /// </summary>
        public bool MoveShapeDown(Table table)
        {
            if (CheckExisting())
                if (CheckBottomCollision(table))
                {
                    _startBlockPos.Y += 1;
                    _endBlockPos.Y += 1;
                    return true;
                }
            return false;
        }

        /// <summary>
        /// Changes positions of shape
        /// </summary>
        /// <param name="tableDistination"></param>
        public void ChangeTable(Table tableDistination)
        {
            if (currentShapeIndex < numShape2)
            {
                _startBlockPos = new Vector2(tableDistination.GetCenter() - 1, 0);
                _endBlockPos = new Vector2(tableDistination.GetCenter() + 0, shapeDimension - 1);
            }
            else
            // 3x3
            if (currentShapeIndex < numShape2 + numShape3)
            {
                _startBlockPos = new Vector2(tableDistination.GetCenter() - 1, 0);
                _endBlockPos = new Vector2(tableDistination.GetCenter() + 1, shapeDimension - 1);
            }
            else
            // 4x4
            {
                _startBlockPos = new Vector2(tableDistination.GetCenter() - 2, 0);
                _endBlockPos = new Vector2(tableDistination.GetCenter() + 1, shapeDimension - 1);
            }
        }
    }
}
