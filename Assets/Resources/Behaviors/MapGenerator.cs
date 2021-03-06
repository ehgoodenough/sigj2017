﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	Map map;
	int wallZ = 10;

	public enum Situated
	{
		AboveLeft,
		AboveCenter,
		AboveRight,
		LeftMiddle,
		RightMiddle,
		BelowLeft,
		BelowCenter,
		BelowRight,
		Overlaps
	}

	public enum HAligned
	{
		Left,
		Center,
		Right
	}

	public enum VAligned
	{
		Above,
		Middle,
		Below
	}

	void Start ()
	{
		map = Map.instance;
	}

	public void GenerateLevel(int level)
	{
		switch (level) {
		case 0:
			GenerateLevel1();
			break;
		case 1:
			GenerateLevel2();
			break;
		case 2:
			GenerateLevel3();
			break;
		default:
			GenerateLevel1 ();
			break;
		}
		CreateTiles ();
	}

	private void GenerateLevel1()
	{
		Room hubRoom = CreateRoom (17, 11, new Vector3 (-8, -5, 10));
		Room neRoom = CreateRoom (6, 6, new Vector3 (4, 6, 10));
		CreateCorridor (hubRoom, neRoom, 4);
		Room nwRoom = CreateRoom (11, 10, new Vector3 (-14, 6, 10));
		CreateCorridor (hubRoom, nwRoom, 5);
		Room swRoom = CreateRoom (9, 7, new Vector3 (-11, -11, 10));
		swRoom.DesignateStart ();
		CreateCorridor (swRoom, hubRoom, 4);
		Room seRoom = CreateRoom (10, 4, new Vector3 (-1, -9, 10));
		CreateCorridor (seRoom, hubRoom, 10);
		Room nCorridor = CreateRoom (5, 12, new Vector3 (-2, 6, 10));
		CreateCorridor (hubRoom, nCorridor, 5);
		Room finalRoom = CreateRoom (9, 9, new Vector3 (-4, 17, 10));
		finalRoom.DesignateEnd ();
		CreateCorridor (nCorridor, finalRoom, 5);
	}

	private void GenerateLevel2()
	{
		Room startRoom = CreateRoom (7, 9, new Vector3 (-10, -11, 10));
		startRoom.DesignateStart ();
		Room startNorth = CreateRoom (9, 6, new Vector3 (-12, -3, 10));
		CreateCorridor (startRoom, startNorth, 5);
		Room startSouth = CreateRoom (9, 6, new Vector3 (-12, -16, 10));
		CreateCorridor (startSouth, startRoom, 5);
		Room startCorridorNorth = CreateRoom (13, 6, new Vector3 (-10, 2, 10));
		CreateCorridor (startNorth, startCorridorNorth, 7);
		Room startCorridorSouth = CreateRoom (13, 6, new Vector3 (-10, -21, 10));
		CreateCorridor (startCorridorSouth, startSouth, 7);
		Room mainHub = CreateRoom (22, 19, new Vector3 (-4, -16, 10));
		CreateCorridor (mainHub, startCorridorNorth, 5);
		CreateCorridor (startCorridorSouth, mainHub, 5);
		Room mainHubAnteNorth = CreateRoom (9, 5, new Vector3 (4, 2, 10));
		CreateCorridor (mainHub, mainHubAnteNorth, 9);
		Room mainHubAnteSouth = CreateRoom (7, 8, new Vector3 (4, -23, 10));
		CreateCorridor (mainHubAnteSouth, mainHub, 7);
		// Room farSouthLeft = CreateRoom (8, 6, new Vector3 (0, -28, 10));
		// Room farSouthRight = CreateRoom (8, 6, new Vector3 (7, -28, 10));
		// CreateCorridor (farSouthLeft, mainHubAnteSouth, 4);
		// CreateCorridor (farSouthRight, mainHubAnteSouth, 4);
		Room mainToEndCorridorSouth = CreateRoom (13, 6, new Vector3 (12, -21, 10));
		CreateCorridor (mainToEndCorridorSouth, mainHub, 4);
		Room secondHubAnteSouth = CreateRoom (8, 8, new Vector3 (19, -16, 10));
		CreateCorridor (mainToEndCorridorSouth, secondHubAnteSouth, 5);
		Room secondHub = CreateRoom (14, 12, new Vector3 (21, -9, 10));
		CreateCorridor (secondHubAnteSouth, secondHub, 6);
		Room mainToEndCorridorNorth = CreateRoom (13, 6, new Vector3 (14, 2, 10));
		CreateCorridor (mainHub, mainToEndCorridorNorth, 4);
		CreateCorridor (secondHub, mainToEndCorridorNorth, 4);
		Room finalRoom = CreateRoom (9, 10, new Vector3 (29, 2, 10));
		finalRoom.DesignateEnd ();
		CreateCorridor (secondHub, finalRoom, 4);
	}

	public void GenerateLevel3()
	{
		Room startRoom = CreateRoom (11, 9, new Vector3 (-4, -3, 10));
		startRoom.DesignateStart ();
		Room startSouthRoom = CreateRoom (11, 6, new Vector3 (-4, -8, 10));
		CreateCorridor (startSouthRoom, startRoom, 7);
		Room southHorizHall = CreateRoom (45, 7, new Vector3 (-21, -14, 10));
		CreateCorridor (southHorizHall, startSouthRoom, 7);
		Room southVertHallLeft = CreateRoom (8, 14, new Vector3 (-11, -8, 10));
		CreateCorridor (southHorizHall, southVertHallLeft, 8);
		Room southVertHallRight = CreateRoom (8, 14, new Vector3 (6, -8, 10));
		CreateCorridor (southHorizHall, southVertHallRight, 8);
		Room middleHorizHall = CreateRoom (45, 7, new Vector3 (-21, 5, 10));
		CreateCorridor (southVertHallLeft, middleHorizHall, 8);
		CreateCorridor (southVertHallRight, middleHorizHall, 8);
		Room northVertHallLeft = CreateRoom (8, 14, new Vector3 (-11, 11, 10));
		CreateCorridor (middleHorizHall, northVertHallLeft, 8);
		Room northVertHallRight = CreateRoom (8, 14, new Vector3 (6, 11, 10));
		CreateCorridor (middleHorizHall, northVertHallRight, 8);
		Room finalRoom = CreateRoom (11, 9, new Vector3 (-4, 11, 10));
		finalRoom.DesignateEnd ();
		Room finalNorthRoom = CreateRoom (11, 6, new Vector3 (-4, 19, 10));
		CreateCorridor (finalRoom, finalNorthRoom, 7);
		Room northHorizHall = CreateRoom (45, 7, new Vector3 (-21, 24, 10));
		CreateCorridor (northVertHallLeft, northHorizHall, 8);
		CreateCorridor (northVertHallRight, northHorizHall, 8);
		CreateCorridor (finalNorthRoom, northHorizHall, 7);
		Room swChamber = CreateRoom (13, 14, new Vector3 (-23, -8, 10));
		CreateCorridor (southHorizHall, swChamber, 7);
		CreateCorridor (swChamber, middleHorizHall, 7);
		Room seChamber = CreateRoom (13, 14, new Vector3 (13, -8, 10));
		CreateCorridor (southHorizHall, seChamber, 7);
		CreateCorridor (seChamber, middleHorizHall, 7);
		Room nwChamber = CreateRoom (13, 14, new Vector3 (-23, 11, 10));
		CreateCorridor (middleHorizHall, nwChamber, 7);
		CreateCorridor (nwChamber, northHorizHall, 7);
		Room neChamber = CreateRoom (13, 14, new Vector3 (13, 11, 10));
		CreateCorridor (middleHorizHall, neChamber, 7);
		CreateCorridor (neChamber, northHorizHall, 7);
		Room southChamber = CreateRoom (25, 9, new Vector3 (-11, -22, 10));
		CreateCorridor (southChamber, southHorizHall, 15);
		Room northChamber = CreateRoom (25, 9, new Vector3 (-11, 30, 10));
		CreateCorridor (northHorizHall, northChamber, 15);
	}

	// Connects room1 to room2
	public void ConnectRooms(Room r1, Room r2)
	{
		// Connect the room data structure
		r1.ConnectTo (r2.GetID ());
		r2.ConnectTo (r1.GetID ());

		// Create the corridor between the two rooms
	}

	// Creates a corridor between room1 and room2
	public void CreateCorridor(Room r1, Room r2, int corridorWidth)
	{
		// Get the position of each room
		Vector2 r1Pos = r1.GetPosition ();
		Vector2 r2Pos = r2.GetPosition ();

		// Get the dimensions of each room
		Vector2 r1Dim = r1.GetDimensions ();
		Vector2 r2Dim = r2.GetDimensions ();

		if (GetRelativePosition (r1, r2, corridorWidth) == Situated.AboveCenter) {
			int leftLimit = (int) Mathf.Max (r1Pos.x, r2Pos.x);
			int rightLimit = (int) Mathf.Min (r1Pos.x + r1Dim.x - 1, r2Pos.x + r2Dim.x - 1);
			// Debug.Log ("leftLimit: " + leftLimit);
			// Debug.Log ("rightLimit: " + rightLimit);
			int leftPad = (rightLimit - leftLimit - corridorWidth + 1) / 2;
			int rightPad = (rightLimit - leftLimit - corridorWidth + 1) - leftPad;
			// Debug.Log ("leftPad: " + leftPad);
			// Debug.Log ("rightPad: " + rightPad);

			// Create doorway from room1
			for (int i = 0; i < corridorWidth - 2; i++) {
				r1.SetTileAt ((int) (leftLimit + leftPad + 1 + i), (int) (r1Pos.y + r1Dim.y - 1), Map.Tile.Floor);
			}

			// Create doorway to room2
			for (int i = 0; i < corridorWidth - 2; i++) {
				r2.SetTileAt ((int) (leftLimit + leftPad + 1 + i), (int) (r2Pos.y), Map.Tile.Floor);
			}
		}
	}

	public Room CreateRoom(int width, int height, Vector3 pos)
	{
		// Debug.Log ("Creating Room...");
		Room room = new Room (width, height, (int) pos.x, (int) pos.y);
		for (int row = 0; row < height; row++) 
		{
			for (int col = 0; col < width; col++)
			{
				// Debug.Log ("Row, Col: " + row + ", " + col);
				if (0 == row || height - 1 == row || 0 == col || width - 1 == col)
				{
					if (2 == GameManager.currentLevel) 
					{
						float crackedChance = 0.15f;
						// Debug.Log ("crackedChance: " + crackedChance);
						float bloodyChance = crackedChance + 0.05f * (1 + GameManager.currentLevel);
						// Debug.Log ("bloodyChance: " + bloodyChance);
						float randRoll = Random.Range (0f, 1f);
						// Debug.Log ("randRoll: " + randRoll);
						if (crackedChance > randRoll)
						{
							// Debug.Log ("Creating Cracked Gold Wall");
							room.SetTile (row, col, Map.Tile.CrackedGoldWall);
						}
						else if (bloodyChance > randRoll)
						{
							// Debug.Log ("Creating Bloody Gold Wall");
							room.SetTile (row, col, Map.Tile.BloodyGoldWall);
						}
						else
						{
							room.SetTile (row, col, Map.Tile.GoldWall);
						}
					}
					else
					{
						float vineChance = 0.15f;
						// Debug.Log ("vineChance: " + vineChance);
						float bloodyChance = vineChance + 0.05f * (1 + GameManager.currentLevel);
						// Debug.Log ("bloodyChance: " + bloodyChance);
						float randRoll = Random.Range (0f, 1f);
						// Debug.Log ("randRoll: " + randRoll);
						if (vineChance > randRoll)
						{
							// Debug.Log ("Creating Vine Wall");
							room.SetTile (row, col, Map.Tile.VineWall);
						}
						else if (bloodyChance > randRoll)
						{
							// Debug.Log ("Creating Bloody Wall");
							room.SetTile (row, col, Map.Tile.BloodyWall);
						}
						else
						{
							room.SetTile (row, col, Map.Tile.Wall);
						}
					}
				} else {
					float crackedChance = 0.025f;
					float bloodyChance = crackedChance + 0.0225f * (1 + GameManager.currentLevel);
					float randRoll = Random.Range (0f, 1f);
					if (crackedChance > randRoll)
					{
						room.SetTile (row, col, Map.Tile.CrackedFloor);
					}
					else if (bloodyChance > randRoll)
					{
						room.SetTile (row, col, Map.Tile.BloodyFloor);
					}
					else
					{
						room.SetTile (row, col, Map.Tile.Floor);
					}
				}
			}
		}
		return room;
	}

	public void CreateTiles()
	{
		// Debug.Log ("Creating Tiles...");
		// Debug.Log ("# Rooms: " + Room.rooms.Count);
		foreach (Room room in Room.rooms.Values)
		{
			for (int row = 0; row < room.GetDimensions().y; row++) 
			{
				for (int col = 0; col < room.GetDimensions().x; col++)
				{
					// Debug.Log ("Row, Col: " + row + ", " + col);
					Vector2 pos = room.GetPosition();
					string key = map.getKeyFromPosition (new Vector2 (pos.x + col, pos.y + row));
					// Debug.Log ("Key: " + key);
					if (!map.tiles.ContainsKey (key)) {
						map.addTile (new Vector3 (pos.x + col, pos.y + row, wallZ), room.GetTileAt (row, col));
					} else {
						// Debug.Log ("Key already exists");
					}
				}
			}
		}
	}



	// Tells how r2 is situated relative to r1, given the corridor width
	public Situated GetRelativePosition(Room r1, Room r2, int corridorWidth)
	{
		// Get the position of each room
		Vector2 r1Pos = r1.GetPosition ();
		Vector2 r2Pos = r2.GetPosition ();

		// Get the dimensions of each room
		Vector2 r1Dim = r1.GetDimensions ();
		Vector2 r2Dim = r2.GetDimensions ();

		// Determine the horizontal alignment of room2 relative to room1
		HAligned hAlignment;
		if (r2Pos.x + r2Dim.x < r1Pos.x + corridorWidth)
		{
			hAlignment = HAligned.Left;
		} else if (r2Pos.x > r1Pos.x + r1Dim.x - corridorWidth)
		{
			hAlignment = HAligned.Right;
		} else
		{
			hAlignment = HAligned.Center;
		}

		// Determine the vertical alingmeent of room2 relative to room1
		VAligned vAlignment;
		if (r2Pos.y + r2Dim.y < r1Pos.y + corridorWidth)
		{
			vAlignment = VAligned.Below;
		} else if (r2Pos.y > r1Pos.y + r1Dim.y - corridorWidth)
		{
			vAlignment = VAligned.Above;
		} else
		{
			vAlignment = VAligned.Middle;
		}

		// What a mess...
		if (VAligned.Above == vAlignment) {
			if (HAligned.Left == hAlignment) {
				return Situated.AboveLeft;
			} else if (HAligned.Center == hAlignment) {
				return Situated.AboveCenter;
			} else if (HAligned.Right == hAlignment) {
				return Situated.AboveRight;
			}
		} else if (VAligned.Middle == vAlignment) {
			if (HAligned.Left == hAlignment) {
				return Situated.LeftMiddle;
			} else if (HAligned.Right == hAlignment) {
				return Situated.RightMiddle;
			}
		} else if (VAligned.Below == vAlignment) {
			if (HAligned.Left == hAlignment) {
				return Situated.BelowLeft;
			} else if (HAligned.Center == hAlignment) {
				return Situated.BelowCenter;
			} else if (HAligned.Right == hAlignment) {
				return Situated.BelowRight;
			}
		}

		return Situated.Overlaps;
	}

}
