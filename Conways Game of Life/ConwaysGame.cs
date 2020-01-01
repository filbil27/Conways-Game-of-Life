using System;
using System.Collections.Generic;
using System.Linq;

namespace filbil27.ConwaysGameOfLife
{
    class ConwaysGame
    {
        public bool[,] CurrentGeneration { get; private set; }
        private bool[,] _nextGeneration;

        public int GridSize { get; private set; }

        public ConwaysGame(int size)
        {
            CurrentGeneration = new bool[size, size];
            GridSize = size;
            SetInitialRandomCellsState();
        }

        /// <summary>
        /// Calulates the alive/dead status of each for cell, for the next genaration.
        /// Once completed this is save to the current genaration
        /// </summary>
        public void Tick()
        {
            _nextGeneration = CurrentGeneration;

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    _nextGeneration[i, j] = GetCellsNextState(i, j);
                }
            }

            CurrentGeneration = _nextGeneration;
            _nextGeneration = null;
        }

        /// <summary>
        /// Randomly sets each cells slive/dead status
        /// </summary>
        private void SetInitialRandomCellsState()
        {
            var random = new Random();

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    CurrentGeneration[i, j] = random.NextDouble() >= 0.95;
                }
            }
        }

        /// <summary>
        /// Calulates the cells next state
        /// </summary>
        /// <param name="x">Cells X Coordinate</param>
        /// <param name="y">Cells Y Coordinate</param>
        /// <returns>Bool</returns>
        private bool GetCellsNextState(int x, int y)
        {
            var neighborsStates = GetNeighborsStates(x, y);
            var aliveNeighbors = neighborsStates.Where(o => o).Count();
            var currentState = CurrentGeneration[x, y];

            if (currentState)
            {
                if (aliveNeighbors == 2 || aliveNeighbors == 3) { return true; }
            }
            else
            {
                if (aliveNeighbors == 3) { return true; }
            }

            return false;
        }

        /// <summary>
        /// Collects a list of all the states that the cells neighbors are in
        /// </summary>
        /// <param name="x">Cells X Coordinate</param>
        /// <param name="y">Cells Y Coordinate</param>
        /// <returns>A List of all the states of the neighboring cells</returns>
        private List<bool> GetNeighborsStates(int x, int y)
        {
            var results = new List<bool> { };

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i < 0 || j < 0)
                    {
                        results.Add(false);
                    }
                    else if (i >= GridSize || j >= GridSize)
                    {
                        results.Add(false);
                    }
                    else if (!(i == x && j == y))
                    { 
                        results.Add(CurrentGeneration[i, j]); 
                    }
                }
            }

            return results;
        }
    }
}
