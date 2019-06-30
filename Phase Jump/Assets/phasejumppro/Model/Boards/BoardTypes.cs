﻿using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Direction of movement on a hexagonal board
/// </summary>
public enum HexBoardDirection
{
	Northwest,
	North,
	Northeast,
	East,
	South,
	Southeast,
	Southwest
}

/// <summary>
/// Axial direction of travel
/// </summary>
public enum AxialDirection {
	Right,
	Left
}

public enum AxialType
{
	AxialAny,	// Any axial tile that touches the origin tile
	AxialEdge   // Any axial tile that has an edge touching the origin tile (no square diagonal)
}

namespace EnumExtension
{
	public static class HexBoardDirectionExtensions
	{
		public static HexBoardDirection Opposite(this HexBoardDirection direction)
		{
			switch (direction)
			{
				case HexBoardDirection.Northwest:
					return HexBoardDirection.Southeast;
				case HexBoardDirection.North:
					return HexBoardDirection.South;
				case HexBoardDirection.Northeast:
					return HexBoardDirection.Southwest;
				case HexBoardDirection.Southeast:
					return HexBoardDirection.Northwest;
				case HexBoardDirection.South:
					return HexBoardDirection.North;
				case HexBoardDirection.Southwest:
					return HexBoardDirection.Northeast;
			}

			return HexBoardDirection.North;
		}
	}
}
