using System.Collections;

using TetrisEngine.Models;

namespace ConsoleFrontend.Models
{
    internal class DifferenceMap : IEnumerable<DifferenceMapCell>
    {
        private List<List<DifferenceMapCell>> _map;

        public DifferenceMap(List<List<DifferenceMapCell>> map) => _map = map;

        public static DifferenceMap GetDefaultDifferenceMap(int width, int heigth)
        {
            var map = new List<List<bool>>(heigth);

            for (int i = 0; i < heigth; i++)
            {
                map[i] = new(width);
                for (int j = 0; j < width; j++)
                    map[i][j] = true;
            }

            return new DifferenceMap(map);
        }

        public static DifferenceMap GetDifferenceMap(Cell[][] lastFrame, Cell[][] currentFrame)
        {
            var map =
                from lastCells in lastFrame
                from currentCells in currentFrame
                select lastCells.Zip(currentCells,
                (a, b) => new DifferenceMapCell(b, a.IsFilled != b.IsFilled))
                .ToList();

            return new DifferenceMap(map.ToList());
        }

        public IEnumerator<DifferenceMapCell> GetEnumerator()
        {
            for (int i = 0; i < _map.Count; i++)
                for (int j = 0; j < _map[0].Count; j++)
                    yield return _map[i][j] with { Top = i, Left = j };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}