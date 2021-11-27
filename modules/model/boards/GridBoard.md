# Module: Model.Boards

**Parent:** [Model.Boards](boards.md)

## Template Class: GridBoard\<Tile>

Stores `GridTile` inside `GridCell` across N grid layers. Useful for games where we want to store the state of the board.

_Example:_ Tactics game. Each player has a fixed position on the board.

### Public Methods

**Tile GetTile(Vector3Int loc)**: Return the tile at the specified location, or null if it doesn't exist.
**bool IsBlocked(Rect2Int bounds, int depth)**: Returns true if the rectangle bounds is blocked at the specified layer.
**bool IsCellBlocked(Vector3Int loc)**: Returns true if the cell is blocked.
